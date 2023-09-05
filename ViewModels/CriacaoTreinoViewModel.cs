using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinoSport.ViewModels {
    public partial class CriacaoTreinoViewModel : ObservableObject {

        public ObservableCollection<TimePicker> Horarios { get; set; } = new();

        [ObservableProperty]
        private TimePicker horario;


        Dictionary<string, object> controles;

        [ICommand]
        private void AddHorario() {
            //Horarios.Add();
        }

        public void AddHorarioTeste(TimePicker novoHorario) {
            Horarios.Add(novoHorario);
        }

        public void RmvHorarioTeste(TimePicker novoHorario) {
            Horarios.Remove(novoHorario);
        }

        public void OnAppearing(Dictionary<string, object> controles) {
            this.controles = controles;
        }
    }
}
