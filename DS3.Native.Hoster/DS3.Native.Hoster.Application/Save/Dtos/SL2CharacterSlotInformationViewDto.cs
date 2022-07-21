namespace DS3.Native.Hoster.Application.Save.Dtos
{
    /// <summary>
    /// SL2角色槽信息
    /// </summary>
    public class SL2CharacterSlotInformationViewDto
    {
        /// <summary>
        /// 角色槽索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 角色游玩时间
        /// </summary>
        public int PlayTimeSeconds { get; set; }
    }
}