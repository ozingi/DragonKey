namespace DragonKey.Models.core;

internal class About
{
    public string Title => AppInfo.Name;
    public string Version => AppInfo.VersionString;
    public string GithubUrl => "https://github.com/ozingi/DragonKey";
    public string Message => "DragonKey是用.NET MAUI写着玩的，目前很简陋，只能当作个2FA认证应用，比如 Binance、OKX、Bitfinex、Bittrex等的二次认证。但未来DragonKey将不仅仅是密码管理器...";

}
