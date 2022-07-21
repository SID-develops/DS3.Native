using SoulsFormats;

namespace DS3.Native.DS3
{
    public class BND4File
    {
        internal DS3_SL2 _sl2;
        internal int _idx;
        internal byte[] _datas;

        public BND4File(DS3_SL2 sl2, int idx)
        {
            _sl2 = sl2;
            _idx = idx;
            _datas = Decrypt(_sl2._sl2.Files[_idx]);
        }

        public virtual void Save()
        {
            _sl2._sl2.Files[_idx].Bytes = Encrypt(_datas);
        }

        internal static byte[] Decrypt(BinderFile file)
        {
            return SFUtil.DecryptSL2File(file.Bytes, SFUtil.GetDS3SaveKey());
        }

        internal static byte[] Encrypt(byte[] bytes)
        {
            return SFUtil.EncryptSL2File(bytes, SFUtil.GetDS3SaveKey());
        }
    }
}
