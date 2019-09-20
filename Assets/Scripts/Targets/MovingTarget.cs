using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : Target {

    public Transform pos1, pos2;
    public float delay, moveTime;

    protected bool delayOver;
    protected float timer, marker;
    protected Rigidbody2D rb;
    protected Vector3 freezePos1, freezePos2;

    protected virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
        freezePos1 = pos1.position;
        freezePos2 = pos2.position;
        SetStartPos();
        delayOver = false;
    }

    protected IEnumerator Start() {
        yield return Delay();
    }

    protected virtual void FixedUpdate() {
        if(delayOver) {
            IncrTimer();
            marker = 0.5f * (Mathf.Sin((timer / moveTime) * 360f * Mathf.Deg2Rad) + 1f);
            transform.position = Vector3.Lerp(freezePos1, freezePos2, marker);
        }
    }

    protected virtual void SetStartPos() {
        transform.position = freezePos1;
    }

    protected void IncrTimer() {
        timer += Time.fixedDeltaTime;
        if(timer > moveTime)
            timer = 0;
    }

    protected IEnumerator Delay() {
        yield return new WaitForSeconds(delay);
        delayOver = true;
    }

}
