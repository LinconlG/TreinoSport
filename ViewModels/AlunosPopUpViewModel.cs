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
    public class AlunosPopUpViewModel : ObservableObject {
        public ObservableCollection<Conta> Alunos { get; set; }

        private TreinoContext treinoContext;

        public AlunosPopUpViewModel() {
            Alunos = new();
            treinoContext = new();
        }

        public async void AtribuirAlunos(int codigoTreino, int codigoDia, int codigoHorario) {
            Alunos.Clear();
            var presentes = await treinoContext.GetAlunosPresentes(codigoTreino, codigoDia, codigoHorario);
            foreach (var aluno in presentes) {
                Alunos.Add(aluno);
            }
        }

    }
}
