using UnityEngine;
using System.Collections.Generic;
using System;

public static class GFpathFinding3D {
    public static SortedSet<PathHexCell> openSet = new SortedSet<PathHexCell>();
    // 用Grid3坐标做key
    public static Dictionary<Vector3Int, PathHexCell> openSetDic = new Dictionary<Vector3Int, PathHexCell>();
    public static SortedSet<PathHexCell> closeSet = new SortedSet<PathHexCell>();
    public static Dictionary<Vector3Int, PathHexCell> closeDic = new Dictionary<Vector3Int, PathHexCell>();
    public static float OUTTERRADIUS;
    public static int GRIDWIDTH;
    public static int GRIDHEIGHT;
    const float G_COST_BASE = 10;

    public static void Init(float outterRadius, int gridWidth, int gridHeight) {
        OUTTERRADIUS = outterRadius;
        GRIDWIDTH = gridWidth;
        GRIDHEIGHT = gridHeight;
    }

    public static float Distance(Vector3Int cur, Vector3Int end) {
        int curZ = -cur.x - cur.y;
        int endZ = -end.x - end.y;
        int disX = Mathf.Abs(cur.x - end.x);
        int disY = Mathf.Abs(cur.y - end.y);
        int disZ = Mathf.Abs(curZ - endZ);
        return Mathf.Max(disX, disY, disZ);
    }

    public static bool Astar(Vector3 start, Vector3 end, Predicate<PathHexCell> isWalkable, Func<int, PathHexCell> GetHexCell, out List<Vector3> path) {
        Vector2Int star2Int = GFHex.WorldPosToGridPos2Int(start, OUTTERRADIUS);
        int starIndex = GFHex.GetIndex(GRIDWIDTH, GRIDHEIGHT, star2Int);
        PathHexCell starHex = GetHexCell.Invoke(starIndex);
        // Debug.Log("index false :" + starIndex);

        Vector2Int end2Int = GFHex.WorldPosToGridPos2Int(end, OUTTERRADIUS);
        int endIndex = GFHex.GetIndex(GRIDWIDTH, GRIDHEIGHT, end2Int);
        PathHexCell endHex = GetHexCell.Invoke(endIndex);

        if (!isWalkable(starHex) || !isWalkable(endHex)) {
            path = null;
            return false;
        }
        path = new List<Vector3>();

        openSet.Clear();
        openSetDic.Clear();
        closeSet.Clear();
        closeDic.Clear();

        openSet.Add(starHex);
        openSetDic.Add(starHex.gridPos3Int, starHex);

        while (openSet.Count > 0) {
            var cur = openSet.Min;
            openSet.Remove(cur);
            openSetDic.Remove(cur.gridPos3Int);
            closeSet.Add(cur);
            closeDic.Add(cur.gridPos3Int, cur);
            cur.isblack = true;

            var neighbors = cur.GetArroundGridPos();

            for (int i = 0; i < 6; i++) {
                var neighborPos = neighbors[i];
                var index = GFHex.GetIndex(GRIDWIDTH, GRIDHEIGHT, neighborPos);
                PathHexCell neighbor = GetHexCell.Invoke(index);
                if (!isWalkable(neighbor) || closeDic.ContainsKey(neighborPos)) {
                    continue;
                }

                if (neighborPos == endHex.gridPos3Int) {
                    path.Add(endHex.worldPos);
                    path.Add(cur.worldPos);
                    while (cur.parent != null) {
                        path.Add(cur.parent.worldPos);
                        cur = cur.parent;
                    }
                    return true;
                }

                // float gCost = G_COST_BASE * (1 + Mathf.Abs(cur.worldPos.y - neighbor.worldPos.y) / OUTTERRADIUS);
                float gCost = G_COST_BASE;
                float hCost = Distance(neighborPos, endHex.gridPos3Int);
                float fCost = gCost + hCost;
                if (openSetDic.ContainsKey(neighborPos)) {
                    if (fCost < neighbor.fCost) {
                        neighbor.InitFGH(fCost, gCost, hCost, cur);
                    }
                } else {
                    neighbor.InitFGH(fCost, gCost, hCost, cur);
                    openSet.Add(neighbor);
                    openSetDic.Add(neighborPos, neighbor);
                }

            }

        }
        return false;
    }



}