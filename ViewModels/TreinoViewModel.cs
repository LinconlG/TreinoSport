using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Contexts;
using TreinoSport.Models;

namespace TreinoSport.ViewModels {
    public partial class TreinoViewModel : ObservableObject {

        private TreinoContext _treinoContext;
        public ObservableCollection<Treino> Treinos { get; set; }
        [ObservableProperty]
        private Treino treino;
        [ObservableProperty]
        private bool _isBusy;

        public TreinoViewModel() {
            _treinoContext = new();
            Treinos = new();
        }

        [ICommand]
        private async Task GetTreinosComoAluno() {
            try {
                IsBusy = true;
                Treinos.Clear();
                var lista = await _treinoContext.GetTreinosComoAluno(Preferences.Get("codigoConta", 0));
                foreach (var item in lista) {
                    Treinos.Add(item);
                }
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            finally {
                IsBusy = false;
            }

        }

        public void OnAppearing() {
            IsBusy = true;
        }
    }
}
