using Microsoft.Maui.Controls.PlatformConfiguration;

namespace TreinoSport;

public partial class AppShell : Shell
{
	private static FlyoutItem _paginaCT;
	public AppShell()
	{
		InitializeComponent();
		_paginaCT = PaginaInicialCT;
	}

	private async void ClickSair(object sender, EventArgs e) {
		Preferences.Remove("email");
        Preferences.Remove("senha");
		Preferences.Remove("codigoConta");
		Preferences.Remove("isCT");
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
		
    }

	public static void VisibilidadePaginaInicialCT(bool isCT) {
		if (isCT) {
            _paginaCT.IsVisible = true;
		}
		else {
			_paginaCT.IsVisible = false;
		}
	}
}
