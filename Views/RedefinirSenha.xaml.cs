using TreinoSport.Extensions;
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
		//criar opcao ja tenho codigo
		try {
			var email = _entryEmail.Text;
			var codigoConta = await senhaViewModel.EnviarTokenSenha(email);
            Preferences.Set("codigoConta", codigoConta);
			_carregamento.IsRunning = false;
			_stackInserirCodigo.IsVisible = true;
		}
        catch (Exception ex) {
            if (ex.IsPublicMessageCheck()) {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
            else {
                await DisplayAlert("Erro", "Ocorreu um erro!", "Ok");
            }
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
            var codigoConta = Preferences.Get("codigoConta", 0);
            await senhaViewModel.ChecarTokenSenha(codigoConta, token);
            Preferences.Set("token", token);
            _carregamento.IsRunning = false;
            _stackNovaSenha.IsVisible = true;
        }
        catch (Exception ex) {
            if (ex.IsPublicMessageCheck()) {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
            else {
                await DisplayAlert("Erro", "Ocorreu um erro!", "Ok");
            }
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
            var codigoConta = Preferences.Get("codigoConta", 0);
            var token = Preferences.Get("token", "");
            await senhaViewModel.RedefinirSenha(codigoConta, senha, token);
            Preferences.Clear();
            await DisplayAlert("Senha alterada", "Sua senha foi alterada com sucesso!", "Ok");
            await Navigation.PopAsync();
        }
        catch (Exception ex) {
            if (ex.IsPublicMessageCheck()) {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
            else {
                await DisplayAlert("Erro", "Ocorreu um erro!", "Ok");
            }
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