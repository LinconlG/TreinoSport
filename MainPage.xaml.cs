using Microsoft.Maui.Controls.Xaml;
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
    }

    private async void ClickCadastroBtn(object sender, EventArgs e) {
        await Navigation.PushAsync(new CadastroPage());
    }

    private async void ClickLoginBtn(object sender, EventArgs e) {
        if (CheckCampos()) {
            return;
        }
        string email = LoginEmail.Text;
        string senha = Criptografia.sha256_hash(LoginSenha.Text);
        try {
            await _usuarioService.Login(email, senha);
        }
        catch (Exception) {
            avisoLogin.IsVisible = true;
            return;
        }
        await Shell.Current.GoToAsync($"//{nameof(PaginaInicial)}");
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

