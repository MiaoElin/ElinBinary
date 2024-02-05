using System.Runtime.InteropServices;
namespace ElinBinary {
    //  结构布局（布局类型.替代）
    [StructLayout(LayoutKind.Explicit)]
    public struct Bit32 {
        [FieldOffset(0)]
        public int intValue;
        [FieldOffset(0)]
        public uint uintValue;
        [FieldOffset(0)]
        public float floatValue;
        

    }
}
