using TreinoSport.ViewModels;

namespace TreinoSport.XMLPages;

public partial class PaginaInicialCT : ContentPage
{
    TreinoViewModel treinoViewModel;
    public PaginaInicialCT()
	{
		InitializeComponent();
        this.BindingContext = treinoViewModel = new TreinoViewModel();
    }
    protected override void OnAppearing() {
        base.OnAppearing();
        treinoViewModel.OnAppearing(refreshLista, avisoTreinosVazio);
    }
}