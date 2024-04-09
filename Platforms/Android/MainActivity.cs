using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Hosting;

namespace DragonKey;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]

public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        Window.SetNavigationBarColor(Colors.White.ToAndroid());
        Window.SetStatusBarColor(Colors.White.ToAndroid());
        Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar; // 设置状态栏文字为黑色
                                                                                                 // 注册自定义的渲染器解析器


        base.OnCreate(savedInstanceState);
    }
}
