using System.Runtime.InteropServices;

namespace DS3.Native.Extensions
{
    public static class ByteArrayRWExtension
    {
        public static byte[] ReadByteArray(this byte[] datas, int size, long offset = 0)
        {
            var buffer = new byte[size];
            Array.Copy(datas, offset, buffer, 0, size);
            return buffer;
        }

        public static T ReadT<T>(this byte[] datas, long offset = 0) where T : struct
        {
            var size = Marshal.SizeOf<T>();
            var buffer = datas.ReadByteArray(size, offset);
            using (var po = buffer.Pinned())
                return Marshal.PtrToStructure<T>(po.Address);
        }

        public static T[] ReadTArray<T>(this byte[] datas, long arrLen, long offset = 0) where T : struct
        {
            var size = Marshal.SizeOf<T>();
            var buffer = new T[arrLen];
            for (long i = 0; i < arrLen; i++)
                buffer[i] = datas.ReadT<T>(offset + i * size);
            return buffer;
        }

        public static void WriteByteArray(this byte[] datas, byte[] buffer, long offset = 0)
        {
            Array.Copy(buffer, 0, datas, offset, buffer.LongLength);
        }

        public static void WrtieT<T>(this byte[] datas, T buffer, long offset = 0) where T : struct
        {
            var size = Marshal.SizeOf<T>(); ;
            var byteBuffer = new byte[size];
            using (var po = buffer.Pinned())
            {
                Marshal.Copy(po.Address, byteBuffer, 0, size);
                datas.WriteByteArray(byteBuffer, offset);
            }
        }

        public static void WrtieTArray<T>(this byte[] datas, T[] buffer, long offset = 0) where T : struct
        {
            var size = Marshal.SizeOf<T>();
            for (long i = 0; i < buffer.LongLength; i++)
                datas.WrtieT<T>(buffer[i], offset + i * size);
        }
    }
}
