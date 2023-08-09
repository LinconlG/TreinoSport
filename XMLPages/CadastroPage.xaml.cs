using Microsoft.Maui.Devices;
using TreinoSport.Models;

namespace TreinoSport.XMLPages;

public partial class CadastroPage : ContentPage
{
	public CadastroPage()
	{
		InitializeComponent();
		CadastrarBtn.Clicked += ClickCadastrarBtn;
	}

	private void ClickCadastrarBtn(object sender, EventArgs e) {
		
		if (CheckCampos()) {
			return;
		}
		var usuario = new Usuario();
		usuario.Nome = nomeCompletoEntry.Text;
        usuario.Email = emailEntry.Text;
        usuario.Senha = senhaEntry.Text;
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