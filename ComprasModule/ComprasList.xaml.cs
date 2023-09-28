namespace RavenTechSoftwareApp.ComprasModule;

public partial class ComprasList : ContentPage
{
    CompraDatabase _database;

    // Constructor que recibe FacturaDatabase como parámetro
    public ComprasList()
	{
		InitializeComponent();
	}

    public ComprasList(CompraDatabase database)
    {
        InitializeComponent();
        // Asigna el objeto database recibido como parámetro a _database
        _database = database;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        SurveysPanel.Children.Clear();

        var compras = await _database.GetFactsAsync();

        foreach (var faccomprastura in compras)
        {
            var stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(10, 5),
            };

            var label = new Label
            {
                Text = compras.ToString(),
                VerticalOptions = LayoutOptions.Center,
            };

            var button = new Button
            {
                Text = "Editar",
                TextColor = Color.FromHex("#FFFFFF"),
                BackgroundColor = Color.FromHex("#0000FF"), // Azul
                VerticalOptions = LayoutOptions.Center,
            };

            button.Clicked += async (s, e) =>
            {
                // Abre la página "CrudFacts" para editar la factura
                await Navigation.PushAsync(new CrudCompras(faccomprastura)); // Pasa la factura como parámetro
            };

            stackLayout.Children.Add(label);
            stackLayout.Children.Add(button);

            SurveysPanel.Children.Add(stackLayout);
        }
    }
}