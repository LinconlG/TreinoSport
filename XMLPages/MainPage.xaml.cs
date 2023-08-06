using System.Diagnostics;
using System.Windows.Input;

namespace TreinoSport.XMLPages;

public partial class MainPage : ContentPage
{

    public MainPage()
	{
        InitializeComponent();
        CadastroBtn.Clicked += ClickCadastroBtn;
        
    }

    private void ClickCadastroBtn(object sender, EventArgs e) {
        if (Navigation.NavigationStack.Count == 1) {
            Navigation.PushAsync(new CadastroPage());
        }
    }
}

