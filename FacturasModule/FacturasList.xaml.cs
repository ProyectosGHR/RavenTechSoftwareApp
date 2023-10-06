namespace RavenTechSoftwareApp.FacturasModule;

public partial class FacturasList : ContentPage
{
    FacturaDatabase _database;//instancia de Facturadatabase para poder usar sus metodos


    // Constructor que recibe FacturaDatabase como parámetro
    public FacturasList()
    {
        InitializeComponent();
    }
    public FacturasList(FacturaDatabase database)
    {
        InitializeComponent();
        // Asigna el objeto database recibido como parámetro a _database
        _database = database;//le mandamos la coneccion a factura database con la dbpath que recibe el constructor

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        SurveysPanel.Children.Clear();//limpia el panel donde aparecen los registros

        var facturas = await _database.GetFactsAsync();//obtenemos todos los registros

        foreach (var factura in facturas)
        {
            var stackLayout = new StackLayout//crea un nuevo stacklayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(10, 5),
            };

            var label = new Label//nuevo label
            {
                Text = factura.ToString(),//imprimimos la cadena que retorna el modelo
                VerticalOptions = LayoutOptions.Center,
            };

            var button = new Button//creamos un nuevo boton    
            {
                Text = "Editar",
                TextColor = Color.FromHex("#FFFFFF"),
                BackgroundColor = Color.FromHex("#0000FF"), // Azul
                VerticalOptions = LayoutOptions.Center,
            };

            button.Clicked += async (s, e) =>//decimos que el boton que se crea nos lleve a crudfacts con su evento clicked
            {
                // Abre la página "CrudFacts" para editar la factura
                await Navigation.PushAsync(new CrudFacts(factura)); // Pasa la factura como parámetro
            };

            //añadimos el boton y el label al nuevo stacklayout
            stackLayout.Children.Add(label);
            stackLayout.Children.Add(button);

            SurveysPanel.Children.Add(stackLayout); //metemos el stacklayout en el panel
        }
    }

    private void reload_Clicked(object sender, EventArgs e)
    {
        OnAppearing();
    }
}