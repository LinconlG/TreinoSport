using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using TreinoSport.Models;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class GerenciamentoAulaHorario : Popup
{
	AlunosPopUpViewModel alunosPopUp;
	public GerenciamentoAulaHorario(List<Conta> alunos)
	{
		InitializeComponent();
		this.BindingContext = alunosPopUp = new(alunos);
		alunosPopUp.AtribuirAlunos();
	}
}