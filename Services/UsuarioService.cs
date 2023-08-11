using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private async Task<bool> ChecarEmail(string email) {
            return await _usuarioContext.ChecarEmail(email);
        }
    }
}
