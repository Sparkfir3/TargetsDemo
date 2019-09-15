using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ProjectileBase {

    public GameObject explosion;
    
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Terrain") || collision.CompareTag("Target")) {
            //Destroy(this.gameObject);
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode() {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        explosion.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Destroy(explosion.gameObject);
        Destroy(this.gameObject);
    }

}
