using System.Reflection.Emit;
using DragonKey.Models.core;
using CommunityToolkit.Maui.Alerts;
namespace DragonKey.Views;
[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class UpdateKeyPage : ContentPage
{


    public UpdateKeyPage()

    {

        InitializeComponent();
       // string appDataPath = FileSystem.AppDataDirectory;
        //string initFileName = ItemId;

        //LoadSecretKey(Path.Combine(appDataPath, initFileName));

        //LoadSecretKey(Path.Combine(appDataPath, ItemId));
    }



    protected override void OnAppearing()
    {
    }
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is AddKey addkey)
        {
            if (addkey.FilenamePath == $"{FileSystem.AppDataDirectory}" + $"/{FilenameEditor.Text}")
            {
                File.WriteAllText(addkey.FilenamePath, SecretkeyEditor.Text);
                File.Move(addkey.FilenamePath, $"{FileSystem.AppDataDirectory}" + $"/{FilenameEditor.Text}", true);
            }
            else
            {
                try
                {
                    File.WriteAllText(addkey.FilenamePath, SecretkeyEditor.Text);
                    File.Move(addkey.FilenamePath, $"{FileSystem.AppDataDirectory}" + $"/{FilenameEditor.Text}", false);
                }
                catch(IOException ex)
                {
                    await DisplayAlert("⚠️", "已存在其他相同账户名，内容将保存到当前账户", "OK");
                    #if __ANDROID__
                        var toast = Toast.Make("保存成功");
                        await toast.Show();
                    #endif
                }
            }
        }
        await Shell.Current.GoToAsync("///HomePage");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is AddKey addKey)
        {
            // Delete the file.
            if (File.Exists(addKey.FilenamePath))
                File.Delete(addKey.FilenamePath);
        }

        await Shell.Current.GoToAsync("../..");
    }
    private void LoadSecretKey(string fileNamePath)
    {
        AddKey secretKeyModel = new AddKey();
        // 使用Path.GetFileName方法获取文件名
        if (File.Exists(fileNamePath))
        {
            secretKeyModel.SecretKey = File.ReadAllText(fileNamePath);
            secretKeyModel.Filename = Path.GetFileName(fileNamePath);
            secretKeyModel.FilenamePath = fileNamePath;
        }
        BindingContext = secretKeyModel;

    }

    public string ItemId
    {
        set {  
            LoadSecretKey(value);
        } // 给私有字段itemId赋值，并调用LoadOtpcode方法
    }
}
