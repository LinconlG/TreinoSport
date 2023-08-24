using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Storage;
using TreinoSport.Services;
using TreinoSport.XMLPages;

namespace TreinoSport;

public partial class MainPage : ContentPage
{
    private readonly UsuarioService _usuarioService;
    public MainPage()
	{
        InitializeComponent();
        _usuarioService = new UsuarioService();
        LembrarLogin();
    }

    private async void ClickCadastroBtn(object sender, EventArgs e) {
        await Navigation.PushAsync(new CadastroPage());
    }

    private void ClickLoginBtn(object sender, EventArgs e) {
        if (CheckCampos()) {
            return;
        }
        string email = LoginEmail.Text;
        string senha = Criptografia.sha256_hash(LoginSenha.Text);
        if (LembreLogin.IsChecked) {
            Preferences.Set("email", email);
            Preferences.Set("senha", senha);
        }
        Login(email, senha);
    }

    private async void Login(string email, string senha) {

        try {
            await _usuarioService.Login(email, senha);
        }
        catch (Exception) {
            avisoLogin.IsVisible = true;
            return;
        }
        await Shell.Current.GoToAsync($"//{nameof(PaginaInicial)}");
    }

    private void LembrarLogin() {
        var email = Preferences.Get("email", "falso");
        var senha = Preferences.Get("senha", "falso");
        if (email == "falso" || senha == "falso") {
            return;
        }
        Login(email, senha);
    }

    private bool CheckCampos() {
        var flag = false;
        if (String.IsNullOrWhiteSpace(LoginEmail.Text) || !Criptografia.ValidarEmail(LoginEmail.Text) || String.IsNullOrWhiteSpace(LoginSenha.Text) || LoginSenha.Text.Length < 8) {
            avisoLogin.IsVisible = true;
            flag = true;
        }
        else {
            avisoLogin.IsVisible = false;
        }
        return flag;
    }
}

