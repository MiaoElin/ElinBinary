using UnityEngine;
using System.Collections.Generic;

public static class GFPathFinding {

    // 用有序的哈希表存储已经打开的格子
    public static SortedSet<RectCell2D> openSet;
    // 用字典存储打开的格子，便于一次找到
    public static Dictionary<Vector2Int, RectCell2D> openSetDic;
    public static SortedSet<RectCell2D> closeSet;
    public static Dictionary<Vector2Int, RectCell2D> closeSetDic;
    const int CELLSIZE = 10;

    readonly static Vector2Int[] neighbors = new Vector2Int[4]{
        // 优先级 右 下 左 上
        new Vector2Int(1,0),
        new Vector2Int(0,-1),
        new Vector2Int(-1,0),
        new Vector2Int(0,1)
    };

    // 看情况调用（要不要追逐到 障碍物再停止）
    public static bool IsStart_EndPosInBlockSet(Vector2Int start, Vector2Int end, HashSet<Vector2Int> blockSet) {
        if (blockSet.Contains(start) || blockSet.Contains(end)) {
            return true;
        } else {
            return false;
        }

    }

    public static int Astar(Vector2Int start, Vector2Int end, HashSet<Vector2Int> blockSet, int limiteCount, Vector2Int[] path) {
        // -1无路
        // 1有路
        // -2超出步数

        openSet.Clear();
        openSetDic.Clear();
        closeSet.Clear();
        closeSetDic.Clear();

        int stepCount = 0;
        int count = 0;

        RectCell2D startRect = new RectCell2D();
        startRect.Init(start, 0, 0, 0, null);

        openSet.Add(startRect);
        openSetDic.Add(start, startRect);

        // 当有路可走的时候
        while (openSet.Count > 0) {
            // 判断步数有没有超过限制
            if (stepCount > limiteCount) {
                return -2;
            }

            // 当步数没超
            // 找到最近的那个，移除
            var cur = openSet.Min;
            openSet.Remove(cur);
            openSetDic.Remove(cur.pos);
            closeSet.Add(cur);
            closeSetDic.Add(cur.pos, cur);

            // 遍历四个方向
            for (int i = 0; i < 4; i++) {
                Vector2Int neighborPos = cur.pos + neighbors[i];
                if (blockSet.Contains(neighborPos)) {
                    continue;
                }
                // 如果到达 
                if (neighborPos == end) {
                    count = 0;
                    // 从最后一个开始回溯，因为每个格子的父节点只有一个，而子节点可以多个，所以存父节点更好             从头开始生成路径 、、 后面再试，
                    path[count++] = end;
                    path[count++] = cur.pos;
                    while (cur.parent != null) {
                        path[count++] = cur.parent.pos;
                        cur = cur.parent;
                    }
                    stepCount += 1;
                    return 1;
                }

                // 还没到达 计算这个格子的 g h f
                float gCost = CELLSIZE;
                float hCost = H_Manhattan(neighborPos, end);
                float fCost = gCost + hCost;
                // 判断openSetDic里是否已经有这个格子
                RectCell2D rectNeighbor = new RectCell2D();
                bool has = openSetDic.TryGetValue(neighborPos, out rectNeighbor);
                if (has) {
                    // 如果格子新的f值小于之前存的，则用新的值覆盖
                    if (fCost < rectNeighbor.fCost) {
                        rectNeighbor.fCost = fCost;
                        rectNeighbor.gCost = gCost;
                        rectNeighbor.hCost = hCost;
                        rectNeighbor.parent = cur;
                    }
                } else {
                    rectNeighbor.pos = neighborPos;
                    rectNeighbor.gCost = gCost;
                    rectNeighbor.fCost = fCost;
                    rectNeighbor.hCost = hCost;
                    rectNeighbor.parent = cur;
                    openSet.Add(rectNeighbor);
                    openSetDic.Add(neighborPos, rectNeighbor);
                }
            }
        }
        return -1;
    }

    public static float H_Manhattan(Vector2Int cur, Vector2Int end) {
        return CELLSIZE * Mathf.Abs(cur.x - end.x) + Mathf.Abs(cur.y - end.y);
    }
}