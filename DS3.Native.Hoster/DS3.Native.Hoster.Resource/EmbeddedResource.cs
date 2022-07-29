using System.Reflection;

namespace DS3.Native.Hoster.Resources
{
    public class EmbeddedResource
    {
        private static IFileProvider _fileProvider = null;

        static EmbeddedResource()
        {
            _fileProvider = FS.GetEmbeddedFileProvider(Assembly.GetAssembly(typeof(EmbeddedResource)));
        }

        public static IFileProvider GetFileProvider()
        {
            return _fileProvider;
        }

        public static string GetFullFilePath(string path)
        {
            return Path.Combine(@"/DS3.Native.Hoster.Core/Resources/", path);
        }
    }
}
