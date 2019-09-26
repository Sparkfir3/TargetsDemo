using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Terrain") && (!collision.name.Contains("Projectiles Only") && !collision.name.Contains("Pillar Only"))) {
            Destroy(gameObject);
        } else if(collision.CompareTag("Target")) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
