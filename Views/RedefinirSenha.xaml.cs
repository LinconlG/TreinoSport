using TreinoSport.Extensions;
using TreinoSport.Models;
using TreinoSport.Services;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class RedefinirSenha : ContentPage
{
	RedefinirSenhaViewModel senhaViewModel;
	public RedefinirSenha()
	{
		InitializeComponent();
		this.BindingContext = senhaViewModel = new();
	}

    private async void ClickEnviarCodigo(object sender, EventArgs e) {
		if (ChecarCampos(flagEmail: true)) {
			return;
		}
		_stackInserirEmail.IsVisible = false;
		_carregamento.IsRunning = true;
		try {
			var email = _entryEmail.Text;
			var codigoConta = await senhaViewModel.EnviarTokenSenha(email);
            ContaStatic.SetCodigo(codigoConta);
			_carregamento.IsRunning = false;
			_stackInserirCodigo.IsVisible = true;
		}
        catch (Exception ex) {
            this.HandlerException(ex);
            _carregamento.IsRunning = false;
            _stackInserirEmail.IsVisible = true;
        }
    }
    private async void ClickChecarCodigo(object sender, EventArgs e) {
        if (ChecarCampos(flagCodigo: true)) {
            return;
        }
        _stackInserirCodigo.IsVisible = false;
		_carregamento.IsRunning = true;
		try {
			var token = _entryToken.Text;
            var codigoConta = ContaStatic.GetCodigo();
            await senhaViewModel.ChecarTokenSenha(codigoConta, token);
            ContaStatic.SetToken(token);
            _carregamento.IsRunning = false;
            _stackNovaSenha.IsVisible = true;
        }
        catch (Exception ex) {
            this.HandlerException(ex);
            _carregamento.IsRunning = false;
            _stackInserirCodigo.IsVisible = true;
        }
    }
    private async void ClickRedefinirSenha(object sender, EventArgs e) {
        if (ChecarCampos(flagSenha: true)) {
            return;
        }
        try {
            _stackNovaSenha.IsVisible = false;
            _carregamento.IsRunning = true;
            var senha = Criptografia.sha256_hash(_entrysenha.Text);
            var codigoConta = ContaStatic.GetCodigo();
            var token = ContaStatic.GetToken();
            await senhaViewModel.RedefinirSenha(codigoConta, senha, token);
            Preferences.Clear();
            await DisplayAlert("Senha alterada", "Sua senha foi alterada com sucesso!", "Ok");
            await Navigation.PopAsync();
        }
        catch (Exception ex) {
            this.HandlerException(ex);
            _carregamento.IsRunning = false;
            _stackNovaSenha.IsVisible = true;
        }
    }

    private bool ChecarCampos(bool flagEmail = false, bool flagCodigo = false, bool flagSenha = false) {
		if (flagEmail) {
            if (String.IsNullOrWhiteSpace(_entryEmail.Text) || !Criptografia.ValidarEmail(_entryEmail.Text)) {
                _labelAvisoEmail.IsVisible = true;
				return true;
            }
        }
		if (flagCodigo) {
            if (String.IsNullOrWhiteSpace(_entryToken.Text)) {
                _labelAvisoToken.IsVisible = true;
                return true;
            }
        }
		if (flagSenha) {
            if (String.IsNullOrWhiteSpace(_entrysenha.Text) || _entrysenha.Text.Length < 8 || _entrysenha.Text != _entryConfirmarSenha.Text) {
                _labelAvisoSenha.IsVisible = true;
                return true;
            }
        }
		return false;
	}
}