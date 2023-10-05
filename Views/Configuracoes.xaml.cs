using TreinoSport.Contexts;
using TreinoSport.Extensions;
using TreinoSport.Models;

namespace TreinoSport.Views;

public partial class Configuracoes : ContentPage
{
	UsuarioContext usuarioContext;
	private bool flagCarregado;
	public Configuracoes()
	{
		InitializeComponent();
		usuarioContext = new();
        Loaded += (s, e) => { BuscarConta(); };
        Appearing += (s, e) => {
            if (flagCarregado) {
                BuscarConta();
            }
            else {
                flagCarregado = true;
            }
        };
    }

	private async void BuscarConta() {

		try {
            var codigoConta = Preferences.Get("codigoConta", 0);
            var conta = await usuarioContext.GetContaPorCodigo(codigoConta);
            _entryNome.Text = conta.Nome;
            if (Preferences.Get("isCT", false)) {
                _gridDescricao.IsVisible = true;
                _labelDescricao.IsVisible = true;
                _entryDescricao.Text = conta.Descricao;
            }
            else {
                _labelDescricao.IsVisible = false;
                _gridDescricao.IsVisible = false;
            }
            _entryEmail.Text = conta.Email;
        }
        catch (Exception ex) {
            if (ex.IsPublicMessageCheck()) {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
            else {
                await DisplayAlert("Erro", "Ocorreu um erro!", "Ok");
            }
        }
	}

	private async void ClickSalvarAlterações(object sender, EventArgs e) {
		try {
			var conta = new Conta();
			conta.Codigo = Preferences.Get("codigoConta", 0);
			conta.Nome = _entryNome.Text;
			conta.Descricao = _entryDescricao.Text != null ? _entryDescricao.Text : null;
			conta.Email = _entryEmail.Text;
			await usuarioContext.PatchConta(conta);
			await DisplayAlert("Sucesso", "Seus dados foram atualizados.", "OK");
		}
        catch (Exception ex) {
            if (ex.IsPublicMessageCheck()) {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
            else {
                await DisplayAlert("Erro", "Ocorreu um erro!", "Ok");
            }
        }
    }
}