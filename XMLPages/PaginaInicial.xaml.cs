namespace TreinoSport.XMLPages;

public partial class PaginaInicial : ContentPage
{
	public PaginaInicial()
	{
        InitializeComponent();
        teste1();
	}

	private void teste1() {
		var modal = Navigation.ModalStack.Count;
		var navStack = Navigation.NavigationStack.Count;

		teste.Text = $"modal:{modal.ToString()} navStack{navStack.ToString()}";
	}
}