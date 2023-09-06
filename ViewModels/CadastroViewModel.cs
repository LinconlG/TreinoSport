using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Contexts;
using TreinoSport.Models;

namespace TreinoSport.ViewModels {
    public partial class CadastroViewModel {

        private readonly UsuarioContext _usuarioContext;

        public CadastroViewModel() {
            _usuarioContext = new();
        }

        public async Task CadastrarUsuario(Conta usuario) {
            var emailExiste = await _usuarioContext.CadastrarUsuario(usuario);
            if (emailExiste) {
                throw new Exception("@O email inserido já existe.@");
            }
        }


    }
}
