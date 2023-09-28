using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class PaginaInicialAluno : ContentPage {
    private TreinoViewModel treinoViewModel;
    public PaginaInicialAluno() {
        InitializeComponent();
        this.BindingContext = treinoViewModel = new();
    }
    private async void ClickAlunoDetalhesTreino(object sender, EventArgs e) {
        Button btn = (Button)sender;
        var codigoTreino = int.Parse(btn.ClassId);
        await Navigation.PushAsync(new AlunoTreinoDetalhes(codigoTreino));
    }
    protected override void OnAppearing() {
        base.OnAppearing();
        treinoViewModel.OnAppearing(RefreshLista, avisoTreinosVazio);
    }
}