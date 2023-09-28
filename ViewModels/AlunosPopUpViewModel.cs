using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Models;

namespace TreinoSport.ViewModels {
    public class AlunosPopUpViewModel : ObservableObject {
        public ObservableCollection<Conta> Alunos { get; set; }
        private List<Conta> alunosLista { get; set; }

        public AlunosPopUpViewModel(List<Conta> alunos) {
            Alunos = new();
            alunosLista = alunos;
        }

        public void AtribuirAlunos() {
            Alunos.Clear();
            foreach (var aluno in alunosLista) {
                Alunos.Add(aluno);
            }
        }

    }
}
