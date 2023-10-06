using TreinoSport.Contexts;
using TreinoSport.Extensions;
using TreinoSport.Models;

namespace TreinoSport.Views;

public partial class Configuracoes : ContentPage
{
	UsuarioContext usuarioContext;
	private bool flagCarregado;
    private int codigoConta = 1;
	public Configuracoes()
	{
		InitializeComponent();
		usuarioContext = new();
        codigoConta = ContaStatic.GetCodigo();
        Loaded += (s, e) => { BuscarConta(); };
        Appearing += (s, e) => {
            if (flagCarregado) {
                if (codigoConta != ContaStatic.GetCodigo()) {
                    BuscarConta();
                }
            }
            else {
                flagCarregado = true;
            }
        };
    }

	private async void BuscarConta() {

		try {    
            var conta = await usuarioContext.GetContaPorCodigo(codigoConta);
            _entryNome.Text = conta.Nome;
            if (ContaStatic.GetIsCT()) {
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
            this.HandlerException(ex);
        }
	}

	private async void ClickSalvarAlterações(object sender, EventArgs e) {
		try {
			var conta = new Conta();
			conta.Codigo = ContaStatic.GetCodigo();
			conta.Nome = _entryNome.Text;
			conta.Descricao = _entryDescricao.Text != null ? _entryDescricao.Text : null;
			conta.Email = _entryEmail.Text;
			await usuarioContext.PatchConta(conta);
			await DisplayAlert("Sucesso", "Seus dados foram atualizados.", "OK");
		}
        catch (Exception ex) {
            this.HandlerException(ex);
        }
    }
}