using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

namespace ElinBinary {
    public static class BinaryWriter {
        // sbyte sbyte[]  byte byte[]
        // bool bool[]
        // char char[]
        // short short[] ushort ushort[]
        // int int[] uint uint[]
        // long long[] ulong ulong[]
        // string string[]
        // float float[] double double[]
        // decimal decimal[]
        public static void WriteSbyte(byte[] buffer, sbyte value, ref int index) {
            buffer[index] = (byte)value;
            index++;
        }
        public static void WriteSbyteArray(byte[] buffer, sbyte[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                WriteSbyte(buffer, values[i], ref index);
                // var value = values[i];
                // buffer[index] = (byte)(value >> i * 8);
                // index++;
            }
        }
        public static void WriteBool(byte[] buffer, bool value, ref int index) {
            if (value == true) {
                buffer[index] = 1;
            } else if (value == false) {
                buffer[index] = 0;
            }
            index++;
        }
        public static void WriteBoolArray(byte[] buffer, bool[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                WriteBool(buffer, values[i], ref index);
            }
        }
        public static void WriteChar(byte[] buffer, char value, ref int index) {
            buffer[index] = Convert.ToByte(value);
            index++;
        }
        public static void WriteCharArray(byte[] buffer, char[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                WriteChar(buffer, values[i], ref index);
            }
        }

        public static void WriteByte(byte[] buffer, byte value, ref int index) {
            buffer[index] = value;
            index++;
        }
        public static void WriteByteArray(byte[] buffer, byte[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                buffer[index] = values[i];
                index++;
            }
        }
        public static void WriteShort(byte[] buffer, short value, ref int index) {
            buffer[index] = (byte)(value >> 0);
            index++;
            buffer[index] = (byte)(value >> 8);
            index++;
        }
        public static void WriteShortArray(byte[] buffer, short[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                WriteShort(buffer, values[i], ref index);
            }
        }
        public static void WriteUshort(byte[] buffer, ushort value, ref int index) {
            buffer[index] = (byte)(value >> 0);
            index++;
            buffer[index] = (byte)(value >> 8);
            index++;
        }
        public static void WriteUshortArray(byte[] buffer, ushort[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                // var value = values[i];
                // buffer[index] = (byte)(value >> 0 + 16 * i);
                // index++;
                // buffer[index] = (byte)(value >> 8 + 16 * i);
                // index++;
                WriteUshort(buffer, values[i], ref index);
            }

        }
        public static void WriteInt(byte[] buffer, int value, ref int index) {
            buffer[index] = (byte)(value >> 0); // 数据左移0个0 （移动是以1bit为单位的）
            index++;
            buffer[index] = (byte)(value >> 8); // 数据左移8个0
            index++;
            buffer[index] = (byte)(value >> 16);
            index++;
            buffer[index] = (byte)(value >> 24);
            index++;
        }
        public static void WriteIntArray(byte[] buffer, int[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                // buffer[i * 4 + index] =
                // var val = value[i];
                // buffer[index] = (byte)(val >> (0 + i * 32)); // 数据左移0个0 （移动是以1bit为单位的）
                // index++;
                // buffer[index] = (byte)(val >> (8 + i * 32)); // 数据左移8个0
                // index++;
                // buffer[index] = (byte)(val >> (16 + i * 32));
                // index++;
                // buffer[index] = (byte)(val >> (24 + i * 32));
                // index++;
                WriteInt(buffer, values[i], ref index);
            }
        }
        public static void WriteUint(byte[] buffer, uint value, ref int index) {
            buffer[index] = (byte)(value >> 0);
            index++;
            buffer[index] = (byte)(value >> 8);
            index++;
            buffer[index] = (byte)(value >> 16);
            index++;
            buffer[index] = (byte)(value >> 24);
            index++;
        }
        public static void WriteUintArray(byte[] buffer, uint[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                WriteUint(buffer, values[i], ref index);
            }
        }
        public static void WriteLong(byte[] buffer, long value, ref int index) {
            WriteInt(buffer, (int)value, ref index);
            WriteInt(buffer, (int)value >> 32, ref index);
        }
        public static void WriteLongArray(byte[] buffer, long[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                WriteLong(buffer, values[i], ref index);
            }
        }
        public static void WriteUlong(byte[] buffer, ulong value, ref int index) {
            WriteUint(buffer, (uint)value, ref index);
            WriteUint(buffer, (uint)value >> 32, ref index);
        }
        public static void WriteUlongArray(byte[] buffer, ulong[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                WriteUlong(buffer, values[i], ref index);
            }
        }
        public static void WriteString(byte[] buffer, string value, ref int index) {
            char[] values = value.ToCharArray();
            WriteCharArray(buffer, values, ref index);

        }
        public static void WriteStringArray(byte[] buffer, string[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                WriteString(buffer, values[i], ref index);
            }
        }
        public static void WriteFloat(byte[] buffer, float value, ref int index) {
            // 官方接口
            // byte[] values = new byte[4];
            // values = BitConverter.GetBytes(value);
            // // WriteByteArray(buffer, values, ref index);
            // for (int i = 0; i < 4; i++) {
            //     WriteByte(buffer, values[i], ref index);
            // }
            Bit32 bit32 = new Bit32();
            bit32.floatValue = value;
            WriteInt(buffer, bit32.intValue, ref index);

        }
        public static void WriteFloatArray(byte[] buffer, float[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                WriteFloat(buffer, values[i], ref index);
            }
        }
        public static void WriteDouble(byte[] buffer, double value, ref int index) {
            Bit64 bit = new Bit64();
            bit.doubleValue = value;
            WriteLong(buffer, bit.longValue, ref index);
        }
        public static void WtiteDoubleArray(byte[] buffer, double[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for (int i = 0; i < values.Length; i++) {
                WriteDouble(buffer, values[i], ref index);
            }
        }
        public static void WriteDemical(byte[] buffer, decimal value, ref int index) {
            Bit128 bit = new Bit128();
            bit.decimalValues = value;
            WriteByte(buffer, bit.b0, ref index);
            WriteByte(buffer, bit.b1, ref index);
            WriteByte(buffer, bit.b2, ref index);
            WriteByte(buffer, bit.b3, ref index);
            WriteByte(buffer, bit.b4, ref index);
            WriteByte(buffer, bit.b5, ref index);
            WriteByte(buffer, bit.b6, ref index);
            WriteByte(buffer, bit.b7, ref index);
            WriteByte(buffer, bit.b8, ref index);
            WriteByte(buffer, bit.b9, ref index);
            WriteByte(buffer, bit.b10, ref index);
            WriteByte(buffer, bit.b11, ref index);
            WriteByte(buffer, bit.b12, ref index);
            WriteByte(buffer, bit.b13, ref index);
            WriteByte(buffer, bit.b14, ref index);
            WriteByte(buffer, bit.b15, ref index);
        }
        public static void WriteDemicalArray(byte[] buffer, decimal[] values, ref int index) {
            WriteUshort(buffer, (ushort)values.Length, ref index);
            for(int i=0;i<values.Length;i++){
                WriteDemical(buffer,values[i],ref index);
            }
        }
    }
}