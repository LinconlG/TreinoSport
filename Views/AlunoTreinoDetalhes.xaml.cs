using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using TreinoSport.Extensions;
using TreinoSport.Models;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class AlunoTreinoDetalhes : ContentPage
{
	AlunoTreinoViewModel alunoTreinoViewModel;
    private int codigoTreino;
    private int limitePresenca;
	public AlunoTreinoDetalhes(int codigoTreino, List<DiaDaSemana> datasHorarios)
	{
		InitializeComponent();
		this.BindingContext = alunoTreinoViewModel = new();
        alunoTreinoViewModel.datasLista = datasHorarios;
        this.codigoTreino = codigoTreino;
        BuscarTreino(codigoTreino);
	}

	private async void ClickHorario(object sender, EventArgs e) {
        Button btn = (Button)sender;
        btn.IsEnabled = false;
        var codigoDia = int.Parse(btn.ClassId[1].ToString());
        var codigoHorario = int.Parse(btn.ClassId);
        try {
            var codigoAluno = ContaStatic.GetCodigo();
            var alunos = await alunoTreinoViewModel.BuscarPresentes(codigoTreino, codigoDia, codigoHorario);
            var opcoes = VerificarPresenca(codigoAluno, alunos);

            var resultado = await DisplayActionSheet("SELECIONE UMA OPÇÃO", "FECHAR", null, opcoes);
            if (resultado == null) {
                return;
            }
            if (resultado == "LISTA DE PRESENÇA") {
                await this.ShowPopupAsync(new GerenciamentoAulaHorario(codigoTreino, codigoDia, codigoHorario));
                return;
            }
            if (resultado == "MARCAR PRESENÇA") {
                var alunosTemp = await alunoTreinoViewModel.BuscarPresentes(codigoTreino, codigoDia, codigoHorario);
                if (alunosTemp.Count >= limitePresenca) {
                    await DisplayAlert("Erro", "O limite de alunos presentes para este horário já foi atingido.", "OK");
                    return;
                }
                await alunoTreinoViewModel.MarcarPresenca(codigoTreino, codigoDia, codigoHorario, codigoAluno);
                await DisplayAlert("Concluído", "A presença no horário foi confirmada.", "OK");
                return;
            }
            if (resultado == "REMOVER PRESENÇA") {

                await alunoTreinoViewModel.RemoverPresenca(codigoTreino, codigoDia, codigoHorario, codigoAluno);
                await DisplayAlert("Concluído", "A presença no horário foi removida.", "OK");
                return;
            }
        }
        catch (Exception ex) {
            this.HandlerException(ex);
        }
        finally {
            btn.IsEnabled = true;
        }

    }

    private string[] VerificarPresenca(int codigoAluno, List<Conta> alunos) {
        string[] opcoes;

        if (alunos.Any(a => a.Codigo == codigoAluno)) {
            opcoes = new string[] { "LISTA DE PRESENÇA", "REMOVER PRESENÇA" };
        }
        else {
            opcoes = new string[] { "LISTA DE PRESENÇA", "MARCAR PRESENÇA" };
        }
        return opcoes;
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