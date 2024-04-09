using System.Reflection.Emit;
using DragonKey.Models.core;
using CommunityToolkit.Maui.Alerts;
using Microsoft.Maui.Dispatching;
using OtpNet;
using static System.Net.WebRequestMethods;
using Microsoft.Maui.Storage;

namespace DragonKey.Views;
[QueryProperty(nameof(Fname), nameof(Fname))]
[QueryProperty(nameof(ItemId), nameof(ItemId))]
[QueryProperty(nameof(Filepath), nameof(Filepath))]

public partial class ViewOtpPage : ContentPage
{
    ViewOtp secretModel = new ViewOtp();

    public ViewOtpPage()
    {
        InitializeComponent();
        StartTimer();
        var toolbarItem = new ToolbarItem { Text = "Update" };
        toolbarItem.IconImageSource = GetPlatformImage("edit_key");
        toolbarItem.Clicked += Update_Clicked;
        ToolbarItems.Add(toolbarItem);
    }
    protected override void OnAppearing()
    {
        
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

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
    private async void Update_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(UpdateKeyPage)}?{nameof(UpdateKeyPage.ItemId)}={Filepath}");
    }

    private int FakecountDown;
    private void StartTimer()
    {
        // Use BindableObject.Dispatcher.StartTimer to start a timer with a one-second interval
        this.Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            FakecountDown--;
            if (BindingContext is ViewOtp test && secretModel.TotpCode !=null )
            {
                ViewOtp secretModel = new ViewOtp();
                secretModel.FakeCountDown = FakecountDown.ToString();
                secretModel.TotpCode = test.TotpCode;
                secretModel.progressbar = double.Parse(secretModel.FakeCountDown) / 30;
                secretModel.Filename = Fname;

                BindingContext = secretModel;
                

                if (FakecountDown <= 0)
                {
                    Task task = LoadOtpcode(ItemId);
                    FakecountDown = 30;

                }
            }
            else
            {
                return false;
            }

            return true;
        });
    }
    private async Task LoadOtpcode(string secretKey)
    {

   //     ViewOtp secretModel = new ViewOtp();
        string otp = await getTotpCode(secretKey);
        secretModel.TotpCode = otp.Substring(0, 6);
        secretModel.CountDown = otp.Substring(6);
        secretModel.Filename = Fname;
        // Use int.Parse method to convert a string to an int
        if (FakecountDown == 0)
        {
            int countDown = int.Parse(secretModel.CountDown);
            // Subtract 2 from the countDown value and assign it to secretModel.FakeCountDown
            secretModel.FakeCountDown = (countDown).ToString();
            FakecountDown = int.Parse(secretModel.FakeCountDown);
        }

        BindingContext = secretModel;

    }

    private async Task<string> getTotpCode(string secretKey)
    {
        string secret = secretKey;
        byte[] base32Bytes = null; // 声明base32Bytes
        try
        {
            base32Bytes = OtpNet.Base32Encoding.ToBytes(secret);
        }
        catch (ArgumentException ex) {
            // 处理异常，例如显示错误信息或者执行其他操作
            await DisplayAlert("⚠️", "保存的密钥存在问题，请检查密钥", "OK");
            #if __ANDROID__
                var toast = Toast.Make("请检查密钥");
                await toast.Show();
            #endif
//            await Navigation.PopAsync(); // 返回上一页
            await Shell.Current.GoToAsync($"{nameof(UpdateKeyPage)}?{nameof(UpdateKeyPage.ItemId)}={Filepath}");
            return null; // 或者返回其他值
        }

        // 使用base32Bytes
        if (base32Bytes != null && base32Bytes.Length > 0) // 判断base32Bytes是否为空或者长度为0
        {
            var otp = new OtpNet.Totp(base32Bytes, step: 30);
            var totpCode = otp.ComputeTotp(DateTime.UtcNow);
            var CountDown = otp.RemainingSeconds();
            return totpCode+CountDown;
        }
        else
        {
            await DisplayAlert("⚠️", "保存的密钥存在问题，请检查密钥", "OK");
            #if __ANDROID__
                var toast = Toast.Make("请检查密钥");
                await toast.Show();
            #endif
 //           await Navigation.PopAsync(); // 返回上一页
            await Shell.Current.GoToAsync($"{nameof(UpdateKeyPage)}?{nameof(UpdateKeyPage.ItemId)}={Filepath}");
            return null; // 或者返回其他值
        }

    }
    private string itemId, itemId1, itemId2;
    public string Fname
    {
        get { return itemId2; } // 返回一个私有字段itemId的值
        set { itemId2 = value; } // 给私有字段itemId赋值，并调用LoadOtpcode方法
    }
    public string ItemId
    {
        get { return itemId1; } // 返回一个私有字段itemId的值
        set { itemId1 = value; Task task = LoadOtpcode(value); } // 给私有字段itemId赋值，并调用LoadOtpcode方法
    }
    public string Filepath
    {
        get { return itemId; } // 返回一个私有字段itemId的值
        set { itemId = value;} // 给私有字段itemId赋值，并调用LoadOtpcode方法
    }

}