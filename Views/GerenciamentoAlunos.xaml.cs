using TreinoSport.Extensions;
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

    private async void ClickRemoverAluno(object sender, EventArgs e) {
        bool resposta = await DisplayAlert("Confirmação", "Deseja remover este aluno do treino?", "Sim", "Não");
		if (!resposta) {
			return;
		}
        Button btn = sender as Button;
		var codigoAluno = int.Parse(btn.ClassId);
		await treinoViewModel.RemoverAluno(codigoTreino, codigoAluno);
    }

    private async void ClickAdicionarAluno(object sender, EventArgs e) {
		if (_entryAddAluno.Text == null || _entryAddAluno.Text == "") {
			return;
		}
		try {
            var emailAluno = _entryAddAluno.Text;
            await treinoViewModel.AdicionarAluno(codigoTreino, emailAluno);
            _entryAddAluno.Text = String.Empty;
        }
		catch (Exception ex) {
			if (TaskExtension.IsPublicMessageCheck(ex)) {
				await DisplayAlert("Erro",ex.Message, "Ok");
			}
			else {
                await DisplayAlert("Erro", "Ocorreu um erro!", "Ok");
            }
        }

	}
}