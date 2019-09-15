using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarProjectile : ProjectileBase {

    public GameObject pillar;
    public int count;

    private float rot;

    private void LateUpdate() {
        rot += 360f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Terrain")) {
            Destroy(gameObject);
        } else if(collision.CompareTag("Target")) {
            Destroy(collision.gameObject);
            Instantiate(pillar, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
    }

}
