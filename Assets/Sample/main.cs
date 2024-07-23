using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RD = System.Random;

namespace ElinBinary.sample {
    public class main : MonoBehaviour {

        // int min
        // int (min~0)


        // Start is called before the first frame update
        void Start() {
            // Test.Entry();
            StringAnalysis.Entry();

            int b = 4;
            if (b == 4) {
                b += 1;
                Debug.Log(b);
            } else if (b == 5) {
                Debug.Log("enter");
                // 验证结果是不会进入 if 和else if 不会同时执行
            }

            int a = 4;
            for (int i = 0; i < 5; i++) {
                if (i == 3) {
                    if (a == 4) {
                        Debug.Log("true");
                        break;
                    }
                    a++;
                    Debug.Log("false");  // 未打印，break会直接中断for循环且不执行后面的
                    break;
                }
            }
            Debug.Log(a);

            List<Vector2> all = new List<Vector2>(10);

            for (int i = 0; i < all.Count; i++) {
                int j = UnityEngine.Random.Range(i, all.Count);
                Vector2 temp = all[i];
                all[i] = all[j];
                all[j] = temp;
            }

        }

        // Update is called once per frame
        void Update() {

        }
    }
}
