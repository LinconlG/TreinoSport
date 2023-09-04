﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Contexts.Base;
using TreinoSport.Models;

namespace TreinoSport.Contexts {
    public class TreinoContext : BaseContext{

        public TreinoContext(IConfiguration configuration) : base(configuration) {}

        public async Task<IEnumerable<Treino>> GetTreinosComoAluno(int codigoUsuario) {
            try {
                var endpoint = "/treino/aluno/todos";
                var queryParams = new Dictionary<string, object>() {
                    { "codigoUsuario", codigoUsuario}
                };

                HttpResponseMessage response = await HttpResquest(HttpMethod.Get, _treinoSportApiUrl, endpoint, queryParams);
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

                HttpResponseMessage response = await HttpResquest(HttpMethod.Get, _treinoSportApiUrl, endpoint, queryParams);
                await response.HandleResponse();

                return await HttpUtilities.GetBody<IEnumerable<Treino>>(response);
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }
    }
}
