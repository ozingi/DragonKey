namespace DragonKey;
public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(Views.KeyCase), typeof(Views.KeyCase));
		Routing.RegisterRoute(nameof(Views.ViewOtpPage), typeof(Views.ViewOtpPage));
        Routing.RegisterRoute(nameof(Views.AddKeyPage), typeof(Views.AddKeyPage));
        Routing.RegisterRoute(nameof(Views.UpdateKeyPage), typeof(Views.UpdateKeyPage));
    }
}
