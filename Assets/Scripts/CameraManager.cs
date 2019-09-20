using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    
    public Transform player;

    private void LateUpdate() {
        if(player != null) {
            transform.position = player.position + new Vector3(0, 0, -10);
        }
    }

}
