try
{
    var mutexName = "__DS3_Native_Hoster__";

    if (Mutex.TryOpenExisting(mutexName, out var singletonMutex))
        return;

    singletonMutex = new Mutex(true, mutexName);

    //var trayIcon = new NotifyIcon();
    //trayIcon.Icon = new Icon();

    Serve.Run(RunOptions.Default);
}
catch
{ }