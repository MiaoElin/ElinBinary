using UnityEngine;
using System.Collections.Generic;

public class Sample_PathFinding3D : MonoBehaviour {

    public int gridWidth;
    public int gridHeight;
    public float outterRadius;
    public PathHexCell[] hexCells;

    HashSet<PathHexCell> blockSet;
    PathHexCell startCell;
    PathHexCell endCell;
    [SerializeField] GameObject prefab;

    void Start() {
        hexCells = new PathHexCell[gridWidth * gridHeight];
        blockSet = new HashSet<PathHexCell>();
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                int index = y * gridWidth + x;
                PathHexCell hex = new PathHexCell();
                hex.Ctor(index, x, y, outterRadius);
                hex.SetHeight(hex => {
                    hex.worldPos.y = Terrain.activeTerrain.SampleHeight(hex.worldPos);
                });
                hexCells[index] = hex;
            }
        }

    }

    void Update() {
        LayerMask Ground = 1 << 3;

        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool has = Physics.Raycast(ray, out RaycastHit hit, 100, Ground);
            if (has) {
                Vector2Int hitGrid2Pos = GFHex.WorldPosToGridPos2Int(hit.point, outterRadius);
                var index = GFHex.GetIndex(gridWidth, gridHeight, hitGrid2Pos);
                // 看要不要加一个dic 存格子2坐标系为字典，方便查找
                PathHexCell hex = hexCells[index];
                blockSet.Add(hex);
                hex.isClose = true;
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool has = Physics.Raycast(ray, out RaycastHit hit, 100, Ground);
            if (has) {
                Vector2Int hitGrid2Pos = GFHex.WorldPosToGridPos2Int(hit.point, outterRadius);
                var index = GFHex.GetIndex(gridWidth, gridHeight, hitGrid2Pos);
                // 看要不要加一个dic 存格子2坐标系为字典，方便查找
                PathHexCell hex = hexCells[index];
                blockSet.Remove(hex);
                hex.isClose = false;
            }
        }
    }



    void OnDrawGizmos() {
        if (hexCells == null) {
            return;
        }
        foreach (var hex in hexCells) {
            var arrounds = hex.Get_ArroundWorldPOS(outterRadius);
            for (int i = 0; i < 6; i++) {
                Color color;
                if (hex.isClose) {
                    color = Color.red;
                } else {
                    color = Color.blue;
                }
                Debug.DrawLine(arrounds[i], arrounds[(i + 1) % arrounds.Length], color);
            }
        }
    }
}