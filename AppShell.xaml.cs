namespace TreinoSport;

public partial class AppShell : Shell
{
	private static ShellContent paginaCT;
    private static ShellContent paginaAluno;
    private static ShellContent paginaGerenciarTreino;
    public AppShell()
	{
		InitializeComponent();
		paginaCT = _tabPaginaCT;
		paginaAluno = _tabPaginaAluno;
		paginaGerenciarTreino = _tabGerenciamento;
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
