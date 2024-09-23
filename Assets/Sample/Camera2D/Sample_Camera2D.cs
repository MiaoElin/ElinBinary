using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_Camera2D : MonoBehaviour {
    [SerializeField] GameObject role;
    [SerializeField] Camera main;
    Vector2 moveAxis;

    CameraEntity cam;

    void Start() {
        cam = new CameraEntity(main);
    }

    void Update() {
        moveAxis = Vector2.zero;
        if (Input.GetKey(KeyCode.D)) {
            moveAxis.x = 1;
        } else if (Input.GetKey(KeyCode.A)) {
            moveAxis.x = -1;
        }
        role.transform.position += (Vector3)moveAxis * Time.deltaTime * 5;

        cam.Follow(role.transform.position);

        if (Input.GetKey(KeyCode.P)) {
            cam.main.transform.position += new Vector3(0, cam.Shake(), 0);
        }

    }
}
