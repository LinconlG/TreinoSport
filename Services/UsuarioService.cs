using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Hosting;
using TreinoSport.Contexts;
using TreinoSport.Models;

namespace TreinoSport.Services {
    public class UsuarioService {

        private readonly UsuarioContext _usuarioContext;

        public UsuarioService(UsuarioContext usuarioContext) {
            _usuarioContext = usuarioContext;
        }

        public async Task CadastrarUsuario(Conta usuario) {
            var emailExiste = await ChecarEmail(usuario.Email);
            if (emailExiste) {
                throw new Exception("@O email inserido já existe.@");
            }
            await _usuarioContext.CadastrarUsuario(usuario);
        }

        public async Task Login(string email, string senha) {
            var conta = await _usuarioContext.Login(email, senha);
            Preferences.Set("codigoConta", conta.Codigo);
            Preferences.Set("isCT", conta.IsCentroTreinamento);
        }

        private async Task<bool> ChecarEmail(string email) {
            return await _usuarioContext.ChecarEmail(email);
        }
    }
}
