namespace TreinoSport.XMLPages;

public partial class PaginaInicialAluno : ContentPage
{
	public PaginaInicialAluno()
	{
		InitializeComponent();
	}

    private void ClickedBotao1(object sender, EventArgs e) {
        AppShell.VisibilidadePaginaInicialCT(true);
    }
    private void ClickedBotao2(object sender, EventArgs e) {
        AppShell.VisibilidadePaginaInicialCT(false);
    }

}