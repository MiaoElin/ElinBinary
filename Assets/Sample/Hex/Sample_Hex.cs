using UnityEngine;

public class Sample_Hex : MonoBehaviour {
    public int gridWidth;
    public int gridHeight;
    public float outterRadius;
    public HexCell[] hexCells;

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

    void Update() {
        foreach (var hex in hexCells) {
            // var worldPos = new Vector3(hex.worldPos.x, 0, hex.worldPos.y);
            // Debug.DrawLine(hex.worldPos, hex.worldPos + Vector3.forward * outterRadius, Color.red);
            Vector3[] arround = hex.Get_ArroundWorldPOS(outterRadius);
            for (int i = 0; i < arround.Length; i++) {
                Debug.DrawLine(arround[i], arround[(i + 1) % arround.Length], Color.blue);
            }  
        }
    }

}