namespace RavenTechSoftwareApp.FacturasModule;

public partial class FacturasList : ContentPage
{
    FacturaDatabase _database;

    // Constructor que recibe FacturaDatabase como parámetro
    public FacturasList()
    {
        InitializeComponent();
    }
    public FacturasList(FacturaDatabase database)
    {
        InitializeComponent();
        // Asigna el objeto database recibido como parámetro a _database
        _database = database;
    }

    protected override async void OnAppearing()
    {

        SurveysPanel.Children.Clear();

        var facturas = await _database.GetFactsAsync();

        foreach (var factura in facturas)
        {
            var stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(10, 5),
            };

            var label = new Label
            {
                Text = factura.ToString(),
                VerticalOptions = LayoutOptions.Center,
            };

            var button = new Button
            {
                Text = "Editar",
                BackgroundColor = Color.FromHex("#0000FF"), // Azul
                VerticalOptions = LayoutOptions.Center,
            };

            button.Clicked += async (s, e) =>
            {
                // Abre la página "CrudFacts" para editar la factura
                await Navigation.PushAsync(new CrudFacts(factura)); // Pasa la factura como parámetro
            };

            stackLayout.Children.Add(label);
            stackLayout.Children.Add(button);

            SurveysPanel.Children.Add(stackLayout);
        }
    }

    private void reload_Clicked(object sender, EventArgs e)
    {
        OnAppearing();
    }
}