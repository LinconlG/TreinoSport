using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using TreinoSport.Contexts;
using TreinoSport.Models;
using TreinoSport.Services;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class CadastroPage : ContentPage
{
    private CadastroViewModel cadastroViewModel { get; set; }
    private Button btn;

	public CadastroPage()
	{
        InitializeComponent();
        this.BindingContext = cadastroViewModel = new CadastroViewModel();
	}

	private async void ClickCadastrarBtn(object sender, EventArgs e) {

		if (CheckCampos()) {
			return;
		}
        btn = sender as Button;
        btn.IsEnabled = false;

		var usuario = new Conta();
		usuario.Nome = nomeCompletoEntry.Text;
        usuario.Email = emailEntry.Text;
        usuario.Senha = Criptografia.sha256_hash(senhaEntry.Text);
        usuario.IsCentroTreinamento = false;
        if (tipoConta.SelectedIndex == 1) {
            usuario.IsCentroTreinamento = true;
            usuario.Descricao = editorDescricao.Text;
        }
        try {
            await cadastroViewModel.CadastrarUsuario(usuario);
            await DisplayAlert("Sucesso", "Você foi cadastrado!", "OK");
            LimparCampos();
            await Navigation.PopAsync();
        }
        catch (Exception ex) {
            await DisplayAlert("Falha", $"{(ex.Message.Contains('@') ? MensagemPublicaError(ex.Message) : "Ocorreu um erro, tente novamente")}", "OK");
        }
        finally {
            btn.IsEnabled = true;
        }
    }

    private void OnPickerSelectedIndexChanged(object sender, EventArgs e) {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex == 1) {
            labelDescricao.IsVisible = true;
            editorDescricao.IsVisible = true;
        }
        else {
            labelDescricao.IsVisible = false;
            editorDescricao.IsVisible = false;
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
        if (String.IsNullOrWhiteSpace(emailEntry.Text) || !Criptografia.ValidarEmail(emailEntry.Text)) {
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

    private void LimparCampos() {
        tipoConta.SelectedItem = null;
        nomeCompletoEntry.Text = string.Empty;
        editorDescricao.Text = string.Empty;
        emailEntry.Text = string.Empty;
        senhaEntry.Text = string.Empty;
        senhaConfirmacaoEntry.Text = string.Empty;
    }

    private string MensagemPublicaError(string mensagem) {
        return mensagem.Substring(mensagem.IndexOf('@') + 1, mensagem.LastIndexOf('@') - 1);
    }
}