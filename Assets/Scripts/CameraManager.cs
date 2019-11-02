using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    
    public Transform player;
    public Vector3 zoomOutPos;
    public float zoomOutSize;

    private Camera cam;

    private void Awake() {
        cam = GetComponent<Camera>();
        zoomOutPos.z = -10;
        zoomOutSize = Mathf.Clamp(zoomOutSize, 5, Mathf.Infinity);
    }

    private void LateUpdate() {
        if(Input.GetButton("Zoom Out")) {
            transform.position = zoomOutPos;
            cam.orthographicSize = zoomOutSize;
        } else if(player != null) {
            transform.position = player.position + new Vector3(0, 0, -10);
            cam.orthographicSize = 5;
        }
    }

}
