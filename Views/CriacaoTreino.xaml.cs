using System.Collections.ObjectModel;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class CriacaoTreino : ContentPage
{
    private CriacaoTreinoViewModel criacaoTreinoViewModel;
    private TimePicker _horarioMaisRecente;
    private bool? flagEditar;

    public CriacaoTreino(bool? flagEditar = null) {
        InitializeComponent();
        this.BindingContext = criacaoTreinoViewModel = new();
        this.flagEditar = flagEditar is null ? false : flagEditar.Value;
        //chamar metodo que faz o Get, onde o parametro é a flag
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

    private void ClickConfirmarHorarios(object sender, EventArgs e) {
        if (CheckCampos()) {
            return;
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

    private bool DiaDaSemanaJaExiste(int selectedIndex) {
        return criacaoTreinoViewModel.DiaDaSemanaJaExiste(selectedIndex);
    }

    private bool CheckCampos() {
        if (_pickerModalidade.SelectedItem is null) {
            _labelAvisoModalidade.IsVisible = true;
            return true;
        }
        if (!criacaoTreinoViewModel.DatasExistem()) {
            _labelAvisoModalidade.IsVisible = true;
            return true;
        }
        _labelAvisoModalidade.IsVisible = false;
        _labelAvisoModalidade.IsVisible = false;
        return false;
    }

    protected override void OnAppearing() {
        base.OnAppearing();
    }
}