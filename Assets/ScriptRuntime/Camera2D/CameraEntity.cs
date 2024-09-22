using UnityEngine;
using GameFunctions;

public class CameraEntity {
    public Camera main;
    Vector3 offset;
    Vector3 startPos;
    Vector3 endPos;
    float duration;
    float t;

    public CameraEntity(Camera camera) {
        main = camera;
        offset = main.transform.position;
        duration = 0.2f;
    }

    public void Follow(Vector2 target) {
        Vector3 newPos = offset + (Vector3)target;
        if (newPos != endPos) {
            startPos = main.transform.position;
            endPos = newPos;
            t = 0;
        }

        if (t < duration) {
            t += Time.deltaTime;
            main.transform.position = GFEasing.Ease3D(GFEasingEnum.Linear, t, duration, startPos, endPos);
        }
    }

}