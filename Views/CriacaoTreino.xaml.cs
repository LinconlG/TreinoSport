namespace TreinoSport.Views;

public partial class CriacaoTreino : ContentPage
{
    private TimePicker _novoHorario;
	public CriacaoTreino()
	{
		InitializeComponent();
	}

	private void ClickAddHorario(object sender, EventArgs e) {
        _novoHorario = new TimePicker {
            Format = "HH:mm"
        };

        //vou ter que criar view model com uma lista de horarios ou dia da semana para que add dinamicamente com uma especie de collection view?

        var linhaBtnAddRmv = Grid.GetRow(_btnAddHorario);
		var colunaBtnAddRmv = Grid.GetColumn(_btnAddHorario);

        Grid.SetRow(_novoHorario, linhaBtnAddRmv);
        Grid.SetColumn(_novoHorario, colunaBtnAddRmv);

        if (colunaBtnAddRmv == 3) {
			//criar nova linha no grid
			
			Grid.SetRow(_btnAddHorario, linhaBtnAddRmv + 1);
			Grid.SetColumn(_btnAddHorario, 0);

            Grid.SetRow(_btnRemoverHorario, linhaBtnAddRmv + 1);
            Grid.SetColumn(_btnRemoverHorario, 0);
        }
		else {
            Grid.SetColumn(_btnAddHorario, colunaBtnAddRmv + 1);
            Grid.SetColumn(_btnRemoverHorario, colunaBtnAddRmv + 1);
        }
	}
}