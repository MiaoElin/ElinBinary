using System.Runtime.InteropServices;
namespace ElinBinary {
    [StructLayout(LayoutKind.Explicit)]
    public struct Bit64 {
        [FieldOffset(0)]
        public long longValue;
        [FieldOffset(0)]
        public ulong ulongValue;
        [FieldOffset(0)]
        public double doubleValue;
    }
}