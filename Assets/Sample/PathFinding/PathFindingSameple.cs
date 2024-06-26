using UnityEngine;
using System.Collections.Generic;

public class PathFinding2DSample : MonoBehaviour {

    HashSet<Vector2Int> blockSet = new HashSet<Vector2Int>();
    Vector2Int start = Vector2Int.zero;
    Vector2Int end;
    int limiteCount = 5000;
    // Vector2Int[] path = new Vector2Int[5000];
    public List<Vector2Int> path;

    void Start() {
        // RectCell2D rect = new RectCell2D();
        // rect.Init(Vector2Int.one, 20, 10, 10, null);

        // GFPathFinding.openSet.Add(rect);
        // // GFPathFinding.openSet.Remove(rect);
        // Debug.Log(GFPathFinding.openSet.Count);
        path = new List<Vector2Int>();
        GFPathFinding.openSet?.Clear();
        GFPathFinding.closeSet?.Clear();
    }
    void Update() {
        // 按下鼠标左键
        var mouseGridPos = GetMouseGridPos();
        if (Input.GetMouseButtonDown(0)) {
            start = mouseGridPos;
        } else if (Input.GetMouseButtonDown(1)) {
            end = mouseGridPos;
            GFPathFinding.Astar(start, end, blockSet, limiteCount, out path);
        } else if (Input.GetMouseButton(2)) {
            blockSet.Add(mouseGridPos);
        }
    }

    void OnDrawGizmos() {

        Gizmos.color = Color.yellow;
        if (GFPathFinding.openSet != null) {
            foreach (var rect in GFPathFinding.openSet) {
                Gizmos.DrawCube(new Vector3(rect.pos.x, rect.pos.y, 0), Vector3.one);
            }
        }

        Gizmos.color = Color.black;
        if (GFPathFinding.closeSet != null) {
            foreach (var rect in GFPathFinding.closeSet) {
                Gizmos.DrawCube(new Vector3(rect.pos.x, rect.pos.y, 0), Vector3.one);
            }
        }

        Gizmos.color = Color.blue;
        if (path.Count > 0) {
            foreach (var pos in path) {
                Gizmos.DrawCube(new Vector3(pos.x, pos.y, 0), Vector3.one);
            }
        }

        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(start.x, start.y, 0), Vector3.one);

        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(new Vector3(end.x, end.y, 0), Vector3.one);

        Gizmos.color = Color.red;
        if (blockSet != null) {
            foreach (var pos in blockSet) {
                Gizmos.DrawCube(new Vector3(pos.x, pos.y, 0), Vector3.one);
            }
        }

    }

    public Vector2Int GetMouseGridPos() {
        Vector2 screen = Input.mousePosition;
        Vector2 world = Camera.main.ScreenToWorldPoint(screen);
        // return new Vector2Int ((int)world) 强转误差大，要用四舍五入
        return new Vector2Int(Mathf.RoundToInt(world.x), Mathf.RoundToInt(world.y));
    }
}