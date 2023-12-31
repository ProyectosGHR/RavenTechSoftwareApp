namespace RavenTechSoftwareApp.Pages;
using RavenTechSoftwareApp.FacturasModule;

public partial class Facturas : ContentPage
{
    FacturaDatabase _database;
    public Facturas()
	{
		InitializeComponent();
        _database = new FacturaDatabase(App.DBPath);
    }

    private void VerFact_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Pasa _database como un par�metro a FacturasList
            Navigation.PushAsync(new FacturasList(_database));
        }
        catch (Exception ex)
        {
            // Manejo de excepciones
        }
    }

    private async void GuardarFact_Clicked(object sender, EventArgs e)
    {
        var entryElements = this.Content.FindByName<VerticalStackLayout>("FacturasStackLayout").Children.OfType<Entry>();

        // Verifica si alg�n Entry est� en blanco o nulo
        if (entryElements.Any(entry => string.IsNullOrWhiteSpace(entry.Text)))
        {
            return;
        }

        var newfact = new Factura()
        {
            Fecha = FechaEntry.Text,
            Vendedor = VendedorEntry.Text,
            Comprador = CompradorEntry.Text,
            Descripcion = DescripcionEntry.Text,
            Total = double.TryParse(TotalEntry.Text, out double total) ? total : 0.0
        };

        await _database.SaveFact(newfact);

        MessagingCenter.Send((ContentPage)this,Messages.NewComplete, newfact);

        // Limpia los campos Entry despu�s de guardar
        FechaEntry.Text = "";
        VendedorEntry.Text = "";
        CompradorEntry.Text = "";
        DescripcionEntry.Text = "";
        TotalEntry.Text = "";
        await DisplayAlert("Exito", "Se guardo el registro con exito", "Aceptar");

    }

}