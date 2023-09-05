using System.Collections.ObjectModel;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class CriacaoTreino : ContentPage
{
    CriacaoTreinoViewModel _criacaoTreinoViewModel;
    private TimePicker _horarioMaisRecente;

    public CriacaoTreino(CriacaoTreinoViewModel criacaoTreinoViewModel)
	{
		InitializeComponent();
        this.BindingContext = _criacaoTreinoViewModel = criacaoTreinoViewModel;
	}

	private void ClickAddHorario(object sender, EventArgs e) {

        if (!_criacaoTreinoViewModel.LimiteHorarios()) {
            return;
        }

        _horarioMaisRecente = new TimePicker { Format = "HH:mm" };

        _criacaoTreinoViewModel.AdicionarHorario(_horarioMaisRecente);
    }

    private void ClickRmvHorario(object sender, EventArgs e) {
        _criacaoTreinoViewModel.RemoverHorario();
    }

    protected override void OnAppearing() {
        base.OnAppearing();
    }
}