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
                    Message = "Por favor habilita tus opciones de conexión."
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Revisa tu conexión a internet."
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok"
            };
        }

        public async Task<Response> AccessRegister(
            string url,
            string password,
            string usuario,
            int idEmpresa,
            string nombreDispositivo)
        {
            try
            {
                var client = new HttpClient();
                var content = new StringContent(
                    JsonConvert.SerializeObject(new
                    {
                        passwordusuario = password,
                        nombreusuario = usuario,
                        idempresa = idEmpresa,
                        dispositivo = nombreDispositivo
                    }));
                var result = await client.PostAsync(url, content);

                var tokenJson = "";
                if (result.IsSuccessStatusCode)
                {
                    tokenJson = await result.Content.ReadAsStringAsync();
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "¡Bienvenido!"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "¡Error, usuario o contraseña incorrectos!"
                };
            }
        }

        public async Task<Response> Get<T>(string urlBase, string servicePrefix, string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var user = JsonConvert.DeserializeObject<User>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = user
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
