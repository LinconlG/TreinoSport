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

        public AlunoTreinoViewModel() {
            DatasHorarios = new();
            treinoContext = new();
        }

        public async Task<Treino> BuscarTreino(int codigoTreino) {
            DatasHorarios.Clear();
            var treino = await treinoContext.GetDetalhesTreino(codigoTreino);
            AddDatasHorarios(treino.DatasTreinos);
            return treino;
        }

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
        private bool TratarIntervaloDias(DayOfWeek diaTreino) {
            var ontem = DateTime.Now.AddDays(-1).DayOfWeek;
            if (diaTreino == ontem) {
                return false;
            }
            return true;
        }
    }
}
