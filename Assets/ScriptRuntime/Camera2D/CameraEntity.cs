using UnityEngine;

public class CameraEntity {
    public Camera main;
    Vector3 offset;

    public CameraEntity(Camera camera) {
        main = camera;
        offset = main.transform.position;
    }

    public void Follow(Vector2 target) {
        main.transform.position = offset + (Vector3)target;
    }
}