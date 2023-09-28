using CommunityToolkit.Maui.Views;
using TreinoSport.Extensions;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class AlunoTreinoDetalhes : ContentPage
{
	AlunoTreinoViewModel alunoTreinoViewModel;
    private int codigoTreino;
    private int limitePresenca;
	public AlunoTreinoDetalhes(int codigoTreino)
	{
		InitializeComponent();
		this.BindingContext = alunoTreinoViewModel = new();
        this.codigoTreino = codigoTreino;
        BuscarTreino(codigoTreino);
	}

	private async void ClickHorario(object sender, EventArgs e) {
        Button btn = (Button)sender;
        btn.IsEnabled = false;
        var codigoDia = int.Parse(btn.ClassId[1].ToString());
        var codigoHorario = int.Parse(btn.ClassId);
        var alunos = alunoTreinoViewModel.BuscarAlunosPopUp((DayOfWeek)codigoDia, codigoHorario);
        try {
            var resultado = await DisplayActionSheet("SELECIONE UMA OP��O", "FECHAR", null, "LISTA DE PRESEN�A", "MARCAR PRESEN�A", "REMOVER PRESEN�A");
            if (resultado == null) {
                return;
            }
            if (resultado == "LISTA DE PRESEN�A") {
                await this.ShowPopupAsync(new GerenciamentoAulaHorario(alunos));
                return;
            }
            if (resultado == "MARCAR PRESEN�A") {
                var codigoAluno = Preferences.Get("codigoConta", 0);
                if (alunos.Any(aluno => aluno.Codigo == codigoAluno)) {
                    await DisplayAlert("Erro", "Voc� j� marcou presen�a neste hor�rio.", "OK");
                    return;
                }
                if (alunos.Count >= limitePresenca) {
                    await DisplayAlert("Erro", "O limite de alunos presentes para este hor�rio j� foi atingido.", "OK");
                    return;
                }
                await alunoTreinoViewModel.MarcarPresenca(codigoTreino, codigoDia, codigoHorario, codigoAluno);
                await DisplayAlert("Conclu�do", "A presen�a no hor�rio foi confirmada.", "OK");
                return;
            }
            if (resultado == "REMOVER PRESEN�A") {
                var codigoAluno = Preferences.Get("codigoConta", 0);
                if (!alunos.Any(aluno => aluno.Codigo == codigoAluno)) {
                    await DisplayAlert("Erro", "Voc� n�o possui presen�a neste hor�rio.", "OK");
                    return;
                }
                await alunoTreinoViewModel.RemoverPresenca(codigoTreino, codigoDia, codigoHorario, codigoAluno);
                await DisplayAlert("Conclu�do", "A presen�a no hor�rio foi removida.", "OK");
                return;
            }
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

	private async void BuscarTreino(int codigoTreino) {
		var treino = await alunoTreinoViewModel.BuscarTreino(codigoTreino);
		_labelTituloTreino.Text = treino.Modalidade.ToString();
		_labelDescricao.Text = treino.Descricao;
		_labelVencimento.Text = treino.DataVencimento.Day.ToString();
        limitePresenca = treino.LimiteAlunos;
        _labelLimite.Text = treino.LimiteAlunos.ToString();
	}
}