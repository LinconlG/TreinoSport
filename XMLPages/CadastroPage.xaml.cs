using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using TreinoSport.Contexts;
using TreinoSport.Models;
using TreinoSport.Services;

namespace TreinoSport.XMLPages;

public partial class CadastroPage : ContentPage
{
    private readonly UsuarioService _usuarioService;

	public CadastroPage()
	{
        InitializeComponent();
        _usuarioService = new UsuarioService();
	}

	private async void ClickCadastrarBtn(object sender, EventArgs e) {

		if (CheckCampos()) {
			return;
		}
		var usuario = new Usuario();
		usuario.Nome = nomeCompletoEntry.Text;
        usuario.Email = emailEntry.Text;
        usuario.Senha = senhaEntry.Text;
        try {
            await _usuarioService.CadastrarUsuario(usuario);
            await DisplayAlert("Sucesso", "Você foi cadastrado!", "OK");
        }
        catch (Exception ex) {
            await DisplayAlert("Falha", $"{(ex.Message.Contains('@') ? ex.Message.Substring(ex.Message.IndexOf('@')+1, ex.Message.LastIndexOf('@')-1) : "Ocorreu uma erro, tente novamente")}", "OK");
        }
    }
	private bool CheckCampos() {
		var flag = false;
		if (nomeCompletoEntry.Text == String.Empty || nomeCompletoEntry.Text == null) {
			avisoNomeLabel.IsVisible = true;
			flag = true;
		}
		else {
            avisoNomeLabel.IsVisible = false;
        }
        if (emailEntry.Text == String.Empty || emailEntry.Text == null) {
            avisoEmailLabel.IsVisible = true;
            flag = true;
        }
        else {
            avisoEmailLabel.IsVisible = false;
        }
        if (senhaEntry.Text == String.Empty || senhaEntry.Text == null) {
            avisoSenhaLabel.IsVisible = true;
            flag = true;
        }
        else {
            avisoSenhaLabel.IsVisible = false;
        }
        return flag;
    }
}