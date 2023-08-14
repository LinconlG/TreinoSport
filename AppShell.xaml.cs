using Microsoft.Maui.Controls.PlatformConfiguration;

namespace TreinoSport;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
	}

	private async void ClickSair(object sender, EventArgs e) {
		Preferences.Remove("email");
        Preferences.Remove("senha");
		Preferences.Remove("codigoUsuario");
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }
}
