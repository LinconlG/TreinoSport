using Microsoft.Maui.Controls.PlatformConfiguration;

namespace TreinoSport;

public partial class AppShell : Shell
{
	private static FlyoutItem paginaCT;
    private static FlyoutItem paginaAluno;
    private static FlyoutItem paginaGerenciarTreino;
    public AppShell()
	{
		InitializeComponent();
		paginaCT = _flyoutPaginaInicialCT;
		paginaAluno = _flyoutPaginaInicialAluno;
		paginaGerenciarTreino = _flyoutGerenciamentoTreino;
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
            paginaCT.IsVisible = true;
			paginaGerenciarTreino.IsVisible = true;
			paginaAluno.IsVisible = false;
		}
		else {
			paginaCT.IsVisible = false;
            paginaGerenciarTreino.IsVisible = false;
            paginaAluno.IsVisible = true;
        }
	}
}
