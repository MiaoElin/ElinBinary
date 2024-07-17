using UnityEngine;

public static class GFHex {

    public static int GetIndex(int gridWidth, int gridHeight, Vector3Int gridPos) {
        // grid2Int y
        int y = gridPos.z;
        // y如果超出范围 返回-1
        if (y >= gridHeight || y < 0) {
            return -1;
        }
        // grid2Int x
        int x;
        int minX;
        int maxX;

        if (y % 2 == 1) {
            minX = -y / 2;
        } else {
            minX = -(y - 1) / 2;
        }
        maxX = gridWidth - minX - 1;
        // x 如果超出范围，返回-1
        if (gridPos.x < minX || gridPos.x > maxX) {
            return -1;
        } else {
            x = gridPos.x - minX;
        }
        return y * gridWidth + x;
    }


    public static Vector3Int[] GetArroundGridPos(Vector3Int gridPos) {
        Vector3Int[] arround = new Vector3Int[6];
        arround[0] = new Vector3Int(gridPos.x - 1, gridPos.y + 1, gridPos.z); // 左上
        arround[1] = new Vector3Int(gridPos.x, gridPos.y + 1, gridPos.z - 1); // 右上
        arround[2] = new Vector3Int(gridPos.x - 1, gridPos.y, gridPos.z + 1); // 左
        arround[3] = new Vector3Int(gridPos.x + 1, gridPos.y, gridPos.z - 1); // 右
        arround[4] = new Vector3Int(gridPos.x, gridPos.y - 1, gridPos.z + 1); // 左下
        arround[5] = new Vector3Int(gridPos.x + 1, gridPos.y - 1, gridPos.z); // 右下
        return arround;
    }

    public static Vector2Int WorldPosToGridPos2Int(Vector2 worldPos, float outterRadius) {
        float innerRadius = outterRadius * Mathf.Sqrt(3) / 2;
        Vector2Int guessPos = Vector2Int.zero;
        guessPos.x = Mathf.CeilToInt(worldPos.x / (innerRadius * 2f));
        guessPos.y = Mathf.CeilToInt(worldPos.y / (outterRadius * 1.5f));

        Vector2Int min = new Vector2Int(guessPos.x - 1, guessPos.y - 1);
        Vector2Int max = guessPos;
        float nearlyDistance = float.MaxValue;
        Vector2Int result = guessPos;
        for (int x = min.x; x <= max.x; x++) {
            for (int y = min.y; y <= max.y; y++) {
                Vector2Int curlogic = new Vector2Int(x, y);
                Vector2 cur = Grid2IntToWorldPos(curlogic, outterRadius);
                float distanceSqr = Vector2.SqrMagnitude(worldPos - cur);
                if (distanceSqr < nearlyDistance) {
                    nearlyDistance = distanceSqr;
                    result = curlogic;
                }
            }
        }
        return result;
    }

    public static Vector2Int WorldPosToGridPos2Int(Vector2 worldPos, float outterRadius, HexCell[] hexCells) {
        float innerRadius = outterRadius * Mathf.Sqrt(3) / 2;
        Vector2Int guessPos = Vector2Int.zero;
        guessPos.x = Mathf.CeilToInt(worldPos.x / (innerRadius * 2f));
        guessPos.y = Mathf.CeilToInt(worldPos.y / (outterRadius * 1.5f));

        Vector2Int min = new Vector2Int(guessPos.x - 1, guessPos.y - 1);
        Vector2Int max = guessPos;
        float nearlyDistance = float.MaxValue;
        Vector2Int result = guessPos;
        for (int x = min.x; x <= max.x; x++) {
            for (int y = min.y; y <= max.y; y++) {
                Vector2Int curlogic = new Vector2Int(x, y);
                int index = GetIndex(10, 10, curlogic);
                if (index == -1) {
                    continue;
                }
                Vector3 cur = hexCells[index].worldPos;
                float distanceSqr = Vector2.SqrMagnitude(worldPos - new Vector2(cur.x, cur.z));
                if (distanceSqr < nearlyDistance) {
                    nearlyDistance = distanceSqr;
                    result = curlogic;
                }
            }
        }
        return result;
    }



    public static Vector2 Grid2IntToWorldPos(Vector2Int grid2Int, float outterRadius) {
        Vector2 result;
        float innerRadius = outterRadius * Mathf.Sqrt(3) / 2;
        if (grid2Int.y % 2 == 1) {
            result.x = grid2Int.x * innerRadius * 2f + innerRadius * 2f;
        } else {
            result.x = grid2Int.x * innerRadius * 2f + innerRadius;
        }
        result.y = outterRadius * 1.5f * grid2Int.y + outterRadius;
        return result;
    }


    // public static Vector3Int WorldPosToGridPos3Int(Vector3 worldPos, float outterRadius, int gridWidth) {
    //     // 在这个外层要先判断这个worldPos是否在范围内，不在的话，退出
    //     Vector2Int gridPod2Int = WorldPosToGridPos2Int(worldPos, outterRadius);
    //     // 用index获取，用gridposint 直接获取；

    // }

    public static int GetIndex(int gridWidth, int gridHeight, Vector2Int gridPos2Int) {
        if (gridPos2Int.x < 0 || gridPos2Int.x >= gridWidth) {
            return -1;
        }
        if (gridPos2Int.y < 0 || gridPos2Int.y >= gridHeight) {
            return -1;
        }

        return gridPos2Int.y * gridWidth + gridPos2Int.x;

    }
}