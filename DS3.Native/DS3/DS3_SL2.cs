using SoulsFormats;

namespace DS3.Native.DS3
{

    public class DS3_SL2
    {
        internal BND4 _sl2;
        internal string _sl2Path;

        public DS3_SL2(string sl2Path)
        {
            _sl2Path = sl2Path;
            _sl2 = BND4.Read(_sl2Path);


            MenuFile = new DS3MenuFile(this);
            SaveFiles = new DS3SaveFile[10];
            for (var i = 0; i < SaveFiles.Length; i++)
                SaveFiles[i] = new DS3SaveFile(this, i);
        }

        public void Save(string sl2Path = "")
        {

            for (var i = 0; i < SaveFiles.Length; i++)
                SaveFiles[i].Save();
            MenuFile.Save();
            _sl2.Write(string.IsNullOrWhiteSpace(sl2Path) ? _sl2Path : sl2Path);
        }

        public DS3MenuFile MenuFile { get; }
        public DS3SaveFile[] SaveFiles { get; }
    }
}
