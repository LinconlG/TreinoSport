using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinoSport.Models {
    public class DiaDaSemana {
        public string NomeDia { get; set; }
        public DayOfWeek DiaEnum { get; set; }
        public List<DateTime> Horarios { get; set; }
    }
}
