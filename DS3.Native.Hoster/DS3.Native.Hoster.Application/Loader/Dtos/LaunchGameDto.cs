namespace DS3.Native.Hoster.Application.Loader.Dtos
{
    /// <summary>
    /// 启动游戏 Dto
    /// </summary>
    public class LaunchGameDto
    {
        /// <summary>
        /// 游戏Exe文件路径
        /// </summary>
        public string GameExePath { get; set; }

        /// <summary>
        /// 要连接的服务器主机名
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 要连接的服务器公钥
        /// </summary>
        public string HostPublicKey { get; set; }
    }
}