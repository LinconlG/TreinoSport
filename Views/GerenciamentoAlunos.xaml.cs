using CommunityToolkit.Maui.Views;
using TreinoSport.Extensions;
using TreinoSport.Models;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class GerenciamentoAlunos : ContentPage
{
	TreinoViewModel treinoViewModel;
	private int codigoTreino;
	public GerenciamentoAlunos(int codigoTreino, List<DiaDaSemana> datas)
	{
		InitializeComponent();
		this.BindingContext = treinoViewModel = new();
        treinoViewModel.dataLista = datas;
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

		try {
            bool resposta = await DisplayAlert("Confirma��o", "Deseja remover este aluno do treino?", "Sim", "N�o");
            if (!resposta) {
                return;
            }

            var codigoAluno = int.Parse(btn.ClassId);
            await treinoViewModel.RemoverAluno(codigoTreino, codigoAluno);
        }
        catch (Exception ex) {
            this.HandlerException(ex);
        }
        finally {
            btn.IsEnabled = true;
        }
    }
	private async void ClickAulaHorario(object sender, EventArgs e) {
		Button btn = sender as Button;
        btn.IsEnabled = false;
        try {
            var codigoDia = int.Parse(btn.ClassId[1].ToString());
            var codigoHorario = int.Parse(btn.ClassId);
            await this.ShowPopupAsync(new GerenciamentoAulaHorario(codigoTreino, codigoDia, codigoHorario));
        }
        catch (Exception ex) {
            this.HandlerException(ex);
        }
        finally {
            btn.IsEnabled = true;
        }
	}
    private async void ClickAdicionarAluno(object sender, EventArgs e) {
        Button btn = sender as Button;
        try {
            btn.IsEnabled = false;

            if (_entryAddAluno.Text == null || _entryAddAluno.Text == "") {
                return;
            }
            if (treinoViewModel.ChecarAluno(_entryAddAluno.Text)) {
                await DisplayAlert("Erro", "Este aluno j� est� inscrito no treino!", "Ok");
                return;
            }

            var emailAluno = _entryAddAluno.Text;
            await treinoViewModel.AdicionarAluno(codigoTreino, emailAluno);
            _entryAddAluno.Text = String.Empty;
        }
		catch (Exception ex) {
            this.HandlerException(ex);
        }
		finally {
			btn.IsEnabled = true;
        }

	}
}