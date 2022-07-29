using DS3.Native.DS3;
using DS3.Native.Hoster.Application.Loader.Dtos;

namespace DS3.Native.Hoster.Application.Loader
{
    public class LoaderService : IDynamicApiController
    {
        /// <summary>
        /// 获取Steam是否运行且登陆
        /// </summary>
        /// <returns>返回true或者false</returns>

        [HttpGet]
        public Task<bool> IsSteamRunningAndLoggedIn()
        {
            return Task.FromResult(SteamUtils.IsSteamRunningAndLoggedIn());
        }

        /// <summary>
        /// 获取黑魂3游戏路径
        /// </summary>
        /// <returns>游戏路径, 未获取到为空字符串</returns>
        public Task<string> GetGamePath()
        {
            var path = SteamUtils.GetGameInstallPath("DARK SOULS III") + @"\Game\DarkSoulsIII.exe";
            if (!File.Exists(path))
                throw new Exception("游戏路径未找到.");
            return Task.FromResult(path);
        }

        /// <summary>
        /// 启动游戏
        /// </summary>
        /// <param name="dto">参数</param>
        /// <returns>成功则返回进程ID否则为空</returns>
        public Task<uint> LaunchGame(LaunchGameDto dto)
        {
            return Task.FromResult(DS3Loader.Launch(dto.GameExePath, dto.HostName, dto.HostPublicKey));
        }
    }
}