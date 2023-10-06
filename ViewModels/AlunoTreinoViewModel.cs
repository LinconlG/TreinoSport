using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Contexts;
using TreinoSport.Models;

namespace TreinoSport.ViewModels {
    public class AlunoTreinoViewModel : ObservableObject {

        TreinoContext treinoContext;
        public ObservableCollection<DiaDaSemanaDTO> DatasHorarios { get; set; }
        public List<DiaDaSemana> datasLista { get; set; }

        public AlunoTreinoViewModel() {
            DatasHorarios = new();
            treinoContext = new();
        }

        public async Task<Treino> BuscarTreino(int codigoTreino) {
            DatasHorarios.Clear();
            var treino = await treinoContext.GetDetalhesTreinoBasico(codigoTreino);
            AddDatasHorarios(datasLista);
            return treino;
        }
/*        private void AtribuirAlunos(DayOfWeek dia, int codigoHorario) {
            Alunos.Clear();
            foreach (var data in DatasHorarios) {

                if (data.DiaEnum == dia) {

                    foreach (var horario in data.Horarios) {

                        if (horario.Codigo == codigoHorario) {

                            foreach (var alunoPresente in horario.AlunosPresentes) {
                                Alunos.Add(alunoPresente);
                            }
                        }
                    }
                }
            }
        }*/
        private void AddDatasHorarios(List<DiaDaSemana> diasDaSemana) {
            if (!diasDaSemana.Any()) {
                return;
            }

            foreach (var dia in diasDaSemana) {
                if (TratarIntervaloDias(dia.Dia)) {
                    DatasHorarios.Add(new DiaDaSemanaDTO(dia));
                }
            }
        }
        public Task<List<Conta>> BuscarPresentes(int codigoTreino, int codigoDia, int codigoHorario) {
            return treinoContext.GetAlunosPresentes(codigoTreino, codigoDia, codigoHorario);
        }
        private bool TratarIntervaloDias(DayOfWeek diaTreino) {
            var ontem = DateTime.Now.AddDays(-1).DayOfWeek;
            if (diaTreino == ontem) {
                return false;
            }
            return true;
        }
        public async Task MarcarPresenca(int codigoTreino, int codigoDia, int codigoHorario, int codigoAluno) {
            var datasDTO = DatasHorarios.ToList();
            var datas = datasDTO.ConvertAll(d => d.Conversao());
            await treinoContext.PatchInserirAlunoHorario(codigoTreino, codigoDia, codigoHorario, codigoAluno, datas);
            await BuscarTreino(codigoTreino);
        }
        public async Task RemoverPresenca(int codigoTreino, int codigoDia, int codigoHorario, int codigoAluno) {
            var datasDTO = DatasHorarios.ToList();
            var datas = datasDTO.ConvertAll(d => d.Conversao());
            await treinoContext.PatchDeletarAlunoHorario(codigoTreino, codigoDia, codigoHorario, codigoAluno, datas);
            await BuscarTreino(codigoTreino);
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
    }
}
