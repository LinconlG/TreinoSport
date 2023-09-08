using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinoSport.Models {
    public class DiaDaSemana {
        public DayOfWeek Dia { get; set; }
        public List<DateTime> Horarios { get; set; }

        public DiaDaSemana() { }

        public DiaDaSemana(DiaDaSemanaDTO dto) {
            Dia = dto.DiaEnum;
            Horarios = dto.ConverterDateTime();
        }

    }
}
