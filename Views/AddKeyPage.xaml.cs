using CommunityToolkit.Maui.Alerts;
using DragonKey.Models.core;
namespace DragonKey.Views;
//[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class AddKeyPage : ContentPage
{
	public AddKeyPage()
	{
		InitializeComponent();


        string appDataPath = FileSystem.AppDataDirectory;
        string initFileName = $"账户{Path.GetRandomFileName().Substring(0, 8)}";

        LoadSecretKey(Path.Combine(appDataPath, initFileName));
    }
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is AddKey addkey)
        {
            File.WriteAllText(addkey.FilenamePath, SecretkeyEditor.Text);
            try
            {
                // 尝试使用File.Move方法
                File.Move(addkey.FilenamePath, $"{FileSystem.AppDataDirectory}"+ $"/{FilenameEditor.Text}", false);
                    if (addkey.FilenamePath != $"{FileSystem.AppDataDirectory}" + $"/{FilenameEditor.Text}") {
                        // 写入密钥
   //                     await DisplayAlert("🎉", "添加成功", "OK");
   //                     var toast = Toast.Make("添加账户成功");
    //                    await toast.Show();
                        #if __ANDROID__
                          // Show a toast message
                          var toast = Toast.Make("添加账户成功");
                          await toast.Show();
                        #elif WINDOWS
                          // Show an alert message
                          await DisplayAlert("🎉", "添加成功", "OK");
                        #endif

                }
                else
                    {
                        await DisplayAlert("⚠️", "账户名已存在，将保存为默认账户名", "OK");
                        #if __ANDROID__
                            var toast = Toast.Make("保存成功");
                            await toast.Show();
                        #endif
                        
                    }
            }
            catch (IOException ex)
            {
                // 处理异常，例如显示错误信息或者执行其他操作
                await DisplayAlert("⚠️", "账户名已存在，将保存为默认账户名", "OK");
                #if __ANDROID__
                    var toast = Toast.Make("保存成功");
                    await toast.Show();
                #endif
   //             Console.WriteLine(ex.Message);
            }
        }
        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is AddKey addKey)
        {
            // Delete the file.
            if (File.Exists(addKey.Filename))
                File.Delete(addKey.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }
    private void LoadSecretKey(string fileNamePath)
    {
        AddKey secretKeyModel = new AddKey();
        // 假设有一个txt文件的路径
 //       string filePath = "C:\\Users\\Documents\\test.txt";

        // 使用Path.GetFileName方法获取文件名
        string fileName = Path.GetFileName(fileNamePath);
        secretKeyModel.FilenamePath = fileNamePath;

        if (File.Exists(fileName))
        {
            secretKeyModel.Date = File.GetCreationTime(fileName);
            secretKeyModel.SecretKey = File.ReadAllText(fileName);
            secretKeyModel.Filename = Path.GetFileName(fileNamePath);
            secretKeyModel.FilenamePath = fileNamePath;
        }

        BindingContext = secretKeyModel;

    }
    //public string ItemId
    //{
    //    set { LoadNote(value); }
    //}
}
