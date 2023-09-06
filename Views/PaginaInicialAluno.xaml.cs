using System.Collections.ObjectModel;
using TreinoSport.Models;
using TreinoSport.Services;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class PaginaInicialAluno : ContentPage
{
    private TreinoViewModel treinoViewModel;
	public PaginaInicialAluno()
	{
        InitializeComponent();
        this.BindingContext = treinoViewModel = new();
	}

    protected override void OnAppearing() {
        base.OnAppearing();
        treinoViewModel.OnAppearing(RefreshLista, avisoTreinosVazio);
    }
}