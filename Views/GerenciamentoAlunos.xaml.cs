using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class GerenciamentoAlunos : ContentPage
{
	TreinoViewModel treinoViewModel;
	private int codigoTreino;
	public GerenciamentoAlunos(int codigoTreino)
	{
		InitializeComponent();
		this.BindingContext = treinoViewModel = new();
		ReceberTreino(codigoTreino);
		this.codigoTreino = codigoTreino;
	}

	private async void ReceberTreino(int codigoTreino) {
		var treino = await treinoViewModel.BuscarTreinoBasico(codigoTreino);
		_labelNomeTreino.Text = treino.Nome;
		_labelListaAlunos.Text = _labelListaAlunos.Text.Replace("X", $"{treino.Alunos.Count}");
    }

	private async void ClickAdicionarAluno(object sender, EventArgs e) {
		var emailAluno = _entryAddAluno.Text;
		await treinoViewModel.AdicionarAluno(codigoTreino, emailAluno);
	}
}