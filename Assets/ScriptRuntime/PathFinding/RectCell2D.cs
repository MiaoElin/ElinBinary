using UnityEngine;

public class RectCell2D {
    public Vector2Int pos;
    public float fCost;
    public float gCost;
    public float hCost;

    public RectCell2D parent;


    public void Init(Vector2Int pos, float f, float g, float h, RectCell2D parent) {
        this.pos = pos;
        this.fCost = f;
        this.gCost = g;
        this.hCost = h;

        this.parent = parent;
    }
}