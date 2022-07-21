namespace DS3.Native.Hoster.Application.Save.Dtos
{
    /// <summary>
    /// SL2文件信息 Dto
    /// </summary>
    public class SL2InformationViewDto
    {
        /// <summary>
        /// SL2文件SteamId
        /// </summary>
        public uint SteamId { get; set; }

        /// <summary>
        /// SL2文件角色槽信息列表 0-9
        /// </summary>
        public List<SL2CharacterSlotInformationViewDto> CharacterSlotInformations { get; set; }
    }
}