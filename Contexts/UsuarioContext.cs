using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TreinoSport.Contexts.Base;
using TreinoSport.Models;

namespace TreinoSport.Contexts {
    public class UsuarioContext : BaseContext{
        
        public async Task CadastrarUsuario(Usuario usuario) {
            try {

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, _treinoSportApiUrl + "/usuario/cadastrar");

                message.Content = JsonContent.Create(usuario);

                HttpResponseMessage response = await client.SendAsync(message);
                await response.HandleResponse();
                //response.EnsureSuccessStatusCode(); // Check that the status code is in the 200 range. Throw an HttpRequestException if not

                string responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e) {

                throw new Exception($"{e.Message}");
            }
        }

        public async Task<bool> ChecarEmail(string email) {
            try {

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                var queryParams = new Dictionary<string, object>() {
                    { "email", email}
                };

                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, _treinoSportApiUrl + "/usuario/email" + ParamsToString(queryParams));

                HttpResponseMessage response = await client.SendAsync(message);
                await response.HandleResponse();

                string responseBody = await response.Content.ReadAsStringAsync();

                return await HttpUtilities.GetBody<bool>(response);
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<int> Login(string email, string senha) {
            try {

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json;charset=UTF-8");

                var queryParams = new Dictionary<string, object>() {
                    { "email", email},
                    { "senha", senha}
                };

                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, _treinoSportApiUrl + "/login" + ParamsToString(queryParams));

                HttpResponseMessage response = await client.SendAsync(message);
                await response.HandleResponse();

                string responseBody = await response.Content.ReadAsStringAsync();

                return await HttpUtilities.GetBody<int>(response);
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }
    }
}
