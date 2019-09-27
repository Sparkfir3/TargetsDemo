using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingTarget : Target {
    
    public Transform pos1, pos2;
    public float timer;

    private int currentPos = 1;
    private Vector3 setPos1, setPos2;

    protected void Start() {
        setPos1 = pos1.position;
        setPos2 = pos2.position;
        StartCoroutine(Teleport());
    }

    protected IEnumerator Teleport() {
        while(true) {
            if(currentPos == 1) {
                transform.position = setPos2;
                currentPos = 2;
            } else {
                transform.position = setPos1;
                currentPos = 1;
            }
            yield return new WaitForSeconds(timer);
        }
    }

}
