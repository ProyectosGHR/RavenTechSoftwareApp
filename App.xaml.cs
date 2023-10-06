using RavenTechSoftwareApp.FacturasModule;
using RavenTechSoftwareApp.Pages;
using RavenTechSoftwareApp.ComprasModule;

namespace RavenTechSoftwareApp;


public partial class App : Application
{
    public static string DBPath { get; private set; }

    public App()
	{
		InitializeComponent();
        DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RaventechSoft.db3");
        MainPage = new AppShell();
	}
}
