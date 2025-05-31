using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Storage;
using TreinoSport.Extensions;
using TreinoSport.Models;
using TreinoSport.Services;
using TreinoSport.ViewModels;
using TreinoSport.Views;

namespace TreinoSport;

public partial class MainPage : ContentPage
{
    private LoginViewModel loginViewModel;

    public MainPage()
	{
        InitializeComponent();
        this.BindingContext = loginViewModel = new();
        LembrarLogin();
    }

    private async void ClickCadastroBtn(object sender, EventArgs e) {
        await Navigation.PushAsync(new CadastroPage());
    }
    private async void ClickSenhaBtn(object sender, EventArgs e) {
        await Navigation.PushAsync(new RedefinirSenha());
    }
    private async void ClickLoginBtn(object sender, EventArgs e) {
        try {
            if (CheckCampos())
                return;

            LoginBtn.IsEnabled = false;

            string email = _entryLoginEmail.Text;
            string senhaPref = _entryLoginSenha.Text;

            if (_checkLembrarLogin.IsChecked) {
                ContaStatic.SetConta(email, senhaPref);
            }

            await Login(email, senhaPref);
        }
        catch (Exception ex) {

            throw new Exception(ex.Message);
        }

    }
    private async Task Login(string email, string senha) {

        try {
            string senhaCrpt = Criptografia.sha256_hash(senha);
            await loginViewModel.Login(email, senhaCrpt);
        }
        catch (Exception ex) {
            this.HandlerException(ex);
            Preferences.Clear();         
            return;
        }
        finally {
            LoginBtn.IsEnabled = true;
        }

        _entryLoginEmail.Text = String.Empty;
        _entryLoginSenha.Text = String.Empty;

        if (ContaStatic.GetIsCT()) {
            AppShell.VisibilidadeFlyoutCT(true);
            await Shell.Current.GoToAsync($"//{nameof(PaginaInicialCT)}");
        }
        else {
            AppShell.VisibilidadeFlyoutCT(false);
            await Shell.Current.GoToAsync($"//{nameof(PaginaInicialAluno)}");
        }
    }

    private async void LembrarLogin() {
        var email = ContaStatic.GetEmail();
        var senha = ContaStatic.GetSenha();
        if (email == null || senha == null) {
            _checkLembrarLogin.IsChecked = false;
            return;
        }
        _entryLoginEmail.Text = email;
        _entryLoginSenha.Text = senha;
        await Login(email, senha);
    }

    private bool CheckCampos() {
        return false;
        var flag = false;
        if (String.IsNullOrWhiteSpace(_entryLoginEmail.Text) || !Criptografia.ValidarEmail(_entryLoginEmail.Text) || String.IsNullOrWhiteSpace(_entryLoginSenha.Text) || _entryLoginSenha.Text.Length < 8) {
            _labelavisoLogin.IsVisible = true;
            flag = true;
        }
        else {
            _labelavisoLogin.IsVisible = false;
        }
        return flag;
    }
}

