using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Storage;
using TreinoSport.Services;
using TreinoSport.ViewModels;
using TreinoSport.Views;

namespace TreinoSport;

public partial class MainPage : ContentPage
{
    private readonly UsuarioService _usuarioService;
    private readonly CadastroPage _cadastroPage;

    public MainPage(UsuarioService usuarioService, CadastroPage cadastroPage)
	{
        InitializeComponent();
        _usuarioService = usuarioService;
        _cadastroPage = cadastroPage;
        LembrarLogin();
    }

    private async void ClickCadastroBtn(object sender, EventArgs e) {
        await Navigation.PushAsync(_cadastroPage);
    }

    private async void ClickLoginBtn(object sender, EventArgs e) {
        if (CheckCampos())
            return;

        LoginBtn.IsEnabled = false;

        string email = LoginEmail.Text;
        string senha = Criptografia.sha256_hash(LoginSenha.Text);

        if (LembreLogin.IsChecked) {
            Preferences.Set("email", email);
            Preferences.Set("senha", senha);
        }

        await Login(email, senha);
    }

    private async Task Login(string email, string senha) {
        
        try {
            await _usuarioService.Login(email, senha);
        }
        catch (Exception) {
            avisoLogin.IsVisible = true;
            return;
        }
        finally {
            LoginBtn.IsEnabled = true;
        }

        if (Preferences.Get("isCT", false)) {
            AppShell.VisibilidadeFlyoutCT(true);
            await Shell.Current.GoToAsync($"//{nameof(PaginaInicialCT)}");
        }
        else {
            AppShell.VisibilidadeFlyoutCT(false);
            await Shell.Current.GoToAsync($"//{nameof(PaginaInicialAluno)}");
        }
    }

    private async void LembrarLogin() {
        var email = Preferences.Get("email", "falso");
        var senha = Preferences.Get("senha", "falso");
        if (email == "falso" || senha == "falso") {
            return;
        }
        await Login(email, senha);
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

