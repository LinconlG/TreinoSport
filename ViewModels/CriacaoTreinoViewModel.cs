using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Models;
using TreinoSport.Models.Enums;
using TreinoSport.Util;

namespace TreinoSport.ViewModels
{
    public partial class CriacaoTreinoViewModel : ObservableObject
    {

        public ObservableCollection<DiaDaSemanaDTO> DatasHorarios { get; set; } = new();


        public void AdicionarHorario(TimePicker novoHorario, string diaString) {
            foreach (var data in DatasHorarios) {
                if (data.DiaEnum.ToString() == diaString) {
                    data.Horarios.Add(novoHorario);
                }
            }
        }

        public void RemoverHorario(string diaString) {
            foreach (var data in DatasHorarios) {
                if (data.DiaEnum.ToString() == diaString && data.Horarios.Count > 0) {
                    data.Horarios.RemoveAt(data.Horarios.Count - 1);
                }
            }
        }

        public bool LimiteHorarios(string diaEnum) {
            var dia = DatasHorarios.First(dia => dia.DiaEnum.ToString() == diaEnum);
            if (dia.Horarios.Count < 8) {
                return true;
            }
            return false;
        }

        public bool DatasExistem() {
            return DatasHorarios.Count > 0;
        }

        public void AdicionarDia(DayOfWeek diaEnum) {
            var diaDaSemana = new DiaDaSemanaDTO();
            diaDaSemana.DiaEnum = diaEnum;
            diaDaSemana.Horarios = new();
            diaDaSemana.NomeDia = Utilidade.TratarDayOfWeek(diaEnum);
            DatasHorarios.Add(diaDaSemana);
        }

        public void RemoverDiaDaSemana(string diaString) {
            foreach (var data in DatasHorarios.ToList()) {
                if (data.DiaEnum.ToString() == diaString) {
                    DatasHorarios.Remove(data);
                }
            }
        }

        public bool DiaDaSemanaJaExiste(int selectedIndex) {
            if (DatasHorarios.Any(data => data.DiaEnum == (DayOfWeek)selectedIndex)) {
                return true;
            }
            return false;
        }



        public void OnAppearing(Dictionary<string, object> controles) {


        }
    }
}
