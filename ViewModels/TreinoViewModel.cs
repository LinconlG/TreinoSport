using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Contexts;
using TreinoSport.Models;

namespace TreinoSport.ViewModels {
    public partial class TreinoViewModel : ObservableObject {

        RefreshView refreshLista;
        Grid avisoTreinoVazio;


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
                ChecarTreinos(lista);
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

        [ICommand]
        private async Task GetTreinosComoCT() {
            try {
                IsBusy = true;
                Treinos.Clear();
                var lista = await _treinoContext.GetTreinosComoCT(Preferences.Get("codigoConta", 0));
                ChecarTreinos(lista);
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

        private void ChecarTreinos(IEnumerable<Treino> treinos) {
            if (refreshLista is null || avisoTreinoVazio is null) {
                return;
            }
            if (!treinos.Any()) {
                refreshLista.IsVisible = false;
                avisoTreinoVazio.IsVisible = true;
            }
            else {
                refreshLista.IsVisible = true;
                avisoTreinoVazio.IsVisible = false;
            }
        }
        public void OnAppearing(RefreshView refreshView = null, Grid avisoTreinoVazio = null) {
            IsBusy = true;
            refreshLista = refreshView;
            this.avisoTreinoVazio = avisoTreinoVazio;
        }

    }
}
