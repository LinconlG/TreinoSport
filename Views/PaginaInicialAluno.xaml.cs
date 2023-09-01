using System.Collections.ObjectModel;
using TreinoSport.Models;
using TreinoSport.Services;
using TreinoSport.ViewModels;

namespace TreinoSport.Views;

public partial class PaginaInicialAluno : ContentPage
{
    TreinoViewModel _treinoViewModel;
	public PaginaInicialAluno(TreinoViewModel treinoViewModel)
	{
        InitializeComponent();
        this.BindingContext = _treinoViewModel = treinoViewModel;
	}

    protected override void OnAppearing() {
        base.OnAppearing();
        _treinoViewModel.OnAppearing(RefreshLista, avisoTreinosVazio);
    }
}