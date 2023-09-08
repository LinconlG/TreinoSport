using CommunityToolkit.Maui.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Extensions;
using TreinoSport.Util;

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

        public DiaDaSemanaDTO() {}

        public DiaDaSemanaDTO(DiaDaSemana diaDaSemana) { 
            NomeDia = Utilidade.TratarDayOfWeek(diaDaSemana.Dia);
            DiaEnum = diaDaSemana.Dia;
            Horarios = diaDaSemana.Horarios.ConvertAll(h => h.ToTimePicker()).ToObservableCollection();
        }
    }
}
