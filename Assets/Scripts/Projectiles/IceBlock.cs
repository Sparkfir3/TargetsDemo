using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : ProjectileBase {

    public GameObject iceBurst;

    private float rot;

    private void LateUpdate() {
        rot += 360f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        if((collision.CompareTag("Terrain") && collision.name != "Projectiles Only") || collision.CompareTag("Target")) {
            Instantiate(iceBurst, transform.position, transform.rotation, transform.parent);
            Destroy(gameObject);
        }
    }

}
