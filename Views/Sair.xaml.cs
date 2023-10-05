using TreinoSport.Models;

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
            else {
                flagCarregado = true;
            }
        };
    }

    private async void Saindo() {

        bool resposta = await DisplayAlert("Sair", "Deseja sair da sua conta?", "Sim", "Não");

        if (resposta) {
            ContaStatic.Logout();
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        else {
            if (ContaStatic.GetIsCT()) {
                await Shell.Current.GoToAsync($"//{nameof(PaginaInicialCT)}");
            }
            else {
                await Shell.Current.GoToAsync($"//{nameof(PaginaInicialAluno)}");
            }
        }
    }
}