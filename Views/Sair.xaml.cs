namespace TreinoSport.Views;

public partial class Sair : ContentPage
{
	public Sair()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing() {
        base.OnAppearing();
        Preferences.Remove("email");
        Preferences.Remove("senha");
        Preferences.Remove("codigoConta");
        Preferences.Remove("isCT");
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }
}