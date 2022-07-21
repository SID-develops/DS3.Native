using System.Diagnostics;

namespace DS3.Native.DS3
{
    public static class DS3Loader
    {
        private static string _psName = "DarkSoulsIII";
        private static string _psMutex = "\\BaseNamedObjects\\DarkSoulsIIIMutex";
        private static string MachinePrivateIp = NetUtils.GetMachineIPv4(false);
        private static string MachinePublicIp = NetUtils.GetMachineIPv4(true);

        private static void KillNamedMutexIfExists()
        {
            var processes = Process.GetProcessesByName(_psName);
            if (processes != null && processes.Length > 0)
                foreach (var process in processes)
                    WinAPIProcesses.KillMutex(process, _psMutex);
        }

        private static string ResolveConnectIp(string hostName, string privateHostName)
        {
            string connectionHostName = hostName;
            string connectionHostNameIp = NetUtils.HostnameToIPv4(connectionHostName);
            string privateHostNameIp = NetUtils.HostnameToIPv4(privateHostName);

            if (connectionHostNameIp == MachinePublicIp)
            {
                if (privateHostNameIp == MachinePrivateIp)
                {
                    connectionHostName = "127.0.0.1";
                }
                else
                {
                    connectionHostName = privateHostName;
                }
            }

            return connectionHostName;
        }

        public static uint Launch(string gamePath, string hostName,string publicKey)
        {
            if (publicKey == null || publicKey.Length == 0)
                throw new Exception("服务器公钥为空");
            KillNamedMutexIfExists();

            var serverPathData = PatchingUtils.MakeEncryptedServerInfo(hostName, publicKey);
            if (serverPathData == null)
                throw new Exception("生成服务器信息失败.");

            DarkSoulsLoadConfig loadConfig;
            if (!BuildConfig.ExeLoadConfiguration.TryGetValue(ExeUtils.GetExeSimpleHash(gamePath), out loadConfig))
                throw new Exception("无法检测游戏版本信息.");

            string exeDir = Path.GetDirectoryName(gamePath);

            string appIdFile = Path.Combine(exeDir, "steam_appid.txt");
            if (!File.Exists(appIdFile))
                File.WriteAllText(appIdFile, BuildConfig.SteamAppId.ToString());

            var psStartInfo = new STARTUPINFO();
            var psInfo = new PROCESS_INFORMATION();

            var createSucceed = WinAPI.CreateProcess(
                null,
                "\"" + gamePath + "\"",
                IntPtr.Zero,
                IntPtr.Zero,
                false,
                ProcessCreationFlags.CREATE_SUSPENDED,
                IntPtr.Zero,
                exeDir,
                ref psStartInfo,
                out psInfo
            );

            if (!createSucceed)
                throw new Exception($"游戏无法启动,请检查游戏路径: {gamePath}.");

            int patchedLen;
            var patchSucceed = WinAPI.WriteProcessMemory(psInfo.hProcess, (IntPtr)loadConfig.ServerInfoAddress, serverPathData, (uint)serverPathData.Length, out patchedLen);
            if (!patchSucceed || patchedLen != serverPathData.Length)
            {
                WinAPI.ResumeThread(psInfo.hThread);
                WinAPI.TerminateProcess(psInfo.hProcess, -1);
                throw new Exception("无法写入服务器信息, 请确认启动器是有足够的权限.");
            }

            WinAPI.ResumeThread(psInfo.hThread);

            return psInfo.dwProcessId;
        }
    }
}
