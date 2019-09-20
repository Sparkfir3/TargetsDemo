using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostArea : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player"))
            collision.GetComponent<PlayerController>().floating = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Player"))
            collision.GetComponent<PlayerController>().floating = false;
    }

}
