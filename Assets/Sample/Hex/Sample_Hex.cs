using UnityEngine;

public class Sample_Hex : MonoBehaviour {
    public int gridWidth;
    public int gridHeight;
    public float outterRadius;
    public HexCell[] hexCells;
    [SerializeField] Transform mouse;


    void Start() {
        hexCells = new HexCell[gridWidth * gridHeight];
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                HexCell hex = new HexCell();
                int index = y * gridWidth + x;
                hex.Ctor(index, x, y, outterRadius);
                hexCells[index] = hex;
            }
        }
    }

    void OnGUI() {

        // Vector2 mousePos = Input.mousePosition;
        // Vector3 mouseWordPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));

        Vector2 center = GFHex.WorldPosToGridPos2Int(new Vector2(mouse.position.x, mouse.position.z), outterRadius, hexCells);
        GUILayout.Label($"Mouse Pos{center}");

        if (hexCells == null) {
            return;
        }
        foreach (var hex in hexCells) {
            Vector3 worldPos = hex.worldPos;
            Vector3 screePos = Camera.main.WorldToScreenPoint(worldPos);
            GUI.Label(new Rect(screePos.x - 1, Screen.height - screePos.y, 30, 20), $"({hex.gridPos2Int.x},{hex.gridPos2Int.y})");
        }

    }

    void OnDrawGizmos() {
        if (hexCells == null) {
            return;
        }
        foreach (var hex in hexCells) {
            Vector3[] arround = hex.Get_ArroundWorldPOS(outterRadius);
            for (int i = 0; i < arround.Length; i++) {
                Debug.DrawLine(arround[i], arround[(i + 1) % arround.Length], Color.blue);
            }
        }

    }

}