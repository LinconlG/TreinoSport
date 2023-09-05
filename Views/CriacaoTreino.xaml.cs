using System.Collections.ObjectModel;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class CriacaoTreino : ContentPage
{
    CriacaoTreinoViewModel _criacaoTreinoViewModel;
    private TimePicker _novoHorario;

	public CriacaoTreino(CriacaoTreinoViewModel criacaoTreinoViewModel)
	{
		InitializeComponent();
        this.BindingContext = _criacaoTreinoViewModel = criacaoTreinoViewModel;
	}

	private void ClickAddHorario(object sender, EventArgs e) {
        _novoHorario = new TimePicker {
            Format = "HH:mm"
        };

        //por um limite de add

        _criacaoTreinoViewModel.AddHorarioTeste(_novoHorario);
    }

    private void ClickRmvHorario(object sender, EventArgs e) {

        _criacaoTreinoViewModel.RmvHorarioTeste(_novoHorario);
    }

    protected override void OnAppearing() {
        base.OnAppearing();
    }
}