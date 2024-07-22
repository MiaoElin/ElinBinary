using System;
using System.Collections.Generic;
using UnityEngine;

public class Rect3DComparer : IComparer<RectCell3D> {

    int IComparer<RectCell3D>.Compare(RectCell3D a, RectCell3D b) {
        Bit128 fKey = new Bit128();
        fKey.i32_0 = a.pos.y;
        fKey.i32_1 = a.pos.x;
        fKey.f32_2 = a.hCost;
        fKey.f32_3 = a.fCost;

        Bit128 otherFKey = new Bit128();
        otherFKey.i32_0 = b.pos.y;
        otherFKey.i32_1 = b.pos.x;
        otherFKey.f32_2 = b.hCost;
        otherFKey.f32_3 = b.fCost;

        if (a.pos.x == b.pos.x && a.pos.y == b.pos.y) {
            Debug.Log("SAME" + a.pos);
            return 0;
        }

        if (fKey < otherFKey) {
            return -1;
        } else if (fKey > otherFKey) {
            return 1;
        } else {
            return 0;
        }
    }

}
