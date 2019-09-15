using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBurst : MonoBehaviour {

    private float lifeTime;

    private void Update() {
        lifeTime += Time.deltaTime;
        if(lifeTime >= 3f)
            Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.CompareTag("Target"))
            Destroy(collision.gameObject);
    }

    /*private void OnCollisionStay2D(Collision2D collision) {
        Debug.Log("ree");
        if(collision.gameObject.CompareTag("Target"))
            Destroy(collision.gameObject);
    }*/

}
