using System.Collections.ObjectModel;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class CriacaoTreino : ContentPage
{
    private CriacaoTreinoViewModel criacaoTreinoViewModel;
    private TimePicker _horarioMaisRecente;
    private ToolbarItem itemVoltar;

    public CriacaoTreino()
	{
		InitializeComponent();
        this.BindingContext = criacaoTreinoViewModel = new();
        //itemVoltar.Clicked += LimparCampos;

    }

/*	private void ClickAddHorario(object sender, EventArgs e) {

        if (!_criacaoTreinoViewModel.LimiteHorarios()) {
            return;
        }

        _horarioMaisRecente = new TimePicker { Format = "HH:mm" };

        _criacaoTreinoViewModel.AdicionarHorario(_horarioMaisRecente);
    }

    private void ClickRmvHorario(object sender, EventArgs e) {
        _criacaoTreinoViewModel.RemoverHorario();
    }*/

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
        criacaoTreinoViewModel.AdicionarDia((DayOfWeek)pickerDiaDaSemana.SelectedIndex);
        pickerDiaDaSemana.SelectedItem = null;
    }
}