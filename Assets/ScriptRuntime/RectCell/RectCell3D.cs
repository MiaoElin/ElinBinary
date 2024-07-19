using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RectCell3D : IEquatable<RectCell2D>, IComparable<RectCell2D> {
    public Vector2Int pos;
    public Vector3 worldPos;
    public float fCost;
    public float gCost;
    public float hCost;

    public RectCell3D parent;

    public void Ctor(int x, int y, float sideLength) {
        this.pos = new Vector2Int(x, y);
        this.worldPos.x = x * sideLength + sideLength / 2;
        this.worldPos.y = y * sideLength + sideLength / 2;
    }

    public void Init(float f, float g, float h, RectCell3D parent) {
        this.fCost = f;
        this.gCost = g;
        this.hCost = h;
        this.parent = parent;
    }

    public Vector2Int[] GetArround() {
        Vector2Int[] arround = new Vector2Int[8];
        arround[0] = new Vector2Int(pos.x - 1, pos.y + 1); // 左上
        arround[1] = new Vector2Int(pos.x, pos.y + 1);     // 上
        arround[2] = new Vector2Int(pos.x + 1, pos.y + 1); // 右上
        arround[3] = new Vector2Int(pos.x + 1, pos.y);     // 右中
        arround[4] = new Vector2Int(pos.x + 1, pos.y - 1); // 右下
        arround[5] = new Vector2Int(pos.x, pos.y - 1);     // 中下
        arround[6] = new Vector2Int(pos.x - 1, pos.y - 1); // 左下
        arround[1] = new Vector2Int(pos.x - 1, pos.y);     // 左中
        return arround;
    }

    bool IEquatable<RectCell2D>.Equals(RectCell2D other) {
        return pos == other.pos;
    }

    public override int GetHashCode() {
        return pos.GetHashCode();
    }



    // public bool Equals(RectCell2D other) {
    //     if (other == null) {
    //         return false;
    //     }

    //     if (this.pos == other.pos) {
    //         return true;

    //     } else {
    //         return false;
    //     }
    // }

    // int IComparable<RectCell2D>.CompareTo(RectCell2D other) {
    //     // 暂时这么写
    //     if (fCost < other.fCost) {
    //         return -1;
    //     } else if (fCost > other.fCost) {
    //         return 1;
    //     } else {
    //         if (hCost > other.hCost) {
    //             return 1;
    //         } else if (hCost < other.hCost) {
    //             return -1;
    //         } else {
    //             if (pos.x > other.pos.x) {
    //                 return 1;
    //             } else {
    //                 return -1;
    //             }
    //         }
    //     }
    // }


    int IComparable<RectCell2D>.CompareTo(RectCell2D other) {

        Bit128 fKey = new Bit128();
        fKey.i32_0 = pos.y;
        fKey.i32_1 = pos.x;
        fKey.f32_2 = hCost;
        fKey.f32_3 = fCost;

        Bit128 otherFKey = new Bit128();
        otherFKey.i32_0 = other.pos.y;
        otherFKey.i32_1 = other.pos.x;
        otherFKey.f32_2 = other.hCost;
        otherFKey.f32_3 = other.fCost;

        if (fKey < otherFKey) {
            return -1;
        } else if (fKey > otherFKey) {
            return 1;
        } else {
            return 0;
        }

        // if (fCost < other.fCost) {
        //     return -1;
        // } else if (fCost > other.fCost) {
        //     return 1;
        // } else {
        //     if (hCost < other.hCost) {
        //         return -1;
        //     } else if (hCost > other.hCost) {
        //         return 1;
        //     } else {
        //         if (pos.x > other.pos.x) {
        //             return -1;
        //         } else if (pos.x < other.pos.x) {
        //             return 1;
        //         } else {
        //             if (pos.y > other.pos.y) {
        //                 return -1;
        //             } else if (pos.y < other.pos.y) {
        //                 return 1;
        //             } else {
        //                 return 0;
        //             }
        //         }
        //     }
        // }
    }
}