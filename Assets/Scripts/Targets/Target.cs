using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private void OnDestroy() {
        TargetManager.count--;
    }

}
