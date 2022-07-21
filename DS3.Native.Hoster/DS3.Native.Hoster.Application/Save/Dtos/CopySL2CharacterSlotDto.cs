namespace DS3.Native.Hoster.Application.Save.Dtos
{
    /// <summary>
    /// 复制角色槽 Dto
    /// </summary>
    public class CopySL2CharacterSlotDto
    {
        /// <summary>
        /// 源SL2文件路径
        /// </summary>
        public string SourceSL2FilePath { get; set; }

        /// <summary>
        /// 源SL2槽位索引
        /// </summary>
        public int SourceIndex { get; set; }

        /// <summary>
        /// 目标SL2文件路径
        /// </summary>
        public string DestinationSL2FilePath { get; set; }

        /// <summary>
        /// 目标SL2槽位索引
        /// </summary>
        public int DestinationIndex { get; set; }

        /// <summary>
        /// 目标SL2槽位被占用时, 是否强制复制
        /// </summary>
        public bool ForceCopy { get; set; }
    }
}