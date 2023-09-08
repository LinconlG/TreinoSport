using System;
using System.Collections.ObjectModel;
using TreinoSport.Models;
using TreinoSport.Models.Enums;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class CriacaoTreino : ContentPage {
    private CriacaoTreinoViewModel criacaoTreinoViewModel;
    private TimePicker _horarioMaisRecente;
    private bool flagEditar;
    private int? codigoTreino;
    private IEnumerable<string> treinosExistentes;

    public CriacaoTreino(bool? flagEditar = null, int? codigoTreino = null, IEnumerable<string> treinosExistentes = null) {
        InitializeComponent();
        this.BindingContext = criacaoTreinoViewModel = new();
        this.flagEditar = flagEditar is null ? false : flagEditar.Value;
        this.codigoTreino = codigoTreino is null ? null : codigoTreino.Value;
        this.treinosExistentes = treinosExistentes;
        BuscarDetalhesTreino(this.flagEditar, codigoTreino);
    }

    private void ClickAddHorario(object sender, EventArgs e) {
        Button button = sender as Button;
        var diaString = button.ClassId;
        if (!criacaoTreinoViewModel.LimiteHorarios(diaString)) {
            return;
        }

        _horarioMaisRecente = new TimePicker();

        criacaoTreinoViewModel.AdicionarHorario(_horarioMaisRecente, diaString);
    }

    private async void ClickConfirmarHorarios(object sender, EventArgs e) {
        if (CheckCampos()) {
            return;
        }
        try {
            var treino = AtribuirTreino();
            if (flagEditar) {
                treino.Codigo = codigoTreino.Value;
                await criacaoTreinoViewModel.AtualizarDetalhesTreino(treino);
            }
            else {
                await criacaoTreinoViewModel.CriarTreino(treino);
            }

            await DisplayAlert("Tudo certo", "Os detalhes do treino foram salvos com sucesso!", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    private void ClickRmvHorario(object sender, EventArgs e) {
        Button button = sender as Button;
        var diaString = button.ClassId;
        criacaoTreinoViewModel.RemoverHorario(diaString);
    }

    private void ClickRmvDia(object sender, EventArgs e) {
        Button button = sender as Button;
        var diaString = button.ClassId;
        criacaoTreinoViewModel.RemoverDiaDaSemana(diaString);
    }

    private async void PickerModalidadeChanged(object sender, EventArgs e) {
        if (flagEditar) {
            return;
        }
        Picker pickerModalidade = (Picker)sender;
        var index = pickerModalidade.SelectedIndex;
        if (CheckTreinosExistentes(index)) {
            await DisplayAlert("Alerta", "Já existe um treino dessa modalidade.", "OK");
            pickerModalidade.SelectedItem = null;
        }
    }

    private void PickerDiaDaSemanaChanged(object sender, EventArgs e) {
        Picker pickerDiaDaSemana = (Picker)sender;
        var index = pickerDiaDaSemana.SelectedIndex;
        if (pickerDiaDaSemana.SelectedItem is null || DiaDaSemanaJaExiste(index)) {
            pickerDiaDaSemana.SelectedItem = null;
            return;
        }
        criacaoTreinoViewModel.AdicionarDia((DayOfWeek)index);
        pickerDiaDaSemana.SelectedItem = null;
    }

    private bool CheckTreinosExistentes(int index) {
        var modalidade = (ModalidadeTreino)index;
        return treinosExistentes.Contains(modalidade.ToString());
    }

    private bool DiaDaSemanaJaExiste(int selectedIndex) {
        return criacaoTreinoViewModel.DiaDaSemanaJaExiste(selectedIndex);
    }

    private bool CheckCampos() {
        if (_pickerModalidade.SelectedItem is null) {
            _labelAvisoModalidade.IsVisible = true;
            return true;
        }
        if (!criacaoTreinoViewModel.DatasExistem()) {
            _labelAvisoDiaDaSemana.IsVisible = true;
            return true;
        }
        _labelAvisoModalidade.IsVisible = false;
        _labelAvisoDiaDaSemana.IsVisible = false;
        return false;
    }

    private Treino AtribuirTreino() {
        var treino = new Treino();
        treino.Descricao = _editorDescricao.Text;
        treino.Criador = new();
        treino.Criador.Codigo = Preferences.Get("codigoConta", 0);
        if (treino.Criador.Codigo == 0) {
            throw new Exception("Preferencia de conta não foi salva");
        }
        treino.Modalidade = (ModalidadeTreino)_pickerModalidade.SelectedIndex;
        treino.Nome = treino.Modalidade.ToString();
        treino.DataVencimento = _datePickerVencimento.Date;
        return treino;
    }

    private async void BuscarDetalhesTreino(bool flagEditar, int? codigoTreino) {
        if (!flagEditar) {
            return;
        }
        try {
            var treino = await criacaoTreinoViewModel.BuscarDetalhesTreino(codigoTreino.Value);
            _pickerModalidade.SelectedIndex = (int)treino.Modalidade;
            _editorDescricao.Text = treino.Descricao;
            _datePickerVencimento.Date = treino.DataVencimento;
        }
        catch (Exception) {
            await DisplayAlert("Erro", "Ocorreu um erro", "OK");
            await Navigation.PopAsync();
        }
    }

    protected override void OnAppearing() {
        base.OnAppearing();
    }
}