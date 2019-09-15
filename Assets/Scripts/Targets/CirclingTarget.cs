using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingTarget : MovingTarget {

	public float radius;
    public bool clockwise;

    protected override void FixedUpdate() {
        if(delayOver) {
            IncrTimer();
            marker = (timer / moveTime) * 360f * Mathf.Deg2Rad;
            if(clockwise)
                transform.position = freezePos1 + (new Vector3(Mathf.Sin(marker), Mathf.Cos(marker)) * radius);
            else
                transform.position = freezePos1 + (new Vector3(Mathf.Cos(marker), Mathf.Sin(marker)) * radius);
        }
    }

    protected override void SetStartPos() {
        if(clockwise)
            transform.position = freezePos1 + new Vector3(0, radius);
        else
            transform.position = freezePos1 + new Vector3(radius, 0);
    }

}
