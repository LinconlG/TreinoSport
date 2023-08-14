using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreinoSport.Contexts;
using TreinoSport.Models;

namespace TreinoSport.Services.TreinoService {
    public class TreinoService : ITreinoRepository {
        private TreinoContext _treinoContext;
        public TreinoService() {
            _treinoContext = new();
        }
        public Task<IEnumerable<Treino>> GetTreinoAsync() {
            throw new NotImplementedException();
        }
    }
}
