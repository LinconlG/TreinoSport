using System.Collections.ObjectModel;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class CriacaoTreino : ContentPage
{
    private CriacaoTreinoViewModel criacaoTreinoViewModel;
    private TimePicker _horarioMaisRecente;

    public CriacaoTreino() {
        InitializeComponent();
        this.BindingContext = criacaoTreinoViewModel = new();
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

    private void ClickRmvHorario(object sender, EventArgs e) {
        //criacaoTreinoViewModel.RemoverHorario();
    }

    protected override void OnAppearing() {
        base.OnAppearing();
    }

    private void LimparCampos(object sender, EventArgs e) {
        _pickerModalidade.SelectedItem = null;
        _editorDescricao.Text = string.Empty;
        _pickerDiaDaSemana.SelectedItem = null;
        criacaoTreinoViewModel.DatasHorarios.Clear();
    }

    private void PickerDiaDaSemanaChanged(object sender, EventArgs e) {
        Picker pickerDiaDaSemana = (Picker)sender;
        if (pickerDiaDaSemana.SelectedItem is null) {
            return;
        }
        criacaoTreinoViewModel.AdicionarDia((DayOfWeek)pickerDiaDaSemana.SelectedIndex);
        pickerDiaDaSemana.SelectedItem = null;
    }
}