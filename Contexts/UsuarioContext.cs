using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Net.Http.Json;
using System.Text.Json;
using TreinoSport.Contexts.Base;
using TreinoSport.Models;

namespace TreinoSport.Contexts {
    public class UsuarioContext {

        protected readonly HttpClient _httpClient;
        protected readonly IConfiguration _configuration;
        protected readonly string _treinoSportApiUrl;

        public UsuarioContext() {
            _httpClient = new HttpClient();
            _treinoSportApiUrl = "http://192.168.1.109:5050/api";
        }

        public async Task CadastrarUsuario(Usuario usuario) {

            var msg = HttpUtilities.Put(_treinoSportApiUrl + "/usuario/cadastrar", JsonSerializer.Serialize(usuario));

            var uri = _treinoSportApiUrl + "/usuario/cadastrar";
            try {
                var response = await _httpClient.SendAsync(msg);
            }
            catch (Exception e) {

                throw new Exception(e.Message);
            }
        }

        public string ParamsToString(Dictionary<string, object> queryParams) {
            if (queryParams == null || queryParams.Count == 0) {
                return "";
            }
            string stringParams = "?";
            foreach (var param in queryParams) {
                if (param.Value != null && param.Value != "") {

                    if (param.Value is IList) {
                        foreach (object item in param.Value as IList) {
                            stringParams += $"{param.Key}={item}&";
                        }
                    }
                    else {
                        stringParams += param.Key + "=" + param.Value + "&";
                    }
                }
            }
            return stringParams;
        }
    }
}
