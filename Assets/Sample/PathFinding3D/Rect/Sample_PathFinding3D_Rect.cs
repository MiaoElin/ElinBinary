using UnityEngine;
using System.Collections.Generic;

public class Sample_PathFinding3D_Rect : MonoBehaviour {

    [SerializeField] int gridWidth;
    [SerializeField] int gridHeight;
    [SerializeField] float sideLength;
    RectCell3D[] rectCells;
    public List<Vector3> path;
    List<GameObject> pathObjects;

    HashSet<Vector2Int> blockSet;
    [SerializeField] GameObject prefabCube;
    [SerializeField] GameObject prefabPath;
    GameObject startCube;
    GameObject endCube;
    Vector3 start;
    Vector3 end;

    void Start() {
        start = Vector3.zero;
        end = Vector3.zero;
        rectCells = new RectCell3D[gridWidth * gridHeight];
        path = new List<Vector3>();
        pathObjects = new List<GameObject>();
        blockSet = new HashSet<Vector2Int>();
        GFpathFinding3D_Rect.Ctor(gridWidth, gridHeight, sideLength);
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                RectCell3D rect = new RectCell3D();
                rect.Ctor(x, y, sideLength);
                rect.worldPos.y = Terrain.activeTerrain.SampleHeight(rect.worldPos);
                int index = y * gridWidth + x;
                rectCells[index] = rect;
            }
        }

    }

    void Update() {
        LayerMask Ground = 1 << 3;
        // 鼠标左键选择格子
        if (Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool has = Physics.Raycast(ray, out RaycastHit hit, 100, Ground);
            if (has) {
                var gridPos = GFpathFinding3D_Rect.WorldToGridPos(hit.point);
                var index = GFpathFinding3D_Rect.GetIndex(gridPos);
                ref RectCell3D cell = ref rectCells[index];
                if (cell.isBlock) {
                    if (Input.GetKey(KeyCode.C)) {
                        cell.isBlock = false;
                        blockSet.Remove(cell.pos);
                    }
                } else if (!Input.GetKey(KeyCode.C)) {
                    cell.isBlock = true;
                    blockSet.Add(cell.pos);
                }
            }
        }

        if (Input.GetMouseButtonDown(2)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool has = Physics.Raycast(ray, out RaycastHit hit, 100, Ground);
            if (has) {
                var gridPos = GFpathFinding3D_Rect.WorldToGridPos(hit.point);
                var index = GFpathFinding3D_Rect.GetIndex(gridPos);
                RectCell3D cell = rectCells[index];
                if (startCube == null) {
                    startCube = GameObject.Instantiate(prefabCube);
                    startCube.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                start = cell.worldPos;
                startCube.transform.position = start;
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool has = Physics.Raycast(ray, out RaycastHit hit, 100, Ground);
            if (has) {
                var gridPos = GFpathFinding3D_Rect.WorldToGridPos(hit.point);
                var index = GFpathFinding3D_Rect.GetIndex(gridPos);
                RectCell3D cell = rectCells[index];
                if (endCube == null) {
                    endCube = GameObject.Instantiate(prefabCube);
                    endCube.GetComponent<MeshRenderer>().material.color = Color.red;

                }
                end = cell.worldPos;
                endCube.transform.position = end;
            }
        }
        if (start != Vector3.zero && end != Vector3.zero) {
            bool hasPath = GFpathFinding3D_Rect.Astar(start,
                    end,
                    (Vector2Int pos) => { return !blockSet.Contains(pos); },
                    (int index) => { return rectCells[index]; },
                    out path);

            if (hasPath) {
                start = Vector3.zero;
                end = Vector3.zero;
                foreach (var pos in path) {
                    var pathObject = GameObject.Instantiate(prefabPath);
                    pathObject.transform.position = pos;
                    pathObjects.Add(pathObject);
                }
            }
        }

    }

    void OnGUI() {
        foreach (var rect in rectCells) {
            var screenPos = Camera.main.WorldToScreenPoint(rect.worldPos);
            GUI.Label(new Rect(screenPos.x - 20, Screen.height - screenPos.y, 40, 20), $"({rect.pos.x},{rect.pos.y})");
        }
    }
    void OnDrawGizmos() {
        if (rectCells == null) {
            return;
        }
        foreach (var rect in rectCells) {
            Color color = Color.blue;
            if (rect.isClose) {
                color = Color.black;
            }
            if (rect.isBlock) {
                color = Color.red;
            }
            var all = GetCenterRound(rect.worldPos, sideLength);
            for (int i = 0; i < 4; i++) {
                Debug.DrawLine(all[i], all[(i + 1) % 4], color);
            }
        }
    }

    public Vector3[] GetCenterRound(Vector3 center, float sideLength) {
        Vector3[] all = new Vector3[4];
        all[0] = new Vector3(center.x - sideLength / 2, center.y, center.z + sideLength / 2);
        all[1] = new Vector3(center.x + sideLength / 2, center.y, center.z + sideLength / 2);
        all[2] = new Vector3(center.x + sideLength / 2, center.y, center.z - sideLength / 2);
        all[3] = new Vector3(center.x - sideLength / 2, center.y, center.z - sideLength / 2);
        return all;
    }
}