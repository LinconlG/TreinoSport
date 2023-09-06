using TreinoSport.Contexts;

namespace TreinoSport.ViewModels {
    public class LoginViewModel {

        private readonly UsuarioContext _usuarioContext;

        public LoginViewModel() {
            _usuarioContext = new();
        }

        public async Task Login(string email, string senha) {
            var conta = await _usuarioContext.Login(email, senha);
            Preferences.Set("codigoConta", conta.Codigo);
            Preferences.Set("isCT", conta.IsCentroTreinamento);
        }
    }
}
