using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace DS3.Native.Hoster.Resources
{
    public class EmbeddedResource
    {
        private static IFileProvider _fileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

        public static IFileProvider GetFileProvider()
        {
            return _fileProvider;
        }
    }
}
