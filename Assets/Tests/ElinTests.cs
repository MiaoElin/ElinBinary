using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class ElinTests {

    [Test]
    public void TestRect3DPathFinding() {

        int gridWidth = 32;
        int gridHeight = 32;
        float sideLength = 2;

        var rectCells = new RectCell3D[gridWidth * gridHeight];
        GFpathFinding3D_Rect.Ctor(gridWidth, gridHeight, sideLength);
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                RectCell3D rect = new RectCell3D();
                rect.Ctor(x, y, sideLength);
                rect.worldPos.y = 0;
                int index = y * gridWidth + x;
                rectCells[index] = rect;
            }
        }

        GFpathFinding3D_Rect.Ctor(gridWidth, gridHeight, sideLength);

        HashSet<Vector2Int> blocks = new HashSet<Vector2Int>();
        blocks.Add(new Vector2Int(4, 11));
        blocks.Add(new Vector2Int(5, 12));
        blocks.Add(new Vector2Int(6, 13));
        blocks.Add(new Vector2Int(7, 13));
        blocks.Add(new Vector2Int(8, 12));
        blocks.Add(new Vector2Int(9, 11));
        blocks.Add(new Vector2Int(10, 10));
        blocks.Add(new Vector2Int(11, 11));
        blocks.Add(new Vector2Int(12, 12));
        blocks.Add(new Vector2Int(13, 11));
        blocks.Add(new Vector2Int(14, 12));
        blocks.Add(new Vector2Int(15, 11));
        blocks.Add(new Vector2Int(16, 12));
        blocks.Add(new Vector2Int(17, 11));
        blocks.Add(new Vector2Int(18, 12));
        blocks.Add(new Vector2Int(18, 13));
        blocks.Add(new Vector2Int(18, 14));
        blocks.Add(new Vector2Int(19, 15));

        Vector3 start = new Vector3(14, 0, 13);
        Vector3 end = new Vector3(14, 0, 9);

        List<Vector3> path = new List<Vector3>(10000);
        bool has = GFpathFinding3D_Rect.Astar(start, end, (walkPos) => {
            return !blocks.Contains(walkPos);
        }, (int index) => {
            return rectCells[index];
        }, out path);

        foreach (var pos in path) {
            Debug.Log(pos);
        }

        Assert.Pass();

    }

}
