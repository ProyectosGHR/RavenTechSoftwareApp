namespace RavenTechSoftwareApp.Pages;
using RavenTechSoftwareApp.ComprasModule;

public partial class Compras : ContentPage
{
     CompraDatabase _database;
    public Compras()
	{
		InitializeComponent();
        _database = new CompraDatabase(App.DBPath);//nos conectamos a compra databse 

    }

    private async void resgiscompra_Clicked(object sender, EventArgs e)
    {
        var entryElements = this.Content.FindByName<VerticalStackLayout>("FacturasStackLayout").Children.OfType<Entry>();

        // Verifica si algún Entry está en blanco o nulo
        if (entryElements.Any(entry => string.IsNullOrWhiteSpace(entry.Text)))
        {
            return;
        }

        //si esta todo completo almacena la informacion en nuevo objeto de tipo compra
        var newfact = new Compra()
        {
            Fecha = FechaEntry.Text,
            Vendedor = VendedorEntry.Text,
            Comprador = CompradorEntry.Text,
            Descripcion = DescripcionEntry.Text,
            Total = double.TryParse(TotalEntry.Text, out double total) ? total : 0.0
        };

        //mandamos como argumento el nuevo objeto  a compratabase a su metodo savefact
        await _database.SaveFact(newfact);

        MessagingCenter.Send((ContentPage)this, Messages.NewComplete, newfact);

        // Limpia los campos Entry después de guardar
        FechaEntry.Text = "";
        VendedorEntry.Text = "";
        CompradorEntry.Text = "";
        DescripcionEntry.Text = "";
        TotalEntry.Text = "";
        await DisplayAlert("Exito", "Se guardo el registro con exito", "Aceptar");

    }

    private void verlistacompras_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Pasa _database como un parámetro a FacturasList
            Navigation.PushAsync(new ComprasList(_database));
        }
        catch (Exception ex)
        {
            // Manejo de excepciones
        }
    }
}