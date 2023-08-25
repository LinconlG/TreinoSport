using System.Collections.ObjectModel;
using TreinoSport.Models;
using TreinoSport.Services;
using TreinoSport.ViewModels;

namespace TreinoSport.XMLPages;

public partial class PaginaInicialAluno : ContentPage
{

    TreinoViewModel treinoViewModel;
	public PaginaInicialAluno()
	{
        InitializeComponent();
        this.BindingContext = treinoViewModel = new TreinoViewModel();
	}

    protected override void OnAppearing() {
        base.OnAppearing();
        treinoViewModel.OnAppearing();
    }
}