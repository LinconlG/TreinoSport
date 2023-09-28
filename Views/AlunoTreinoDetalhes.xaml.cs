using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class AlunoTreinoDetalhes : ContentPage
{
	AlunoTreinoViewModel alunoTreinoViewModel;
	public AlunoTreinoDetalhes(int codigoTreino)
	{
		InitializeComponent();
		this.BindingContext = alunoTreinoViewModel = new();
		BuscarTreino(codigoTreino);
	}

	private async void BuscarTreino(int codigoTreino) {
		var treino = await alunoTreinoViewModel.BuscarTreino(codigoTreino);
		_labelTituloTreino.Text = treino.Modalidade.ToString();
		_labelDescricao.Text = treino.Descricao;
		_labelVencimento.Text = treino.DataVencimento.Day.ToString();
		_labelLimite.Text = treino.LimiteAlunos.ToString();
	}
}