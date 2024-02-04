using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace ElinBinary.sample {
    public class main : MonoBehaviour {
        // Start is called before the first frame update
        void Start() {
            // int a = -1;
            int index = 2;
            byte[] buffer = new byte[1024];

            // ElinBinary.BinaryWriter.WriteInt(buffer, a, ref index);
            // Debug.Log(index);
            // Debug.Log(buffer[2]);

            // int[] array = new int[]{
            //     1,2,3,4,5
            // };
            // int index2 = 0;
            // byte[] buf = new byte[1024];
            // ElinBinary.BinaryWriter.WriteIntArray(buf, array, ref index2);
            // Debug.Log(index2);
            // Debug.Log(buf[0]);
            // Debug.Log(buf[1]);
            // Debug.Log(buf[2]);
            // Debug.Log(buf[3]);
            // Debug.Log(buf[4]);
            // Debug.Log(buf[5]);
            // Debug.Log(buf[6]);
            // Debug.Log(buf[7]);
            // Debug.Log(buf[8]);

            char aa = 'A';
            BinaryWriter.WriteChar(buffer, aa, ref index);
            string aaa = Convert.ToString(buffer[2], 2);//讲正整数转为2进制字符串；
            Debug.Log(aaa);
            int index2 = 2;
            char bb = BinaryReader.ReadChar(buffer, ref index2);
            Debug.Log(bb);

            // ushort b = 2;
            // BinaryWriter.WriteUshort(buffer, b, ref index);
            // int index2 = 2;
            // ushort c = BinaryReader.ReadUshort(buffer, ref index2);
            // Debug.Log(c);
            // Debug.Log(index);
        }

        // Update is called once per frame
        void Update() {

        }
    }
}
