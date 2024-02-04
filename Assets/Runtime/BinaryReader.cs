using System;
namespace ElinBinary {
    public static class BinaryReader {
        // sbyte sbyte[]  byte byte[]
        // bool bool[]
        // char char[]
        // short short[] ushort ushort[]
        // int int[] uint uint[]
        // long long[] ulong ulong[]
        // float float[] double double[]
        // decimal decimal[]
        // string string[]
        public static sbyte ReadSbyte(byte[] buffer, ref int index) {
            sbyte value = (sbyte)buffer[index];
            return value;
        }
        public static sbyte[] ReadSbyteArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            sbyte[] values = new sbyte[length];
            for (int i = 0; i < length; i++) {
                values[i] = ReadSbyte(buffer, ref index);
            }
            return values;
        }
        public static bool ReadBool(byte[] buffer, ref int index) {
            byte value = buffer[index];
            if (value == 0) {
                return false;
            }
            return true;
        }
        public static bool[] ReadBoolArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            bool[] values = new bool[length];
            for (int i = 0; i < length; i++) {
                values[i] = ReadBool(buffer, ref index);
            }
            return values;
        }
        public static char ReadChar(byte[] buffer, ref int index) {
            char value = Convert.ToChar(buffer[index]);
            index++;
            return value;
        }
        public static char[] ReadCharArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            char[] values = new char[length];
            for (int i = 0; i < length; i++) {
                values[i] = ReadChar(buffer, ref index);
            }
            return values;
        }
        public static short ReadShort(byte[] buffer, ref int index) {
            short value = 0;
            value = (short)(buffer[index] << 0);
            index++;
            value = (short)(value | buffer[index] << 8);
            index++;
            return value;
        }
        public static ushort ReadUshort(byte[] buffer, ref int index) {
            ushort value = 0;
            value = (ushort)(buffer[index] << 0);
            index++;
            value = (ushort)(value | buffer[index] << 8);
            index++;
            return value;
        }
        public static ushort[] ReadUshortArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            ushort[] values = new ushort[length];
            for (int i = 0; i < length; i++) {
                values[i] = ReadUshort(buffer, ref index);
            }
            return values;
        }
        public static int ReadInt(byte[] buffer, ref int index) {
            int value = 0;
            value = buffer[index] << 0;
            index++;
            value = value | buffer[index] << 8;
            index++;
            value = value | buffer[index] << 16;
            index++;
            value = value | buffer[index] << 24;
            index++;
            return value;
        }
        public static int[] ReadIntArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            int[] values = new int[length];
            for (int i = 0; i < length; i++) {
                values[i] = ReadInt(buffer, ref index);
            }
            return values;
        }
        public static uint ReadUint(byte[] buffer, ref int index) {
            uint value = 0;
            value = (uint)buffer[index] << 0;
            index++;
            value = value | (uint)buffer[index] << 8;
            index++;
            return value;
        }
        public static uint[] ReadUintArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            uint[] values = new uint[length];
            for (int i = 0; i < length; i++) {
                ReadUint(buffer, ref index);
            }
            return values;
        }
        public static long ReadLong(byte[] buffer, ref int index) {
            long value = ReadUint(buffer, ref index);
            value = value | ReadUint(buffer, ref index) << 32;
            return value;
        }
        public static long[] ReadLongArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            long[] values = new long[length];
            for (int i = 0; i < length; i++) {
                values[i] = ReadLong(buffer, ref index);
            }
            return values;
        }
        public static ulong ReadUlong(byte[] buffer, ref int index) {
            ulong value = ReadUint(buffer, ref index);
            value = value | ReadUint(buffer, ref index) << 32;
            return value;
        }
        public static ulong[] ReadUlongArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            ulong[] values = new ulong[length];
            for (int i = 0; i < length; i++) {
                values[i] = ReadUlong(buffer, ref index);
            }
            return values;
        }
        public static string ReadString(byte[] buffer, ref int index) {
            char[] values = ReadCharArray(buffer, ref index);
            return values.ToString();
        }
    }
}