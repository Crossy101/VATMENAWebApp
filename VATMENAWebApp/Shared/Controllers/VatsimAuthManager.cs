using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VATMENAWebApp.Shared.Models.VATSIM;
using VATMENAWebApp.Shared.Models.VATSIMAuth;

namespace VATMENAWebApp.Shared.Controllers
{
    public class VatsimAuthManager
    {
        private string userDataURL = "https://auth.vatsim.net/api/user";

        public async Task<VatsimTokenResponse> SendVatsimAuthRequest(string code)
        {
            HttpClient httpClient = new HttpClient();

            VatsimTokenRequest vatsimTokenRequest = new VatsimTokenRequest(code);

            string body = JsonConvert.SerializeObject(vatsimTokenRequest);
            HttpContent content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("https://auth.vatsim.net/oauth/token", content);
            string data = await response.Content.ReadAsStringAsync();

            VatsimTokenResponse vatsimTokenResponse = JsonConvert.DeserializeObject<VatsimTokenResponse>(data);
            return vatsimTokenResponse;
        }

        public async Task<VatsimUserDetails> GetUserDetails(VatsimTokenResponse token)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(userDataURL),
                    Headers = {
                    { HttpRequestHeader.Authorization.ToString(), $"Bearer {token.access_token}" },
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                    { "X-Version", "1" }
                }
                };

                HttpResponseMessage response = await httpClient.SendAsync(httpRequestMessage);
                string data = await response.Content.ReadAsStringAsync();
                VatsimUserDetails vatsimUserDetails = JsonConvert.DeserializeObject<VatsimUserDetails>(data);

                return vatsimUserDetails;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<VatsimUserDetails> GetUserDetails(string token)
        {
            try
            {
                VatsimUserDetails vatsimUserDetails = await this.GetUserDetails(new VatsimTokenResponse
                {
                    access_token = token
                });

                if (vatsimUserDetails != null && vatsimUserDetails.data.oauth.token_valid == "true")
                    return vatsimUserDetails;
                else
                    return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> CheckTokenValidity(string token)
        {
            try
            {
                VatsimUserDetails vatsimUserDetails = await this.GetUserDetails(new VatsimTokenResponse
                {
                    access_token = token
                });

                if (vatsimUserDetails != null && vatsimUserDetails.data.oauth.token_valid == "true")
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
