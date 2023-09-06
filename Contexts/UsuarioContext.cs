﻿using Microsoft.Extensions.Configuration;
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
    public class UsuarioContext {

        public UsuarioContext() { }

        public async Task<bool> CadastrarUsuario(Conta usuario) {
            try {
                var endpoint = "/usuario/cadastrar";
                HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Put, BaseContext.urlAndroidAPI, endpoint, body: usuario);
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

                var endpoint = "/login";

                var queryParams = new Dictionary<string, object>() {
                    { "email", email},
                    { "senha", senha}
                };

                HttpResponseMessage response = await BaseContext.HttpResquest(HttpMethod.Get, BaseContext.urlAndroidAPI, endpoint, queryParams);
                await response.HandleResponse();

                return await HttpUtilities.GetBody<Conta>(response);
            }
            catch (Exception e) {
                throw new Exception($"{e.Message}");
            }
        }
    }
}
