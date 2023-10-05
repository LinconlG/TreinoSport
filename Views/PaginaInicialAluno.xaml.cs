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
        var datas = treinoViewModel.Treinos.Where(t => t.Codigo == codigoTreino).Select(t => t.DatasTreinos).First();
        await Navigation.PushAsync(new AlunoTreinoDetalhes(codigoTreino, datas));
    }
    protected override void OnAppearing() {
        base.OnAppearing();
        treinoViewModel.OnAppearing(RefreshLista, avisoTreinosVazio);
    }
}