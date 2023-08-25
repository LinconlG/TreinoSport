using TreinoSport.ViewModels;

namespace TreinoSport.XMLPages;

public partial class PaginaInicialCT : ContentPage
{
    TreinoViewModel treinoViewModel;
    public PaginaInicialCT()
	{
		InitializeComponent();
        this.BindingContext = treinoViewModel = new TreinoViewModel();
    }

	private void Teste(bool flag) {
		if (flag) {
			
		}
	}
}