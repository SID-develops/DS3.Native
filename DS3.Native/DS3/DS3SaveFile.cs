using DS3.Native.Extensions;
using SoulsFormats;

namespace DS3.Native.DS3
{
    public class DS3SaveFile : BND4File
    {
        public DS3SaveFile(DS3_SL2 save, int idx) : base(save, idx)
        {
        }

        internal int OccupiedSlotsOffset { get => 0x1098; }
        internal int SaveSlotMenuDataLen { get => 0x22A; }
        internal int SaveSlotMenuDataOffset { get => 0x10A2; }
        internal int CurrentSaveSlotMenuDataOffset { get => SaveSlotMenuDataOffset + SaveSlotMenuDataLen * _idx; }
        internal int CharacterNameLen { get => 0x20; }
        internal int CharacterNameOffset { get => CurrentSaveSlotMenuDataOffset; }
        internal int LevelOffset { get => CharacterNameOffset + CharacterNameLen + 0x2; }
        internal int PlaytimeSecondsOffset { get => LevelOffset + 0x4; }
        internal int SteamIdOffset { get => _datas.ReadT<int>(0x58) + 0x6F; }

        public byte OccupiedSlot
        {
            get => _sl2.MenuFile._datas.ReadT<byte>(OccupiedSlotsOffset + _idx);
            set => _sl2.MenuFile._datas.WrtieT<byte>(value,OccupiedSlotsOffset + _idx);
        }

        public string CharacterName
        {
            get
            {
                var bytes = _sl2.MenuFile._datas.ReadByteArray(CharacterNameLen, CharacterNameOffset);
                int terminator;
                for (terminator = 0; terminator < bytes.Length; terminator += 2)
                {
                    // If length is odd (which it really shouldn't be), avoid indexing out of the array and align the terminator to the end
                    if (terminator == bytes.Length - 1)
                        terminator--;
                    else if (bytes[terminator] == 0 && bytes[terminator + 1] == 0)
                        break;
                }
                return SFEncoding.UTF16.GetString(bytes, 0, terminator);
            }
            set
            {
                var bytes = SFEncoding.UTF16.GetBytes(value);
                var data = new byte[CharacterNameLen];
                Array.Copy(bytes, data, bytes.Length > CharacterNameLen ? CharacterNameLen : bytes.Length);
                _sl2.MenuFile._datas.WriteByteArray(data, CharacterNameOffset);
            }
        }

        public int Level
        {
            get => _sl2.MenuFile._datas.ReadT<int>(LevelOffset);
        }

        public int PlayTimeSeconds
        {
            get => _sl2.MenuFile._datas.ReadT<int>(PlaytimeSecondsOffset);
        }

        public uint SteamId
        {
            get => _datas.ReadT<uint>(SteamIdOffset);
            set => _datas.WrtieT<uint>(value, SteamIdOffset);
        }

        public void CopyDataFrom(DS3SaveFile saveFile)
        {
            _datas = saveFile._datas;

            _sl2.MenuFile._datas.WriteByteArray(
                saveFile._sl2.MenuFile._datas.ReadByteArray(saveFile.SaveSlotMenuDataLen, saveFile.CurrentSaveSlotMenuDataOffset),
                CurrentSaveSlotMenuDataOffset);
            OccupiedSlot = saveFile.OccupiedSlot;
            SteamId = _sl2.MenuFile.SteamId;
        }
    }
}
