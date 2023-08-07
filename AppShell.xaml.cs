using RavenTechSoftwareApp.Pages;

namespace RavenTechSoftwareApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("Dashboard",typeof(Dashboard));
    }
}
