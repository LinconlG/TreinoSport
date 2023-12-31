using TreinoSport.Extensions;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class GerenciamentoTreino : ContentPage
{
    TreinoViewModel treinoViewModel;
	public GerenciamentoTreino()
	{
		InitializeComponent();
        this.BindingContext = treinoViewModel = new();
	}

	private async void ClickGerenciarTreino(object sender, EventArgs e) {
        var btn = (Button)sender;
        var codigoTreino = int.Parse(btn.ClassId);
        var datas = treinoViewModel.Treinos.Where(t => t.Codigo == codigoTreino).Select(t => t.DatasTreinos).First();
        try {
            await Navigation.PushAsync(new GerenciamentoAlunos(codigoTreino, datas));
        }
        catch (Exception ex) {
            this.HandlerException(ex);
        }
    }

    protected override void OnAppearing() {
        base.OnAppearing();
        treinoViewModel.OnAppearing();
    }

}