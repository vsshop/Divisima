using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using WebApplication48.Models;

namespace WebApplication48.Services
{
    public class GoogleService
    {
        private const string _clientId = "";
        private const string _clientSecret = "";
        public string CreateRedirectGoogle()
        {
            string url = "https://accounts.google.com/o/oauth2/v2/auth";

            Dictionary<string, string> query = new Dictionary<string, string>()
            {
                {"client_id", _clientId },
                {"redirect_uri", "https://cyberbionic.com.ua/ApiGoogle/GetGoogleCode" },
                {"response_type", "code" },
                {"scope", "email" },
                {"access_type", "offline" }
            };

            return QueryHelpers.AddQueryString(url, query);
        }



        public async Task<BaseResponse<GoogleToken>> GetGoogleToken(string code)
        {
            BaseResponse<GoogleToken> response = new BaseResponse<GoogleToken>();
            try
            {
                string url = "https://oauth2.googleapis.com/token";
                Dictionary<string, string> query = new Dictionary<string, string>()
                {
                    {"client_id", _clientId },
                    {"client_secret", _clientSecret },
                    {"code", code },
                    {"grant_type", "authorization_code" },
                    {"redirect_uri", "https://cyberbionic.com.ua/ApiGoogle/GetGoogleCode" },
                    {"access_type", "offline" }
                };

                HttpClient client = new HttpClient();
                var content = new FormUrlEncodedContent(query);
                var responce = await client.PostAsync(url, content);
                string json = await responce.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<GoogleToken>(json);
                response.Data = token;
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.Error;
                response.Message = ex.Message;
            }
            return response;
        }


        public async Task<BaseResponse<GooglePerson>> GetGooglePerson(GoogleToken token)
        {
            BaseResponse<GooglePerson> response = new BaseResponse<GooglePerson>();
            try
            {
                string url = "https://oauth2.googleapis.com/tokeninfo";

                Dictionary<string, string> query = new Dictionary<string, string>()
                {
                    {"id_token", token.IdToken }
                };

                HttpClient client = new HttpClient();
                var content = new FormUrlEncodedContent(query);
                var responce = await client.PostAsync(url, content);
                var result = await responce.Content.ReadAsStringAsync();
                var person = JsonConvert.DeserializeObject<GooglePerson>(result);

                response.Data = person;
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.Error;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
