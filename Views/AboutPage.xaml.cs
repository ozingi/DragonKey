namespace DragonKey.Views;
using DragonKey.Models.core;
public partial class AboutPage : ContentPage
{
	int count = 0;

	public AboutPage()
	{
		InitializeComponent();
		About about = new About();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

//		if (count == 1)
//			CounterBtn.Text = $"Clicked {count} time";
//		else
//			CounterBtn.Text = $"Clicked {count} times";

//		SemanticScreenReader.Announce(CounterBtn.Text);
	}
    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.core.About about)
        {
            // Navigate to the specified URL in the system browser.
            await Launcher.Default.OpenAsync(about.GithubUrl);
        }

    }
}

