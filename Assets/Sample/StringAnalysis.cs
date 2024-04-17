using System;
using UnityEngine;
public static class StringAnalysis {

    public static void Entry() {
        String str1 = "你好,我是机器人[id:]。";
        String str2 = "你好,这是一个[type:500]。";
        String str3 = "[e:33]";

        Analysis(str1);
        Analysis(str2);
        Analysis(str3);
    }

    static void Analysis(string str) {

        bool has = FindBetween(str, '[', ']', out string findTxt);

        if (has) {
            bool hastarget = FindBySlipt(findTxt, ':', out var findtarget1, out var findtarget2);
            if (hastarget) {
                if (findtarget1 != null && findtarget2 != null) {
                    Debug.Log(findtarget1 + " is " + findtarget2);
                }
            }
        } else {
            Debug.Log("Don't have'[]'");
        }

    }
    static bool FindBetween(string input, char left, char right, out string findTxt) {
        var chars = input.ToCharArray();
        findTxt = null;
        bool has = false;
        for (int i = 0; i < chars.Length; i++) {
            var cha = chars[i];
            if (cha == left) {
                // findTxt = cha.ToString();
                for (int j = i + 1; j < chars.Length; j++) {
                    var ch = chars[j];
                    if (ch == right) {
                        has = true;
                        break;
                    }
                    findTxt += ch.ToString();
                }
                break;
            }
        }
        return has;
    }

    static bool FindBySlipt(string str, char slipt, out string strLeft, out string strRight) {
        // find target;
        char[] findChars = str.ToCharArray();
        strLeft = null;
        strRight = null;
        bool hastarget = false;

        for (int i = 0; i < findChars.Length; i++) {
            var cha = findChars[i];
            if (cha != slipt) {
                strLeft += cha.ToString();
            } else if (cha == slipt) {
                for (int j = i + 1; j < findChars.Length; j++) {
                    var ch = findChars[j];
                    if (ch != slipt) {
                        strRight += ch.ToString();
                        hastarget = true;
                    } else if (ch == slipt) {
                        hastarget = false;
                        Debug.Log($"多于一个{slipt},不符合要求");
                        break;
                    }
                }
                break;
            }
        }
        if (strLeft == null) {
            Debug.Log("左边数据为空");
        }
        if (strRight == null) {
            Debug.Log("右边数据为空");
        }
        return hastarget;
    }
}