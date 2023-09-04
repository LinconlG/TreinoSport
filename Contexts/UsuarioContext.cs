using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TreinoSport.Contexts.Base;
using TreinoSport.Models;

namespace TreinoSport.Contexts {
    public class UsuarioContext : BaseContext{
        public UsuarioContext(IConfiguration configuration) : base(configuration) { }

        public async Task<bool> CadastrarUsuario(Conta usuario) {
            try {
                var endpoint = "/usuario/cadastrar";
                HttpResponseMessage response = await HttpResquest(HttpMethod.Put, _treinoSportApiUrl, endpoint, body: usuario);
                await response.HandleResponse();
                var emailExiste = await response.GetBody<bool>();
                return emailExiste;
            }
            catch (Exception e) {

                throw new Exception($"{e.Message}");
            }
        }

        public async Task<Conta> Login(string email, string senha) {
            try {                

                var queryParams = new Dictionary<string, object>() {
                    { "email", email},
                    { "senha", senha}
                };

                HttpRequestMessage message = new(HttpMethod.Get, _treinoSportApiUrl + "/login" + ParamsToString(queryParams));

                HttpResponseMessage response = await httpClient.SendAsync(message);
                await response.HandleResponse();

                string responseBody = await response.Content.ReadAsStringAsync();

                return await HttpUtilities.GetBody<Conta>(response);
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }
    }
}
