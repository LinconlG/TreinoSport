using TreinoSport.Views;

namespace TreinoSport;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
