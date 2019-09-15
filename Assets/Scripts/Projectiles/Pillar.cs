using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Terrain")) {
            Destroy(gameObject);
        } else if(collision.CompareTag("Target")) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
