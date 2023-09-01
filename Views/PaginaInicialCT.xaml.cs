using TreinoSport.ViewModels;

namespace TreinoSport.XMLPages;

public partial class PaginaInicialCT : ContentPage
{
    TreinoViewModel _treinoViewModel;
    public PaginaInicialCT(TreinoViewModel treinoViewModel)
	{
		InitializeComponent();
        this.BindingContext = _treinoViewModel = treinoViewModel;
    }
    protected override void OnAppearing() {
        base.OnAppearing();
        _treinoViewModel.OnAppearing(refreshLista, avisoTreinosVazio);
    }

    private void ClickedBtnGerenciarTreino(object sender, EventArgs e) {
        var btn = (Button)sender;
        var codigoTreino = int.Parse(btn.Text);

    }
    private async void ClickAdicionar(object sender, EventArgs e) {
        await Navigation.PushAsync(new CriacaoTreino());
    }
}