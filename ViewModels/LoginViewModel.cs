﻿using TreinoSport.Contexts;
using TreinoSport.Models;

namespace TreinoSport.ViewModels {
    public class LoginViewModel {

        private readonly UsuarioContext _usuarioContext;

        public LoginViewModel() {
            _usuarioContext = new();
        }

        public async Task Login(string email, string senha) {
            //var conta = await _usuarioContext.Login(email, senha);
            Conta conta = new() {
                Codigo = 6,
                IsCentroTreinamento = true
            };
            ContaStatic.SetCodigo(conta.Codigo);
            ContaStatic.SetIsCT(conta.IsCentroTreinamento);
        }
    }
}
