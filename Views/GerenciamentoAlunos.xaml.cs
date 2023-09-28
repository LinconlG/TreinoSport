using CommunityToolkit.Maui.Views;
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
        Button btn = sender as Button;
        btn.IsEnabled = false;

        bool resposta = await DisplayAlert("Confirmação", "Deseja remover este aluno do treino?", "Sim", "Não");
		if (!resposta) {
			return;
		}

		var codigoAluno = int.Parse(btn.ClassId);
		try {		
            await treinoViewModel.RemoverAluno(codigoTreino, codigoAluno);
        }
        catch (Exception ex) {
            if (TaskExtension.IsPublicMessageCheck(ex)) {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
            else {
                await DisplayAlert("Erro", "Ocorreu um erro!", "Ok");
            }
        }
        finally {
            btn.IsEnabled = true;
        }
    }
	private async void ClickAulaHorario(object sender, EventArgs e) {
		Button btn = sender as Button;
		var codigoDia = int.Parse(btn.ClassId[1].ToString());
		var codigoHorario = int.Parse(btn.ClassId);
        //this.ShowPopup(new GerenciamentoAulaHorario(codigoTreino, codigoDia, codigoHorario));
		//await Navigation.PushAsync(new GerenciamentoAulaHorario(codigoTreino, codigoDia, codigoHorario));
	}
    private async void ClickAdicionarAluno(object sender, EventArgs e) {
        Button btn = sender as Button;
        try {
            btn.IsEnabled = false;

            if (_entryAddAluno.Text == null || _entryAddAluno.Text == "") {
                return;
            }
            if (treinoViewModel.ChecarAluno(_entryAddAluno.Text)) {
                await DisplayAlert("Erro", "Este aluno já está inscrito no treino!", "Ok");
                return;
            }

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
		finally {
			btn.IsEnabled = true;
        }

	}
}