namespace RavenTechSoftwareApp.FacturasModule;
using RavenTechSoftwareApp.Pages;

public partial class CrudFacts : ContentPage
{
    FacturaDatabase _database;
    Factura _factura;
    public CrudFacts(Factura factura)
	{
		InitializeComponent();
        _database = new FacturaDatabase(App.DBPath);
        _factura = factura; // Almacena la factura recibida como parámetro

        // Configura los Entry con los datos de la factura 
        IdEntry.Text = _factura.Id.ToString();
        FechaEntry.Text = _factura.Fecha;
        VendedorEntry.Text = _factura.Vendedor;
        CompradorEntry.Text = _factura.Comprador;
        DescripcionEntry.Text = _factura.Descripcion;
        TotalEntry.Text = _factura.Total.ToString();
    }

    private async void GuardarFact_Clicked(object sender, EventArgs e)
    {
        var entryElements = this.Content.FindByName<VerticalStackLayout>("FacturasStackLayout").Children.OfType<Entry>();

        // Verifica si algún Entry está en blanco o nulo
        if (entryElements.Any(entry => string.IsNullOrWhiteSpace(entry.Text)))
        {
            return;
        }

        var newfact = new Factura()
        {
            Id = Convert.ToInt32(IdEntry.Text),
            Fecha = FechaEntry.Text,
            Vendedor = VendedorEntry.Text,
            Comprador = CompradorEntry.Text,
            Descripcion = DescripcionEntry.Text,
            Total = double.TryParse(TotalEntry.Text, out double total) ? total : 0.0
        };

        await _database.SaveFact(newfact);
        // Limpia los campos Entry después de guardar
        FechaEntry.Text = "";
        VendedorEntry.Text = "";
        CompradorEntry.Text = "";
        DescripcionEntry.Text = "";
        TotalEntry.Text = "";

        await DisplayAlert("Exito", "Se actualizo el registro con exito", "Aceptar");
        await Navigation.PopToRootAsync();



    }



    private async void BorrarFact_Clicked(object sender, EventArgs e)
    {
        // Muestra un mensaje de confirmación para borrar la factura
        var confirmar = await DisplayAlert("Confirmación", "¿Estás seguro de que deseas borrar esta factura?", "Sí", "No");
        if (confirmar)
        {
            // Llama a un método para borrar la factura de la base de datos
            await _database.DeleteFactAsync(_factura);
            // Limpia los campos Entry después de guardar
            FechaEntry.Text = "";
            VendedorEntry.Text = "";
            CompradorEntry.Text = "";
            DescripcionEntry.Text = "";
            TotalEntry.Text = "";

            await DisplayAlert("Exito", "Se borro el registro con exito", "Aceptar");
            await Navigation.PopToRootAsync();
        }
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}