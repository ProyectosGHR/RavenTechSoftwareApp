using RavenTechSoftwareApp.Pages;

namespace RavenTechSoftwareApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("Dashboard",typeof(Dashboard));
        Routing.RegisterRoute("Facturas", typeof(Facturas));
        Routing.RegisterRoute("Ventas", typeof(Ventas));
        Routing.RegisterRoute("Compras", typeof(Compras));

    }
}
