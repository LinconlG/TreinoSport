using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using TreinoSport.Contexts;
using TreinoSport.Models;

namespace TreinoSport.XMLPages;

public partial class CadastroPage : ContentPage
{
    private readonly UsuarioContext _usuarioContext;

	public CadastroPage(UsuarioContext usuarioContext)
	{
        InitializeComponent();
        _usuarioContext = usuarioContext;

        CadastrarBtn.Clicked += ClickCadastrarBtn;
	}

	private async void ClickCadastrarBtn(object sender, EventArgs e) {

        var b = (Button)sender;
        b.IsEnabled = false;
		if (CheckCampos()) {
			return;
		}
		var usuario = new Usuario();
		usuario.Nome = nomeCompletoEntry.Text;
        usuario.Email = emailEntry.Text;
        usuario.Senha = senhaEntry.Text;
        //try {
            await _usuarioContext.CadastrarUsuario(usuario);
            await DisplayAlert("Sucesso", "Você foi cadastrado!", "OK");
        //}
        //catch (Exception exc) {
          //  await DisplayAlert("Falha", $"Você não foi cadastrado, tente novamente \n{exc.Message}", "OK");
        //}

    }
	private bool CheckCampos() {
		var flag = false;
		if (nomeCompletoEntry.Text == String.Empty) {
			avisoNomeLabel.IsVisible = true;
			flag = true;
		}
		else {
            avisoNomeLabel.IsVisible = false;
        }
        if (emailEntry.Text == String.Empty) {
            avisoEmailLabel.IsVisible = true;
            flag = true;
        }
        else {
            avisoEmailLabel.IsVisible = false;
        }
        if (senhaEntry.Text == String.Empty) {
            avisoSenhaLabel.IsVisible = true;
            flag = true;
        }
        else {
            avisoSenhaLabel.IsVisible = false;
        }
        return flag;
    }
}