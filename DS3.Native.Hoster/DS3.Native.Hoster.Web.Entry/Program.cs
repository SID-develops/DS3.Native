using DS3.Native.Hoster.Resources;
using Furion;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Diagnostics;
using System.Reflection;

try
{
    var eventLoop = true;

    var serviceName = "DS3药丸启动器本地驻守服务";
    var mutexName = "__DS3_Native_Hoster__";

    if (Mutex.TryOpenExisting(mutexName, out var singletonMutex))
        return;

    singletonMutex = new Mutex(true, mutexName);

    Serve.Run(RunOptions.DefaultSilence, urls: $"http://127.0.0.1:0");

    var firstUrl = string.Empty;

    while (string.IsNullOrWhiteSpace((firstUrl = App.GetService<IServer>()?.Features?.Get<IServerAddressesFeature>()?.Addresses?.FirstOrDefault())))
        Thread.Sleep(10);

    var trayIcon = new NotifyIcon()
    {
        Icon = new Icon(EmbeddedResource.GetFileProvider().GetFileInfo("/Images/Icons/tray_icon.ico").CreateReadStream()),
        Text = $"{serviceName}",
        Visible = true,
        ContextMenuStrip = new ContextMenuStrip()
    };

    var openItem = new EventHandler((s, e) =>
    {
        Process.Start("explorer", firstUrl);
    });

    var exitItem = new EventHandler((s, e) =>
    {
        eventLoop = false;
    });

    trayIcon.MouseClick += new MouseEventHandler((s, e) =>
    {
        if (e.Clicks == 0 && e.Button == MouseButtons.Left)
            openItem(s, e);
    });

    trayIcon.ContextMenuStrip.Items.Add($"本次监听链接: {firstUrl}", null, null).Enabled = false;
    trayIcon.ContextMenuStrip.Items.Add($"-", null, null);
    trayIcon.ContextMenuStrip.Items.Add("打开界面", null, openItem);
    trayIcon.ContextMenuStrip.Items.Add("退出", null, exitItem);

    trayIcon.ShowBalloonTip(3, serviceName, $"欢迎您使用药丸启动器\n本次监听地址: {firstUrl}", ToolTipIcon.Info);

    // Serve.Run(silence:true);

    while (eventLoop)
    {
        Thread.Sleep(10);
        Application.DoEvents();
    }
}
catch (Exception ex)
{
    var errFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), $"Exception_{DateTimeOffset.Now.Ticks}.txt");
    File.WriteAllText(errFile, ex.StackTrace);
    MessageBox.Show($"启动失败, 错误已写入文件: {errFile}\n请将文件提交至官方, 我们将会及时处理.");
}