using UnityEngine;
using System.Collections.Generic;
using System;

public static class GFpathFinding3D_Rect {

    public static SortedSet<RectCell3D> openSet = new SortedSet<RectCell3D>();
    public static Dictionary<Vector2Int, RectCell3D> openDic = new Dictionary<Vector2Int, RectCell3D>();

    public static SortedSet<RectCell3D> closeSet = new SortedSet<RectCell3D>();
    public static Dictionary<Vector2Int, RectCell3D> closeDic = new Dictionary<Vector2Int, RectCell3D>();

    static int GRIDWIDTH;
    static int GRIDHEIGHT;
    static float SIDELENGTH;
    const int G_COST_BASE = 10;

    public static void Ctor(int gridWidth, int gridHeight, float sideLength) {
        GRIDWIDTH = gridWidth;
        GRIDHEIGHT = gridHeight;
        SIDELENGTH = sideLength;
    }

    public static Vector2Int[] GetArround() {
        Vector2Int[] arround = new Vector2Int[8];
        arround[0] = new Vector2Int(-1, 1); // 左上
        arround[1] = new Vector2Int(0, 1);     // 上
        arround[2] = new Vector2Int(1, 1); // 右上
        arround[3] = new Vector2Int(1, 0);     // 右中
        arround[4] = new Vector2Int(1, -1); // 右下
        arround[5] = new Vector2Int(0, -1);     // 中下
        arround[6] = new Vector2Int(-1, -1); // 左下
        arround[1] = new Vector2Int(-1, 0);     // 左中
        return arround;
    }

    public static bool Astar(Vector3 start, Vector3 end, Predicate<Vector2Int> isWalkable, Func<int, RectCell3D> GetRectCell3D, out List<Vector3> path) {
        Vector2Int startPos = WorldToGridPos(start);
        int startIndex = GetIndex(startPos);
        Vector2Int endPos = WorldToGridPos(end);
        int endIndex = GetIndex(endPos);
        if (startIndex == -1 || endIndex == -1) {
            path = null;
            return false;
        }

        if (!isWalkable(startPos) || !isWalkable(endPos)) {
            path = null;
            return false;
        }

        openSet.Clear();
        openDic.Clear();
        closeSet.Clear();
        closeDic.Clear();
        path = new List<Vector3>();

        RectCell3D rectStar = GetRectCell3D(startIndex);
        RectCell3D rectEnd = GetRectCell3D(endIndex);
        openSet.Add(rectStar);
        openDic.Add(startPos, rectStar);

        while (openSet.Count > 0) {
            var cur = openSet.Min;
            openSet.Remove(cur);
            openDic.Remove(cur.pos);
            closeSet.Add(cur);
            closeDic.Add(cur.pos, cur);

            Vector2Int[] neighbors = cur.GetArround();
            for (int i = 0; i < 8; i++) {
                var neighborPos = neighbors[i];
                if (!isWalkable(neighborPos) || closeDic.ContainsKey(neighborPos)) {
                    continue;
                }
                int left = i - 1;
                if ((left == -1)) {
                    left = 7;
                }
                if (!isWalkable(neighbors[left]) && !isWalkable(neighbors[(i + 1) % 8])) {
                    continue;
                }
                int index = GetIndex(neighborPos);
                if (index == -1) {
                    continue;
                }
                RectCell3D neighbor = GetRectCell3D(index);

                if (neighbor.pos == endPos) {
                    path.Add(neighbor.worldPos);
                    path.Add(cur.worldPos);
                    while (cur.parent != null) {
                        path.Add(cur.parent.worldPos);
                        cur = cur.parent;
                    }
                    return true;
                }

                float gCost = G_COST_BASE;
                float hCost = H_Manhatan(neighborPos, endPos);
                float fCost = gCost + hCost;

                if (openDic.ContainsKey(neighborPos)) {
                    if (fCost < neighbor.fCost) {
                        neighbor.Init(fCost, gCost, hCost, cur);
                    }
                } else {
                    neighbor.Init(fCost, gCost, hCost, cur);
                    openSet.Add(neighbor);
                    openDic.Add(neighborPos, neighbor);
                }

            }

        }
        return false;
    }

    public static Vector2Int WorldToGridPos(Vector3 worldPos) {
        float x = (worldPos.x - SIDELENGTH) / SIDELENGTH;
        float y = (worldPos.z - SIDELENGTH) / SIDELENGTH;
        return new Vector2Int(Mathf.CeilToInt(x), Mathf.CeilToInt(y));
    }

    public static int GetIndex(Vector2Int gridPos) {
        if (gridPos.x < 0 || gridPos.x >= GRIDWIDTH || gridPos.y < 0 || gridPos.y >= GRIDHEIGHT) {
            return -1;
        }
        return gridPos.y * GRIDWIDTH + gridPos.x;
    }

    public static float H_Manhatan(Vector2Int curPos, Vector2Int endPos) {
        return G_COST_BASE * (Mathf.Abs(curPos.x - endPos.x) + Mathf.Abs(curPos.y - endPos.y));
    }

}