﻿using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Hosting;
using TreinoSport.Contexts;
using TreinoSport.Models;

namespace TreinoSport.Services {
    public class UsuarioService {

        private readonly UsuarioContext _usuarioContext;

        public UsuarioService() {
            _usuarioContext = new UsuarioContext();
        }

        public async Task CadastrarUsuario(Usuario usuario) {
            var a = await ChecarEmail(usuario.Email);
            if (a) {
                throw new Exception("@O email inserido já existe.@");
            }
            await _usuarioContext.CadastrarUsuario(usuario);
        }

        public async Task Login(string email, string senha) {
            var codigoUsuario = await _usuarioContext.Login(email, senha);
            Preferences.Set("codigoUsuario", codigoUsuario);
        }

        private async Task<bool> ChecarEmail(string email) {
            return await _usuarioContext.ChecarEmail(email);
        }
    }
}
