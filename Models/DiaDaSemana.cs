using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinoSport.Models
{
    public class DiaDaSemana {
        public string NomeDia { get; set; }
        public DayOfWeek DiaEnum { get; set; }
        public ObservableCollection<TimePicker> Horarios { get; set; }
    }
}
