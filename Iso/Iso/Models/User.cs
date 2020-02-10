using Newtonsoft.Json;

namespace Iso.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "idusuario")]
        public int IdUsuario { get; set; }

        [JsonProperty(PropertyName = "idempresa")]
        public int IdEmpresa { get; set; }

        [JsonProperty(PropertyName = "dispositivo")]
        public string Dispositivo { get; set; }

        [JsonProperty(PropertyName = "nombreusuario")]
        public string NombreUsuario { get; set; }

        [JsonProperty(PropertyName = "passwordusuario")]
        public string PasswordUsuario { get; set; }

        [JsonProperty(PropertyName = "iconousuario")]
        public string IconoUsuario { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
