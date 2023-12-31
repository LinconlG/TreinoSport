﻿using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TreinoSport.Contexts;
using TreinoSport.Models;

namespace TreinoSport.ViewModels {
    public partial class TreinoViewModel : ObservableObject {

        RefreshView refreshLista;
        Grid avisoTreinoVazio;


        private TreinoContext treinoContext;
        public ObservableCollection<Treino> Treinos { get; set; }
        public ObservableCollection<Conta> Alunos { get; set; }
        public ObservableCollection<DiaDaSemanaDTO> DatasHorarios { get; set; }
        public List<DiaDaSemana> dataLista { get; set; }

        [ObservableProperty]
        private Treino treino;
        [ObservableProperty]
        private bool _isBusy;

        public TreinoViewModel() {
            treinoContext = new();
            Treinos = new();
            Alunos = new();
            DatasHorarios = new();
        }

        [ICommand]
        private async Task GetTreinosComoAluno() {
            try {
                IsBusy = true;
                Treinos.Clear();
                var lista = await treinoContext.GetTreinosComoAluno(ContaStatic.GetCodigo());
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
                var lista = await treinoContext.GetTreinosComoCT(ContaStatic.GetCodigo());
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
        private async Task GetTreinosComCores() {
            try {
                IsBusy = true;
                Treinos.Clear();
                var codigoConta = ContaStatic.GetCodigo();
                var isCT = ContaStatic.GetIsCT();
                var lista = await treinoContext.GetTreinosComCores(codigoConta, isCT);
                ChecarTreinos(lista);
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
        public List<Conta> BuscarAlunosPopUp(DayOfWeek dia, int codigoHorario) {
            foreach (var data in DatasHorarios) {

                if (data.DiaEnum == dia) {

                    foreach (var horario in data.Horarios) {

                        if (horario.Codigo == codigoHorario) {
                            return horario.AlunosPresentes;
                        }
                    }
                }
            }
            return new List<Conta>();
        }

        private void AtribuirBordas(Treino treino) {
            var hoje = DateTime.Now.DayOfWeek;
            foreach (var dia in treino.DatasTreinos) {
                if (dia.Dia == hoje) {
                    treino.Cor = new Color(15, 166, 10);
                    break;
                }
                treino.Cor = new Color(196, 196, 194);
            }
        }

        public async Task<Treino> BuscarTreinoBasico(int codigoTreino) {
            Treino = await treinoContext.GetDetalhesTreinoBasico(codigoTreino);
            Treino.Alunos = await treinoContext.GetAlunos(codigoTreino);
            Alunos.Clear();
            AddAluno(Treino.Alunos);
            AddDatas(dataLista);
            return Treino;
        }

        private void AddAluno(List<Conta> alunos) {

            if (!alunos.Any()) {
                return;
            }
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

        public async Task RemoverAluno(int codigoTreino, int codigoAluno) {
            await treinoContext.DeleteAluno(codigoTreino, codigoAluno);
            foreach (var aluno in Alunos.ToList()) {
                if (aluno.Codigo == codigoAluno) {
                    Alunos.Remove(aluno);
                }
            }
        }

        public bool ChecarAluno(string emailAluno) {
            if (Alunos.Any(al => al.Email == emailAluno)) {
                return true;
            }
            return false;
        }

        public async Task AdicionarAluno(int codigoTreino, string emailAluno) {
            var aluno = await treinoContext.PutAluno(codigoTreino, emailAluno);
            AddAluno(new List<Conta> { aluno });
        }

        public void OnAppearing(RefreshView refreshView = null, Grid avisoTreinoVazio = null) {
            IsBusy = true;
            refreshLista = refreshView;
            this.avisoTreinoVazio = avisoTreinoVazio;
        }

    }
}
