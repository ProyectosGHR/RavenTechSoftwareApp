namespace RavenTechSoftwareApp.VentasModule;

public partial class CrudVentas : ContentPage
{
    VentaDatabase _database; //hacemos instancias de la compradatabase para poder usar sus metodos 
    Venta _ventas; //instancia de el modelo para poner devolverlo para que lo trabaje compra databse
    public CrudVentas(Venta ventas) //hacemos que crud compra reciba el objeto del modelo para poder trabajarlo
    {
        InitializeComponent();
        _database = new VentaDatabase(App.DBPath);
        _ventas = ventas; // Almacena la factura recibida como parámetro

        // Configura los Entry con los datos de la factura 
        IdEntry.Text = _ventas.Id.ToString();
        FechaEntry.Text = _ventas.Fecha;
        VendedorEntry.Text = _ventas.Vendedor;
        CompradorEntry.Text = _ventas.Comprador;
        DescripcionEntry.Text = _ventas.Descripcion;
        TotalEntry.Text = _ventas.Total.ToString();
    }

    private async void GuardarCompras_Clicked(object sender, EventArgs e)
    {
        var entryElements = this.Content.FindByName<VerticalStackLayout>("VentasStackLayout").Children.OfType<Entry>();

        // Verifica si algún Entry está en blanco o nulo
        if (entryElements.Any(entry => string.IsNullOrWhiteSpace(entry.Text)))
        {
            return;
        }
        //si esta todo completo almacena la informacion en nuevo objeto de tipo compra
        var newfact = new Venta()
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

    private async void BorrarCompra_Clicked(object sender, EventArgs e)
    {
        // Muestra un mensaje de confirmación para borrar la factura
        var confirmar = await DisplayAlert("Confirmación", "¿Estás seguro de que deseas borrar esta factura?", "Sí", "No");
        if (confirmar)
        {
            // Llama a un método para borrar la factura de la base de datos
            await _database.DeleteFactAsync(_ventas);
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
}