using RavenTechSoftwareApp.FacturasModule;
using RavenTechSoftwareApp.Pages;

namespace RavenTechSoftwareApp;


public partial class App : Application
{
    public static string DBPath { get; private set; }
    public static FacturaDatabase Database { get; private set; }

    public App()
	{
		InitializeComponent();
        DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Raventech.db3");
        Database = new FacturaDatabase(DBPath);

        MainPage = new AppShell();
	}
}
