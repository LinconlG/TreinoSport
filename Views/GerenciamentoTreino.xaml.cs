namespace TreinoSport.Views;

public partial class GerenciamentoTreino : ContentPage
{
	public GerenciamentoTreino()
	{
		InitializeComponent();
		
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

}