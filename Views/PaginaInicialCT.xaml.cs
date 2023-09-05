using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class PaginaInicialCT : ContentPage
{
    TreinoViewModel _treinoViewModel;
    CriacaoTreino _criacaoTreino;
    public PaginaInicialCT(TreinoViewModel treinoViewModel, CriacaoTreino criacaoTreino)
	{
		InitializeComponent();
        this.BindingContext = _treinoViewModel = treinoViewModel;
        _criacaoTreino = criacaoTreino;
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
        await Navigation.PushAsync(_criacaoTreino);
    }
}