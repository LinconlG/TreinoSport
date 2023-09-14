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
        public ObservableCollection<TimePicker> HorariosPicker { get; set; }
        public List<Horario> Horarios { get; set; }


        public List<Horario> ConverterHorario() {
            var horariosDateTime = new List<Horario>();

            for (int i = 0; i < HorariosPicker.Count; i++) {
                var aux = new Horario() { Codigo = i+1, AlunosPresentes = new(), Hora = new(HorariosPicker[i].Time.Ticks) };
                horariosDateTime.Add(aux);
            }
/*            foreach (var horarioPicker in HorariosPicker) {
                var aux = new Horario() { AlunosPresentes = new(), Hora = new(horarioPicker.Time.Ticks) };
                horariosDateTime.Add(aux);
            }*/
            return horariosDateTime;
        }

        public DiaDaSemanaDTO() {}
            
        public DiaDaSemanaDTO(DiaDaSemana diaDaSemana) { 
            NomeDia = Utilidade.TratarDayOfWeek(diaDaSemana.Dia);
            DiaEnum = diaDaSemana.Dia;
            HorariosPicker = diaDaSemana.Horarios.ConvertAll(h => h.Hora.ToTimePicker()).ToObservableCollection();
        }
    }
}
