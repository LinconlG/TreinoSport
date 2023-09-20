using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Contexts.Base;
using TreinoSport.Extensions;
using TreinoSport.Models;

namespace TreinoSport.Contexts {
    public class TreinoContext {

        public TreinoContext() { }

        public async Task<IEnumerable<Treino>> GetTreinosComoAluno(int codigoUsuario) {
            try {
                var endpoint = "/treino/aluno/todos";
                var queryParams = new Dictionary<string, object>() {
                    { "codigoUsuario", codigoUsuario}
                };

                HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Get, BaseContext.urlAndroidAPI, endpoint, queryParams);
                await response.HandleResponse();

                return await HttpUtilities.GetBody<IEnumerable<Treino>>(response);
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<IEnumerable<Treino>> GetTreinosComoCT(int codigoCT) {
            try {
                var endpoint = "/treino/ct/todos";

                var queryParams = new Dictionary<string, object>() {
                    { "codigoCT", codigoCT}
                };

                HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Get, BaseContext.urlAndroidAPI, endpoint, queryParams);
                await response.HandleResponse();

                return await HttpUtilities.GetBody<IEnumerable<Treino>>(response);
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task PutTreino(Treino treino) {
            try {
                var endpoint = "/treino/ct/criar";

                HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Put, BaseContext.urlAndroidAPI, endpoint, null, treino);
                await response.HandleResponse();
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<Treino> GetDetalhesTreino(int codigoTreino) {
            try {
                var endpoint = "/treino/ct/detalhes";

                var queryParams = new Dictionary<string, object>() {
                    { "codigoTreino", codigoTreino}
                };

                HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Get, BaseContext.urlAndroidAPI, endpoint, queryParams);
                await response.HandleResponse();

                var treino = await HttpUtilities.GetBody<Treino>(response);
                return treino;
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task PatchDetalhesTreino(Treino treino) {
            try {
                var endpoint = "/treino/ct/detalhes";

                HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Patch, BaseContext.urlAndroidAPI, endpoint, body: treino);
                await response.HandleResponse();
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<IEnumerable<Treino>> GetTreinosParaGerenciar(int codigoCT) {
            try {
                var endpoint = "/treino/gerenciamento/lista";

                var queryParams = new Dictionary<string, object>() {
                    { "codigoCT", codigoCT}
                };

                HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Get, BaseContext.urlAndroidAPI, endpoint, queryParams);
                await response.HandleResponse();

                var treinos = await HttpUtilities.GetBody<IEnumerable<Treino>>(response);
                return treinos;
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<Treino> GetTreinoBasico(int codigoTreino) {
            try {
                var endpoint = "/treino/gerenciamento/especifico";

                var queryParams = new Dictionary<string, object>() {
                    { "codigoTreino", codigoTreino}
                };

                HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Get, BaseContext.urlAndroidAPI, endpoint, queryParams);
                await response.HandleResponse();

                var treino = await HttpUtilities.GetBody<Treino>(response);
                return treino;
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<List<Conta>> GetAlunos(int codigoTreino) {
            try {
                var endpoint = "/treino/alunos";

                var queryParams = new Dictionary<string, object>() {
                    { "codigoTreino", codigoTreino}
                };

                HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Get, BaseContext.urlAndroidAPI, endpoint, queryParams);
                await response.HandleResponse();

                var alunos = await HttpUtilities.GetBody<List<Conta>>(response);
                return alunos;
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }

        public async Task<int> PutAluno(int codigoTreino, string emailAluno) {
                var endpoint = "/treino/alunos";

                var queryParams = new Dictionary<string, object>() {
                    { "codigoTreino", codigoTreino},
                    { "emailAluno", emailAluno}
                };

                HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Put, BaseContext.urlAndroidAPI, endpoint, queryParams);
                await response.HandleResponse();

                var alunoInserido = await HttpUtilities.GetBody<int>(response);
                return alunoInserido;
        }

        public async Task DeleteTreino(int codigoTreino) {
            var endpoint = "/treino/ct/detalhes";

            var queryParams = new Dictionary<string, object>() {
                    { "codigoTreino", codigoTreino}
                };

            HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Delete, BaseContext.urlAndroidAPI, endpoint, queryParams);
            await response.HandleResponse();
        }

        public async Task DeleteAluno(int codigoTreino, int codigoAluno) {
            try {
                var endpoint = "/treino/alunos";

                var queryParams = new Dictionary<string, object>() {
                    { "codigoTreino", codigoTreino},
                    { "codigoConta", codigoAluno}
                };

                HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Delete, BaseContext.urlAndroidAPI, endpoint, queryParams);
                await response.HandleResponse();
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }
    }
}
