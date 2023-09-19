using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinoSport.Models {
    public class Horario {
        public int Codigo { get; set; }
        public DateTime Hora { get; set; }
        public string HoraString { get; set; }
        public List<Conta> AlunosPresentes { get; set; }
    }
}
