using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinoSport.Models
{
    public class DiaDaSemanaDTO {
        public string NomeDia { get; set; }
        public DayOfWeek DiaEnum { get; set; }
        public ObservableCollection<TimePicker> Horarios { get; set; }


        public List<DateTime> ConverterDateTime() {
            var horariosDateTime = new List<DateTime>();
            foreach (var horario in Horarios) {
                var aux = new DateTime(horario.Time.Ticks);
                horariosDateTime.Add(aux);
            }
            return horariosDateTime;
        }
    }
}
