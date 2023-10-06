using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using TreinoSport.Models;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class GerenciamentoAulaHorario : Popup
{
	AlunosPopUpViewModel alunosPopUp;
	public GerenciamentoAulaHorario(int codigoTreino, int codigoDia, int codigoHorario)
	{
		InitializeComponent();
		this.BindingContext = alunosPopUp = new();
		alunosPopUp.AtribuirAlunos(codigoTreino, codigoDia, codigoHorario);
	}
}