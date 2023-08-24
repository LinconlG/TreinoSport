using Microsoft.Maui.Controls.PlatformConfiguration;

namespace TreinoSport;

public partial class AppShell : Shell
{
	//private static FlyoutItem _flyoutItem;
	public AppShell()
	{
		InitializeComponent();
		//_flyoutItem = PaginaInicialCT;
	}

	private async void ClickSair(object sender, EventArgs e) {
		Preferences.Remove("email");
        Preferences.Remove("senha");
		Preferences.Remove("codigoUsuario");
		Preferences.Remove("isCT");
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
		
    }

/*	public static void VisibilidadePaginaInicialCT(bool isCT) {
		if (isCT) {
            _flyoutItem.IsVisible = true;
		}
		else {
			_flyoutItem.IsVisible = false;
		}
	}*/
}
