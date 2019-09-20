using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {
    
    [HideInInspector] public static int count = 0;

    private void Awake() {
        count = 0;
        foreach(Transform child in transform)
            count++;
    }

}
