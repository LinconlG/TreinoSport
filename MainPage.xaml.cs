﻿using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Storage;
using TreinoSport.Extensions;
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
        if (CheckCampos())
            return;

        LoginBtn.IsEnabled = false;

        string email = _entryLoginEmail.Text;
        string senhaPref = _entryLoginSenha.Text ;

        if (LembreLogin.IsChecked) {
            Preferences.Set("email", email);
            Preferences.Set("senha", senhaPref);
        }

        await Login(email, senhaPref);
    }

    private async Task Login(string email, string senha) {
        
        try {
            string senhaCrpt = Criptografia.sha256_hash(senha);
            await loginViewModel.Login(email, senhaCrpt);
        }
        catch (Exception ex) {
            if (ex.IsPublicMessageCheck()) {
                await DisplayAlert("Erro", ex.Message, "Ok");
                _labelavisoLogin.IsVisible = true;
            }
            else {
                await DisplayAlert("Erro", "Ocorreu um erro!", "Ok");
            }
            Preferences.Clear();         
            return;
        }
        finally {
            LoginBtn.IsEnabled = true;
        }

        _entryLoginEmail.Text = String.Empty;
        _entryLoginSenha.Text = String.Empty;

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
        _entryLoginEmail.Text = email;
        _entryLoginSenha.Text = senha;
        await Login(email, senha);
    }

    private bool CheckCampos() {
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

