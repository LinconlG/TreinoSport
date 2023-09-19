using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TreinoSport.Contexts;
using TreinoSport.Models;

namespace TreinoSport.ViewModels {
    public partial class TreinoViewModel : ObservableObject {

        RefreshView refreshLista;
        Grid avisoTreinoVazio;


        private TreinoContext _treinoContext;
        public ObservableCollection<Treino> Treinos { get; set; }
        public ObservableCollection<Conta> Alunos { get; set; }
        public ObservableCollection<DiaDaSemanaDTO> DatasHorarios { get; set; }

        [ObservableProperty]
        private Treino treino;
        [ObservableProperty]
        private Treino conta;
        [ObservableProperty]
        private bool _isBusy;

        public TreinoViewModel() {
            _treinoContext = new();
            Treinos = new();
            Alunos = new();
            DatasHorarios = new();
        }

        [ICommand]
        private async Task GetTreinosComoAluno() {
            try {
                IsBusy = true;
                Treinos.Clear();
                var lista = await _treinoContext.GetTreinosComoAluno(Preferences.Get("codigoConta", 0));
                ChecarTreinos(lista);
                foreach (var treino in lista) {
                    Treinos.Add(treino);
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

        [ICommand]
        private async Task GetTreinosParaGerenciar() {
            try {
                IsBusy = true;
                Treinos.Clear();
                var lista = await _treinoContext.GetTreinosParaGerenciar(Preferences.Get("codigoConta", 0));
                foreach (var treino in lista) {
                    AtribuirBordas(treino);
                    Treinos.Add(treino);
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

        private void AtribuirBordas(Treino treino) {
            var hoje = DateTime.Now.DayOfWeek;
            foreach (var dia in treino.DatasTreinos) {
                if (dia.Dia == hoje) {
                    treino.Cor = new Color(15, 166, 10);
                    break;
                }
                treino.Cor = new Color(171, 31, 31);
            }
        }

        public async Task<Treino> BuscarTreinoBasico(int codigoTreino) {
            Treino = await _treinoContext.GetTreinoBasico(codigoTreino);
            Treino.Alunos = await _treinoContext.GetAlunos(codigoTreino);
            AddAluno(Treino.Alunos);
            AddDatas(Treino.DatasTreinos);
            return Treino;
        }

        private void AddAluno(List<Conta> alunos) {

            if (!alunos.Any()) {
                return;
            }
            Alunos.Clear();
            foreach (var aluno in alunos) {
                Alunos.Add(aluno);
            }
        }

        private void AddDatas(List<DiaDaSemana> datas) {

            if (!datas.Any()) {
                return;
            }
            DatasHorarios.Clear();
            foreach (var data in datas) {
                DatasHorarios.Add(new DiaDaSemanaDTO(data));
            }
        }

        public void AdicionarAluno(int codigoTreino, string emailAluno) {
            //meotod de add
            //ad na lista
        }

        public void OnAppearing(RefreshView refreshView = null, Grid avisoTreinoVazio = null) {
            IsBusy = true;
            refreshLista = refreshView;
            this.avisoTreinoVazio = avisoTreinoVazio;
        }

    }
}
