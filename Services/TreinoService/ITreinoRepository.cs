using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Models;

namespace TreinoSport.Services.TreinoService {
    public interface ITreinoRepository {
        Task<IEnumerable<Treino>> GetTreinoAsync();
    }
}
