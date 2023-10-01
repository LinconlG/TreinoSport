using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Contexts;

namespace TreinoSport.ViewModels {
    public class RedefinirSenhaViewModel {

        private readonly UsuarioContext usuarioContext;

        public RedefinirSenhaViewModel() {
            usuarioContext = new();
        }

        public Task<int> EnviarTokenSenha(string email) {
            return usuarioContext.PutEnviarTokenSenha(email);
        }
        public Task ChecarTokenSenha(int codigoConta, string tokenInserido) {
            return usuarioContext.GetChecarTokenSenha(codigoConta, tokenInserido);
        }
        public Task RedefinirSenha(int codigoConta, string novaSenha, string tokenInserido) {
            return usuarioContext.PutRedefinirSenha(codigoConta, novaSenha, tokenInserido);
        }
    }
}
