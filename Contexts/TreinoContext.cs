using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Contexts.Base;
using TreinoSport.Models;

namespace TreinoSport.Contexts {
    public class TreinoContext : BaseContext{
        public TreinoContext() { }

        public async Task<IEnumerable<Treino>> GetTreinosAluno(int codigoUsuario) {

        }
    }
}
