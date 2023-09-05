using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Models;
using TreinoSport.Util;

namespace TreinoSport.ViewModels {
    public partial class CriacaoTreinoViewModel : ObservableObject {

        public ObservableCollection<DiaDaSemana> DatasHorarios { get; set; } = new();

        [ICommand]
        private void AddHorario() {
            //Horarios.Add();
        }

/*        public void AdicionarHorario(TimePicker novoHorario) {
            if (Horarios.Count < 8) {
                Horarios.Add(novoHorario);
            }
        }*/

/*        public void RemoverHorario() {
            if (Horarios.Count > 0) {
                Horarios.RemoveAt(Horarios.Count - 1);
            }
        }
*/
/*        public bool LimiteHorarios() {
            if (Horarios.Count < 8) {
                return true;
            }
            return false;
        }*/

        public void AdicionarDia(DayOfWeek diaEnum) {
            var diaDaSemana = new DiaDaSemana();
            diaDaSemana.DiaEnum = diaEnum;
            diaDaSemana.Horarios = new();
            diaDaSemana.NomeDia = Utilidade.TratarDayOfWeek(diaEnum);
            DatasHorarios.Add(diaDaSemana);
        }

        public void OnAppearing(Dictionary<string, object> controles) {


        }
    }
}
