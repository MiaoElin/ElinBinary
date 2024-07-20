using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Sample_PathFinding3D_Hex : MonoBehaviour {

    public int gridWidth;
    public int gridHeight;
    public float outterRadius;
    public PathHexCell[] hexCells;

    HashSet<PathHexCell> blockSet;
    PathHexCell startCell;
    PathHexCell endCell;
    [SerializeField] GameObject prefabCube;
    [SerializeField] GameObject prefabSphere;
    GameObject starCube;
    GameObject endCube;
    List<Vector3> path;
    Dictionary<Vector3, GameObject> pathObjectDic;

    void Start() {

        GFpathFinding3D_Hex.Init(outterRadius, gridWidth, gridHeight);

        path = new List<Vector3>();
        pathObjectDic = new Dictionary<Vector3, GameObject>();
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

        // 鼠标左键添加blockSet 或者取消blockSet
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool has = Physics.Raycast(ray, out RaycastHit hit, 100, Ground);
            if (has) {
                var index = GFHex.GetIndex(hit.point, outterRadius, gridWidth, gridHeight);
                // 看要不要加一个dic 存格子2坐标系为字典，方便查找
                PathHexCell hex = hexCells[index];
                if (!hex.isClose) {
                    hex.isClose = true;
                    blockSet.Add(hex);
                } else {
                    hex.isClose = false;
                    blockSet.Remove(hex);
                }
            }
        }

        if (Input.GetMouseButtonDown(2)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool has = Physics.Raycast(ray, out RaycastHit hit, 100, Ground);
            if (has) {
                var index = GFHex.GetIndex(hit.point, outterRadius, gridWidth, gridHeight);
                PathHexCell hex = hexCells[index];
                startCell = hex;
                if (starCube == null) {
                    starCube = GameObject.Instantiate(prefabCube, transform);
                    starCube.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                starCube.transform.position = hex.worldPos + Vector3.up * 0.5f;
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool has = Physics.Raycast(ray, out RaycastHit hit, 100, Ground);
            if (has) {
                var index = GFHex.GetIndex(hit.point, outterRadius, gridWidth, gridHeight);
                PathHexCell hex = hexCells[index];
                endCell = hex;
                if (endCube == null) {
                    endCube = GameObject.Instantiate(prefabCube, transform);
                    endCube.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                endCube.transform.position = hex.worldPos + Vector3.up * 0.5f;
            }
        }

        if (startCell != null && endCell != null) {
            bool has = GFpathFinding3D_Hex.Astar(startCell.worldPos,
                  endCell.worldPos,
                  (PathHexCell hexCell) => { return !blockSet.Contains(hexCell); },
                  (int index) => { return hexCells[index]; },
                  out path);

            if (has) {
                if (pathObjectDic.Count != path.Count) {
                    foreach (var pos in path) {
                        var cube = GameObject.Instantiate(prefabSphere, transform);
                        cube.transform.position = pos + Vector3.up * 0.5f;
                        pathObjectDic.Add(pos, cube);
                    }
                }
                startCell = null;
                endCell = null;
            } else {
                starCube.GetComponentInChildren<Text>().gameObject.SetActive(true);
                endCube.GetComponentInChildren<Text>().gameObject.SetActive(true);
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
                Color color = Color.blue;

                if (hex.isClose) {
                    color = Color.red;
                }
                if (hex.isblack) {
                    color = Color.black;
                }
                Debug.DrawLine(arrounds[i], arrounds[(i + 1) % arrounds.Length], color);
            }
        }

    }

    void OnGUI() {
        LayerMask Ground = 1 << 3;
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool has = Physics.Raycast(ray, out RaycastHit hit, 100, Ground);
            if (has) {
                var index = GFHex.GetIndex(hit.point, outterRadius, gridWidth, gridHeight);
                PathHexCell hex = hexCells[index];
                GUILayout.Label($"Mouse Pos{hex.gridPos2Int}");

            }
        }

        if (hexCells == null) {
            return;
        }
        foreach (var hex in hexCells) {
            Vector3 worldPos = hex.worldPos;
            Vector3 screePos = Camera.main.WorldToScreenPoint(worldPos);
            GUI.Label(new Rect(screePos.x - 20, Screen.height - screePos.y, 40, 20), $"({hex.gridPos3Int.x},{hex.gridPos3Int.y})");
        }

    }
}