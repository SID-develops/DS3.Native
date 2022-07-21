using System.Runtime.InteropServices;

namespace DS3.Native.Extensions
{
    public class PinnedObject : IDisposable
    {
        private object _obj;
        private int _size;

        private GCHandle _handle;
        public IntPtr Address { get => _handle.AddrOfPinnedObject(); }

        public PinnedObject(object obj)
        {
            _obj = obj;
            _handle = GCHandle.Alloc(_obj, GCHandleType.Pinned);
        }

        public void Dispose()
        {
            if (_handle.IsAllocated)
                _handle.Free();
        }

        ~PinnedObject()
        {
            Dispose();
        }
    }

    public static class PinnedObjectExtension
    {
        public static PinnedObject Pinned(this object obj)
        {
            return new PinnedObject(obj);
        }
    }
}
