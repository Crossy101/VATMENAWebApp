using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VATMENAWebApp.Server.Data;
using VATMENAWebApp.Shared.Config;
using VATMENAWebApp.Shared.Controllers;
using VATMENAWebApp.Shared.Models.Permissions;
using VATMENAWebApp.Shared.Models.VATSIM;
using VATMENAWebApp.Shared.Models.VATSIM.MemberData;
using VATMENAWebApp.Shared.Models.VATSIMAuth;

namespace VATMENAWebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        VatsimAuthManager vatsimAuthManager;

        private readonly ILogger<AuthenticationController> _logger;
        private readonly VatmenaDbContext _context;
        private readonly VATMENAConfig _config;

        public AuthenticationController(ILogger<AuthenticationController> logger, IOptions<VATMENAConfig> config, VatmenaDbContext context)
        {
            this.vatsimAuthManager = new VatsimAuthManager();
            _logger = logger;
            _context = context;
            _config = config.Value;
        }

        /// <summary>
        /// This function checks the current users auth_token once logged into VATSIM Auth API using OAuth 2.0.
        /// Once complete, vatsim will send a code to the user 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(string code)
        {
            try
            {
                string currentToken = Request.Cookies["auth_token"];
                if (currentToken != null)
                {
                    bool tokenValid = await this.vatsimAuthManager.CheckTokenValidity(currentToken);
                    if(tokenValid)
                        return Redirect(_config.WebAppAddress + "/Dashboard");
                }


                VatsimTokenResponse vatsimTokenResponse = await this.vatsimAuthManager.SendVatsimAuthRequest(code);
                VatsimUserDetails vatsimUserDetails = await this.vatsimAuthManager.GetUserDetails(vatsimTokenResponse);

                VatsimMember foundMember = _context.VatsimMembers.FirstOrDefault(vMember => vMember.Id == vatsimUserDetails.data.cid);
                if (foundMember == null && vatsimUserDetails != null)
                    await CreateUserAccount(vatsimUserDetails);
                else if(foundMember == null && vatsimUserDetails == null)
                    return Redirect("https://test.vatsim.me:44347");

                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.MaxAge = new TimeSpan(7, 0, 0, 0);

                Response.Cookies.Append("vatsim_cid", vatsimUserDetails.data.cid, cookieOptions);
                Response.Cookies.Append("auth_token", vatsimTokenResponse.access_token, cookieOptions);

                return Redirect(_config.WebAppAddress + "/Dashboard");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// This function is called on every web browser every 60 seconds and will confirm if the current user has an authenticated token to be logged in.
        /// </summary>
        /// <returns></returns>
        [HttpGet("auth/token")]
        public async Task<IActionResult> Token()
        {
            string auth_token = Request.Cookies["auth_token"];
            string cid = Request.Cookies["vatsim_cid"];
            if(auth_token == null || cid == null)
                return Ok(false);

            bool autenticated = await this.vatsimAuthManager.CheckTokenValidity(auth_token);

            if (autenticated)
                return Ok(true);
            else
                return Ok(false);
        }

        /// <summary>
        /// This function will log the user out and delete any cookies associated with the website
        /// </summary>
        /// <returns></returns>
        [HttpGet("auth/logout")]
        public async Task<IActionResult> Logout()
        {
            await Task.Run(() =>
            {
                List<KeyValuePair<string, string>> cookies = Request.Cookies.ToList();
                foreach (var cookie in cookies)
                {
                    Response.Cookies.Delete(cookie.Key);
                }
            });


            return Ok();
        }

        /// <summary>
        /// If a user doesn't have a current account under the database then this function will perform all necessary database info to be added.
        /// </summary>
        /// <param name="vatsimUserDetails"></param>
        /// <returns></returns>
        public async Task CreateUserAccount(VatsimUserDetails vatsimUserDetails)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync($"https://api.vatsim.net/api/ratings/{vatsimUserDetails.data.cid}");

            string data = await response.Content.ReadAsStringAsync();
            VatsimMember vatsimMember = JsonConvert.DeserializeObject<VatsimMember>(data);
            vatsimMember.FullName = vatsimUserDetails.data.personal.name_full;

            UserPermission userPermissions = new UserPermission
            {
                Id = vatsimMember.Id,
                IsAdmin = false,
                AdminEvents = false
            };

            PersonalDetail personalDetails = new PersonalDetail
            {
                Id = vatsimMember.Id,
                FirstName = vatsimUserDetails.data.personal.name_first,
                LastName = vatsimUserDetails.data.personal.name_last,
                Email = vatsimUserDetails.data.personal.email,
                Country = vatsimUserDetails.data.personal.country.name
            };

            await _context.VatsimMembers.AddAsync(vatsimMember);
            await _context.UserPermissions.AddAsync(userPermissions);
            await _context.PersonalDetails.AddAsync(personalDetails);
            await _context.SaveChangesAsync();
        }
    }
}
