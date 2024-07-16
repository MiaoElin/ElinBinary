using UnityEngine;

public struct HexCell {
    public int index;
    public Vector3Int gridPos3Int;
    public Vector2Int gridPos2Int;
    public Vector3 worldPos;


    // 用每行的 y%2 余数为0的，x轴从-(y/2)开始，如果余数为1，则x轴从-((y-1)/2)开始。
    // 01  23  34  56  每两行为一组，x轴开始的数一样，每加两行，x轴开始的数就减1；
    // 这样可以通过y找到x以及z （z = -x -y）

    public void Ctor(int index, int x, int y, float outterRadius) {
        this.index = index;

        // grid2Int
        gridPos2Int = new Vector2Int(x, y);

        float innerRadius = outterRadius * Mathf.Sqrt(3) / 2;

        // grid3Int和worldPos的y
        gridPos3Int.y = y;
        worldPos.y = 0;

        if (y % 2 == 1) {
            // grid3Int的x
            gridPos3Int.x = (-y / 2) + x;
            // worldPos的x
            worldPos.x = gridPos2Int.x * innerRadius * 2f + innerRadius * 2;

        } else if (y % 2 == 0) {
            gridPos3Int.x = (-(y - 1) / 2) + x;
            worldPos.x = gridPos2Int.x * innerRadius * 2f + innerRadius;
        }
        // gridPos3Int的z
        gridPos3Int.z = -gridPos3Int.x - gridPos3Int.y;
        worldPos.z = gridPos2Int.y * outterRadius * 1.5f + outterRadius;


    }


    public void Grid2IntToWorldPos(Vector2Int grid2Int) {

    }


    public Vector3Int[] GetArroundGridPos() {
        Vector3Int[] arround = new Vector3Int[6];
        arround[0] = new Vector3Int(gridPos3Int.x - 1, gridPos3Int.y + 1, gridPos3Int.z); // 左上
        arround[1] = new Vector3Int(gridPos3Int.x, gridPos3Int.y + 1, gridPos3Int.z - 1); // 右上
        arround[2] = new Vector3Int(gridPos3Int.x - 1, gridPos3Int.y, gridPos3Int.z + 1); // 左
        arround[3] = new Vector3Int(gridPos3Int.x + 1, gridPos3Int.y, gridPos3Int.z - 1); // 右
        arround[4] = new Vector3Int(gridPos3Int.x, gridPos3Int.y - 1, gridPos3Int.z + 1); // 左下
        arround[5] = new Vector3Int(gridPos3Int.x + 1, gridPos3Int.y - 1, gridPos3Int.z); // 右下
        return arround;
    }

    public Vector3[] Get_ArroundWorldPOS(float outterRadius) {
        float innerRadius = outterRadius * Mathf.Sqrt(3) / 2;
        Vector3[] arround = new Vector3[6];
        arround[0] = new Vector3(worldPos.x, worldPos.y, worldPos.z + outterRadius); // top
        arround[1] = new Vector3(worldPos.x + innerRadius, worldPos.y, worldPos.z + 0.5f * outterRadius); // 右上
        arround[2] = new Vector3(worldPos.x + innerRadius, worldPos.y, worldPos.z - 0.5f * outterRadius); // 右下
        arround[3] = new Vector3(worldPos.x, worldPos.y, worldPos.z - outterRadius);
        arround[4] = new Vector3(worldPos.x - innerRadius, worldPos.y, worldPos.z - 0.5f * outterRadius);
        arround[5] = new Vector3(worldPos.x - innerRadius, worldPos.y, worldPos.z + 0.5f * outterRadius);
        return arround;
    }
}