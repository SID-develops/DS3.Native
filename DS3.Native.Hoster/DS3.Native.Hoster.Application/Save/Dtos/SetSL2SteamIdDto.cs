namespace DS3.Native.Hoster.Application.Save.Dtos
{
    /// <summary>
    /// 更新SL2文件SteamId Dto
    /// </summary>
    public class SetSL2SteamIdDto
    {
        /// <summary>
        /// 目标SL2文件路径
        /// </summary>
        public string SL2FilePath { get; set; }

        /// <summary>
        /// 目标SteamId
        /// </summary>
        public uint SteamId { get; set; }

        /// <summary>
        /// 需要写入新文件? 为空字符串或者null则写入原始路径
        /// </summary>
        public string? NewSL2FilePath { get; set; }
    }
}