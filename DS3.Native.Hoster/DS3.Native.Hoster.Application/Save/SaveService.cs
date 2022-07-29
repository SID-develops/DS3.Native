using DS3.Native.DS3;
using DS3.Native.Hoster.Application.Save.Dtos;

namespace DS3.Native.Hoster.Application.Save
{
    public class SaveService : IDynamicApiController
    {
        /// <summary>
        /// 获取SL2文件信息
        /// </summary>
        /// <param name="sl2FilePath">SL2文件路径</param>
        /// <returns>SL2文件信息</returns>
        /// <exception cref="Exception"></exception> 
        public Task<SL2InformationViewDto> GetSL2Information(string sl2FilePath)
        {
            if (!File.Exists(sl2FilePath))
                throw new Exception("无法找到SL2文件!");

            var sl2 = new DS3_SL2(sl2FilePath);

            var result = new SL2InformationViewDto() 
            { 
                SteamId = sl2.MenuFile.SteamId,
                CharacterSlotInformations = new List<SL2CharacterSlotInformationViewDto> { } 
            };

            for (var i = 0; i < 10; i++)
            {
                var saveFile = sl2.SaveFiles[i];
                if (saveFile.OccupiedSlot != 1)
                    result.CharacterSlotInformations.Add(null);
                else
                    result.CharacterSlotInformations.Add(new SL2CharacterSlotInformationViewDto
                    {
                        Index = i,
                        Name = saveFile.CharacterName,
                        Level = saveFile.Level,
                        PlayTimeSeconds = saveFile.PlayTimeSeconds
                    });
            }

            return Task.FromResult(result);
        }

        /// <summary>
        /// 设置SL2文件SteamId
        /// </summary>
        /// <param name="dto">参数</param>
        /// <returns>原始SteamId</returns>
        /// <exception cref="Exception"></exception>
        public Task<uint> SetSL2SteamId(SetSL2SteamIdDto dto)
        {
            if (!File.Exists(dto.SL2FilePath))
                throw new Exception("无法找到SL2文件!");

            var sl2 = new DS3_SL2(dto.SL2FilePath);
            var originalSteamId = sl2.MenuFile.SteamId;
            sl2.MenuFile.SteamId = dto.SteamId;
            sl2.Save(dto.NewSL2FilePath);
            return Task.FromResult(originalSteamId);
        }

        /// <summary>
        /// 复制SL2文件角色槽信息
        /// </summary>
        /// <param name="dto">参数</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task CopySL2CharacterSlot(CopySL2CharacterSlotDto dto)
        {
            if (!File.Exists(dto.SourceSL2FilePath))
                throw new Exception("无法找到源SL2文件!");
            if (!File.Exists(dto.DestinationSL2FilePath))
                throw new Exception("无法找到目标SL2文件!");

            var sourceSL2 = new DS3_SL2(dto.SourceSL2FilePath);
            var destinationSL2 = new DS3_SL2(dto.DestinationSL2FilePath);

            var sourceSaveFile = sourceSL2.SaveFiles[dto.SourceIndex];
            if (sourceSaveFile.OccupiedSlot != 1)
                throw new Exception("源角色槽并未使用.");
            var destinationSaveFile = destinationSL2.SaveFiles[dto.DestinationIndex];

            if (!dto.ForceCopy && destinationSaveFile.OccupiedSlot == 1)
                throw new Exception("目标角色槽已经使用, 且并未开启强制复制, 取消操作!");

            destinationSaveFile.CopyDataFrom(sourceSaveFile);
            destinationSL2.Save();
            return Task.CompletedTask;
        }
    }
}