using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace SlackBoard.Modules
{

    internal class Login
    {
        public string userName { get; set; }
        public string passWord { get; set; }
    }

    class Token
    {
        public Token()
        {

        }

        public String authorizationToken { get; set; }
    }

    public class RestService
    {
        internal HttpClient webClient;

        public RestService()
        {
            webClient = new HttpClient()
            {
                MaxResponseContentBufferSize = 256000
            };
        }

        internal async Task<bool> ValidateLoginAsync(String username, String password)
        {
            Login loginsession = new Login
            {
                userName = username,
                passWord = password
            };

            try
            {
                StringContent requestContent = new StringContent(JsonConvert.SerializeObject(loginsession), Encoding.UTF8, "application/json");

                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://slackboard.azurewebsites.net/api/Login")
                {
                    Content = requestContent
                };

                var response = await webClient.SendAsync(httpRequest);

                if (response.IsSuccessStatusCode)
                {
                    String responseToken = await response.Content.ReadAsStringAsync();

                    Token key = JsonConvert.DeserializeObject<Token>(responseToken);

                    Toast.MakeText(Application.Context, "Your login has been successful", ToastLength.Short).Show();

                    var tokenFile = Application.Context.GetSharedPreferences("token", FileCreationMode.Private);

                    var tokenEdit = tokenFile.Edit().PutString("token", key.authorizationToken).Commit();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "There has been an error.", ToastLength.Long).Show();
                return false;
            }
        }
    }
}