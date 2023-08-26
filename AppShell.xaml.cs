using Microsoft.Maui.Controls.PlatformConfiguration;

namespace TreinoSport;

public partial class AppShell : Shell
{
	private static FlyoutItem _paginaCT;
    private static FlyoutItem _paginaAluno;
    public AppShell()
	{
		InitializeComponent();
		_paginaCT = PaginaInicialCT;
		_paginaAluno = PaginaInicialAluno;
    }

	private async void ClickSair(object sender, EventArgs e) {
		Preferences.Remove("email");
        Preferences.Remove("senha");
		Preferences.Remove("codigoConta");
		Preferences.Remove("isCT");
		
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }

	public static void VisibilidadeFlyoutCT(bool isCT) {
		if (isCT) {
            _paginaCT.IsVisible = true;
			_paginaAluno.IsVisible = false;
		}
		else {
			_paginaCT.IsVisible = false;
            _paginaAluno.IsVisible = true;
        }
	}
}
