using DS3.Native.Extensions;

namespace DS3.Native.DS3
{
    public class DS3MenuFile : BND4File
    {
        public DS3MenuFile(DS3_SL2 save) : base(save, 10)
        {
        }

        internal int SteamIdOffset { get => 0x8; }


        public uint SteamId
        {
            get => _datas.ReadT<uint>(SteamIdOffset);
            set
            {
                _datas.WrtieT<uint>(value, SteamIdOffset);
                foreach (var saveFile in _sl2.SaveFiles.Where(it => it.OccupiedSlot == 1))
                    saveFile.SteamId = value;
            }
        }
    }
}
