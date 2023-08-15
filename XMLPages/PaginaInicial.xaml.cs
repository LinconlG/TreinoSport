using System.Collections.ObjectModel;
using TreinoSport.Models;
using TreinoSport.Services;
using TreinoSport.ViewModels;

namespace TreinoSport.XMLPages;

public partial class PaginaInicial : ContentPage
{

    TreinoViewModel treinoViewModel;
	public PaginaInicial()
	{
        InitializeComponent();
        this.BindingContext = treinoViewModel = new TreinoViewModel();
	}

    protected override void OnAppearing() {
        base.OnAppearing();
        treinoViewModel.OnAppearing();
    }
}