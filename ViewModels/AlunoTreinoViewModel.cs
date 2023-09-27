using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Models;

namespace TreinoSport.ViewModels {
    public class AlunoTreinoViewModel : ObservableObject {

        public ObservableCollection<DiaDaSemanaDTO> DatasHorarios { get; set; }


    }
}
