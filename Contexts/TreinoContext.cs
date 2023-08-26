using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Contexts.Base;
using TreinoSport.Models;

namespace TreinoSport.Contexts {
    public class TreinoContext : BaseContext{
        public TreinoContext() { }

        public async Task<IEnumerable<Treino>> GetTreinosComoAluno(int codigoUsuario) {
            try {
                var queryParams = new Dictionary<string, object>() {
                    { "codigoUsuario", codigoUsuario}
                };

                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, _treinoSportApiUrl + "/treino/aluno/todos" + ParamsToString(queryParams));

                HttpResponseMessage response = await httpClient.SendAsync(message);
                await response.HandleResponse();

                string responseBody = await response.Content.ReadAsStringAsync();

                return await HttpUtilities.GetBody<IEnumerable<Treino>>(response);
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<IEnumerable<Treino>> GetTreinosComoCT(int codigoCT) {
            try {
                var queryParams = new Dictionary<string, object>() {
                    { "codigoCT", codigoCT}
                };

                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, _treinoSportApiUrl + "/treino/ct/todos" + ParamsToString(queryParams));

                HttpResponseMessage response = await httpClient.SendAsync(message);
                await response.HandleResponse();

                string responseBody = await response.Content.ReadAsStringAsync();

                return await HttpUtilities.GetBody<IEnumerable<Treino>>(response);
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }
    }
}
