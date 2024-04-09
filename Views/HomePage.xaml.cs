namespace DragonKey.Views;
using CommunityToolkit.Maui.Alerts;
using System.Xml.Linq;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        BindingContext = new Models.core.Home();
        var toolbarItem = new ToolbarItem { Text = "Add" };
        toolbarItem.IconImageSource = GetPlatformImage("add_key");
        toolbarItem.Clicked += Add_Clicked;
        ToolbarItems.Add(toolbarItem);
    }
    protected override void OnAppearing()
    {
        ((Models.core.Home)BindingContext).LoadNotes();
    }
    ImageSource GetPlatformImage(string baseName)
    {
        // Get the current device platform
        var platform = DeviceInfo.Platform;

        // Return a different image source based on the platform
        if (platform == DevicePlatform.WinUI)
        {
            return ImageSource.FromFile(baseName + "_windows.png");
        }
        else
        {
            return ImageSource.FromFile(baseName + ".png");
        }

    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddKeyPage));
    }

    private async void secretKeysCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var secretKey = (Models.core.AddKey)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(ViewOtpPage)}?{nameof(ViewOtpPage.ItemId)}={secretKey.SecretKey}&" +
                $"{nameof(ViewOtpPage.Filepath)}={secretKey.FilenamePath}&" +
                $"{nameof(ViewOtpPage.Fname)}={secretKey.Filename}");

            // Unselect the UI
            secretKeysCollection.SelectedItem = null;
        }
    }
}