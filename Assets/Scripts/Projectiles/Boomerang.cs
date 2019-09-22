using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : ProjectileBase {

    public float maxDistance;

    private float rot, percentTraveled = 0, returnBuffer;
    private bool returning = false;
    private Vector2 startVelocity;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        startVelocity = rb.velocity;
    }

    protected override void Update() {
        percentTraveled += (startVelocity.magnitude * Time.deltaTime) / maxDistance;
        if(!returning && percentTraveled >= 1f)
            BeginReturn();
        else if(returning && percentTraveled >= 2.5f)
            Destroy(gameObject);
    }

    private void LateUpdate() {
        rot += 360f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
    }

    private void FixedUpdate() {
        if(!returning)
            rb.velocity = startVelocity * Mathf.Cos(Mathf.Deg2Rad * 90f * percentTraveled);
        else {
            if(percentTraveled <= 1f)
                rb.velocity = startVelocity * Mathf.Sin(Mathf.Deg2Rad * 90f * percentTraveled) * -1f;
            else
                rb.velocity = startVelocity * -1f;
            returnBuffer += Time.fixedDeltaTime;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Terrain") && !collision.name.Contains("Projectiles Only")) {
            if(returning) {
                if(returnBuffer >= 0.1f)
                    Destroy(gameObject);
            } else {
                returning = true;
                percentTraveled = 1 - percentTraveled;
            }
        } else if(collision.CompareTag("Target")) {
            Destroy(collision.gameObject);
            if(returning) {
                if(returnBuffer >= 0.1f)
                    Destroy(gameObject);
            } else {
                returning = true;
                percentTraveled = 1 - percentTraveled;
            }
        }
    }

    private void BeginReturn() {
        returning = true;
        percentTraveled = 0;
    }

}
