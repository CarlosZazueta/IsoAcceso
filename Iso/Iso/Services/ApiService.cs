namespace Iso.Services
{
    using Models;
    using System.Threading.Tasks;
    using Plugin.Connectivity;
    using System.Net.Http;
    using System;
    using Newtonsoft.Json;

    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet settings."
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check your internet connection."
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok"
            };
        }

        public async Task<string> Get<T>(string urlBase)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(urlBase);
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }
    }
}
