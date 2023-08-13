using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using TreinoSport.Contexts;
using TreinoSport.Models;
using TreinoSport.Services;

namespace TreinoSport.XMLPages;

public partial class CadastroPage : ContentPage
{
    private readonly UsuarioService _usuarioService;
    private Button btn;

	public CadastroPage()
	{
        InitializeComponent();
        _usuarioService = new UsuarioService();
	}

	private async void ClickCadastrarBtn(object sender, EventArgs e) {

		if (CheckCampos()) {
			return;
		}
        btn = sender as Button;
        btn.IsEnabled = false;

		var usuario = new Usuario();
		usuario.Nome = nomeCompletoEntry.Text;
        usuario.Email = emailEntry.Text;
        usuario.Senha = Criptografia.sha256_hash(senhaEntry.Text);
        try {
            await _usuarioService.CadastrarUsuario(usuario);
            await DisplayAlert("Sucesso", "Você foi cadastrado!", "OK");
        }
        catch (Exception ex) {
            await DisplayAlert("Falha", $"{(ex.Message.Contains('@') ? MensagemPublicaError(ex.Message) : "Ocorreu um erro, tente novamente")}", "OK");
        }
        finally {
            btn.IsEnabled = true;
        }
    }
	private bool CheckCampos() {
		var flag = false;
		if (String.IsNullOrWhiteSpace(nomeCompletoEntry.Text)) {
			avisoNomeLabel.IsVisible = true;
			flag = true;
		}
		else {
            avisoNomeLabel.IsVisible = false;
        }
        if (String.IsNullOrWhiteSpace(emailEntry.Text)) {
            avisoEmailLabel.IsVisible = true;
            flag = true;
        }
        else {
            avisoEmailLabel.IsVisible = false;
        }
        if (String.IsNullOrWhiteSpace(senhaEntry.Text) || senhaEntry.Text.Length < 8) {
            avisoSenhaCaracteresLabel.IsVisible = true;
            flag = true;
        }
        else {
            avisoSenhaCaracteresLabel.IsVisible = false;
        }
        if (senhaEntry.Text != senhaConfirmacaoEntry.Text) {
            avisoSenhasLabel.IsVisible = true;
            flag = true;
        }
        else {
            avisoSenhasLabel.IsVisible = false;
        }
        return flag;
    }

    private string MensagemPublicaError(string mensagem) {
        return mensagem.Substring(mensagem.IndexOf('@') + 1, mensagem.LastIndexOf('@') - 1);
    }
}