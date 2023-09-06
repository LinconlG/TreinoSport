using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class PaginaInicialCT : ContentPage
{
    private TreinoViewModel treinoViewModel;
    public PaginaInicialCT()
	{
		InitializeComponent();
        this.BindingContext = treinoViewModel = new();
    }
    protected override void OnAppearing() {
        base.OnAppearing();
        treinoViewModel.OnAppearing(refreshLista, avisoTreinosVazio);
    }

    private void ClickedBtnGerenciarTreino(object sender, EventArgs e) {
        var btn = (Button)sender;
        var codigoTreino = int.Parse(btn.Text);

    }
    private async void ClickAdicionar(object sender, EventArgs e) {
        await Navigation.PushAsync(new CriacaoTreino());
    }
}