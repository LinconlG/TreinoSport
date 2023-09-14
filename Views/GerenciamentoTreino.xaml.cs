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
        try {
            await Navigation.PushAsync(new GerenciamentoAlunos(codigoTreino));
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    protected override void OnAppearing() {
        base.OnAppearing();
        treinoViewModel.OnAppearing();
    }

}