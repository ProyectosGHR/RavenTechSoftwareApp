namespace RavenTechSoftwareApp.FacturasModule;
using RavenTechSoftwareApp.Pages;

public partial class CrudFacts : ContentPage
{
    FacturaDatabase _database; //hacemos instancias de la Facturadatabase para poder usar sus metodos 
    Factura _factura;//instancia de el modelo para poner devolverlo para que lo trabaje compra databse
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
        //si esta todo completo almacena la informacion en nuevo objeto de tipo factura
        var newfact = new Factura()
        {
            Id = Convert.ToInt32(IdEntry.Text),
            Fecha = FechaEntry.Text,
            Vendedor = VendedorEntry.Text,
            Comprador = CompradorEntry.Text,
            Descripcion = DescripcionEntry.Text,
            Total = double.TryParse(TotalEntry.Text, out double total) ? total : 0.0
        };
        //mandamos como argumento el nuevo objeto  a compratabase a su metodo savefact
        await _database.SaveFact(newfact);
        // Limpia los campos Entry después de guardar
        FechaEntry.Text = "";
        VendedorEntry.Text = "";
        CompradorEntry.Text = "";
        DescripcionEntry.Text = "";
        TotalEntry.Text = "";

        await DisplayAlert("Exito", "Se actualizo el registro con exito", "Aceptar");//y desplegamos una alerta que diga que la factura esta actalizada
        await Navigation.PopToRootAsync();//nos devuelve a la pagina raiz



    }



    private async void BorrarFact_Clicked(object sender, EventArgs e)
    {
        // Muestra un mensaje de confirmación para borrar la factura
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

            await DisplayAlert("Exito", "Se borro el registro con exito", "Aceptar");//desplegamos una alerta que diga que se borro la factura
            await Navigation.PopToRootAsync();//nos devuelve a la pagina raiz
        }
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}