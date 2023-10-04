namespace TreinoSport.Views;

public partial class Sair : ContentPage {
    private bool flagCarregado = false;
    public Sair() {
        InitializeComponent();
        BindingContext = this;
        Loaded += (s, e) => { Saindo(); };
        Appearing += (s, e) => {
            if (flagCarregado) {
                Saindo();
            }
        };
    }

    private async void Saindo() {

        bool resposta = await DisplayAlert("Sair", "Deseja sair da sua conta?", "Sim", "Não");

        if (resposta) {
            Preferences.Remove("email");
            Preferences.Remove("senha");
            Preferences.Remove("codigoConta");
            Preferences.Remove("isCT");
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        else {
            if (Preferences.Get("isCT", false)) {
                await Shell.Current.GoToAsync($"//{nameof(PaginaInicialCT)}");
            }
            else {
                await Shell.Current.GoToAsync($"//{nameof(PaginaInicialAluno)}");
            }
        }
        flagCarregado = true;
    }
}