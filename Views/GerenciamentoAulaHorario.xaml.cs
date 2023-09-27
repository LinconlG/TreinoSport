using CommunityToolkit.Maui.Views;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class GerenciamentoAulaHorario : Popup
{
	TreinoViewModel treinoViewModel;
	public GerenciamentoAulaHorario(int codigoTreino, int codigoDia, int codigoHorario)
	{
		InitializeComponent();
		this.BindingContext = treinoViewModel = new();
	}
}