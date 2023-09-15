using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class GerenciamentoAlunos : ContentPage
{
	TreinoViewModel treinoViewModel;
	public GerenciamentoAlunos(int codigoTreino)
	{
		InitializeComponent();
		this.BindingContext = treinoViewModel = new();
		ReceberTreino(codigoTreino);
	}

	private async void ReceberTreino(int codigoTreino) {
		var treino = await treinoViewModel.BuscarTreinoBasico(codigoTreino);
		_labelNomeTreino.Text = treino.Nome;
		_labelListaAlunos.Text = _labelListaAlunos.Text.Replace("X", $"{treino.Alunos.Count}");
    }
}