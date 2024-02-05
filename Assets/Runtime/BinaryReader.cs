using System;
using System.Text;
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
        public static byte ReadByte(byte[] buffer, ref int index) {
            byte value = (byte)buffer[index];
            return value;
        }
        public static byte[] ReadByteArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            byte[] values = new byte[length];
            for (int i = 0; i < length; i++) {
                values[i] = ReadByte(buffer, ref index);
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
            // char[] values = ReadCharArray(buffer, ref index);
            // string value = new String(values);
            // string value = "";
            // for (int i = 0; i < values.Length; i++) {
            //     value = value + values[i];
            // }
            ushort length = ReadUshort(buffer, ref index);
            string value = Encoding.UTF8.GetString(buffer, index, length);
            return value;
        }
        public static string[] ReadStringArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            string[] values = new string[length];
            for (int i = 0; i < length; i++) {
                values[i] = ReadString(buffer, ref index);
            }
            return values;
        }
        public static float ReadFloat(byte[] buffer, ref int index) {
            // 官方接口
            // byte[] values = new byte[4];
            // for (int i = 0; i < 4; i++) {
            //     values[i] = buffer[index];
            //     index++;
            // }
            // float value = BitConverter.ToSingle(values, 0);
            // return value;
            int value = ReadInt(buffer, ref index);
            Bit32 bit = new Bit32();
            bit.intValue = value;
            return bit.floatValue;
        }
        public static float[] ReadFloatArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            float[] values = new float[length];
            for (int i = 0; i < length; i++) {
                values[i] = ReadFloat(buffer, ref index);
            }
            return values;
        }
        public static double ReadDouble(byte[] buffer, ref int index) {
            Bit64 bit = new Bit64();
            bit.longValue = ReadLong(buffer, ref index);
            return bit.doubleValue;
        }
        public static double[] ReadDoubleArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            double[] values = new double[length];
            for (int i = 0; i < length; i++) {
                values[i] = ReadDouble(buffer, ref index);
            }
            return values;
        }
        public static decimal ReadDemical(byte[] buffer, ref int index) {
            Bit128 bit = new Bit128();
            bit.b0 = ReadByte(buffer, ref index);
            bit.b1 = ReadByte(buffer, ref index);
            bit.b2 = ReadByte(buffer, ref index);
            bit.b3 = ReadByte(buffer, ref index);
            bit.b4 = ReadByte(buffer, ref index);
            bit.b5 = ReadByte(buffer, ref index);
            bit.b6 = ReadByte(buffer, ref index);
            bit.b7 = ReadByte(buffer, ref index);
            bit.b8 = ReadByte(buffer, ref index);
            bit.b9 = ReadByte(buffer, ref index);
            bit.b10 = ReadByte(buffer, ref index);
            bit.b11 = ReadByte(buffer, ref index);
            bit.b12 = ReadByte(buffer, ref index);
            bit.b13 = ReadByte(buffer, ref index);
            bit.b14 = ReadByte(buffer, ref index);
            bit.b15 = ReadByte(buffer, ref index);
            return bit.decimalValues;
        }
        public static decimal[] ReadDemicalArray(byte[] buffer, ref int index) {
            ushort length = ReadUshort(buffer, ref index);
            decimal[] values = new decimal[length];
            for (int i = 0; i < length; i++) {
                values[i] = ReadDemical(buffer, ref index);
            }
            return values;
        }
    }
}