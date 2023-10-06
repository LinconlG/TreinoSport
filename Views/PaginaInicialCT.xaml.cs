using TreinoSport.Extensions;
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

    private async void ClickedBtnEditarTreino(object sender, EventArgs e) {
        var btn = (Button)sender;
        btn.IsEnabled = false;
        var codigoTreino = int.Parse(btn.ClassId);
        try {
            await Navigation.PushAsync(new CriacaoTreino(true, codigoTreino));
        }
        catch (Exception ex) {
            this.HandlerException(ex);
        }
        finally {
            btn.IsEnabled = true;
        }

    }
    private async void ClickAdicionar(object sender, EventArgs e) {
        await Navigation.PushAsync(new CriacaoTreino(treinosExistentes: treinoViewModel.Treinos.Select(treino => treino.Nome)));
    }
}