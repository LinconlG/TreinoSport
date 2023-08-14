using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreinoSport.Contexts;
using TreinoSport.Models;

namespace TreinoSport.Services.TreinoService {
    public class TreinoService : ObservableObject {
        private TreinoContext _treinoContext;
        public TreinoService() {
            _treinoContext = new();
        }
        public async Task<IEnumerable<Treino>> GetTreinosAluno(int codigoUsuario) {
            return await _treinoContext.GetTreinosAluno(codigoUsuario);
        }
    }
}
