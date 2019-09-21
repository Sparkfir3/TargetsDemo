using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {
    
    [HideInInspector] public static int count = 0;
    [HideInInspector] public static bool canWin = false;

    public static float winBuffer = 0;

    private void Awake() {
        winBuffer = 0;
        count = 0;
        foreach(Transform child in transform)
            count++;
    }

    private void Update() {
        if(winBuffer < 2.5f && !canWin) {
            winBuffer += Time.deltaTime;
        } else if(count == 0)
            canWin = true;
    }

}
