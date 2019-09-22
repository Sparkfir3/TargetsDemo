using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour {

    private float lifeTime;

    protected virtual void Update() {
        lifeTime += Time.deltaTime;
        if(lifeTime >= 10f)
            Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Terrain") && !collision.name.Contains("Projectiles Only"))
            Destroy(gameObject);
        else if(collision.CompareTag("Target")) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    /*protected void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Terrain"))
            Destroy(this.gameObject);
        else if(collision.gameObject.CompareTag("Target")) {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }*/

}
