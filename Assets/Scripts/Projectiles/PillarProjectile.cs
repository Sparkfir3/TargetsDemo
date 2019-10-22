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
        if(collision.CompareTag("Terrain") && !collision.name.Contains("Projectiles Only")) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Terrain"));
            if(hit.collider == null) {
                RaycastHit2D hitL = Physics2D.Raycast(transform.position, Vector2.left, 0.15f, LayerMask.GetMask("Terrain"));
                RaycastHit2D hitR = Physics2D.Raycast(transform.position, Vector2.left, 0.15f, LayerMask.GetMask("Terrain"));

                Instantiate(pillar, transform.position + new Vector3(hitL.collider != null ? 0.15f : hitR.collider != null ? -0.15f : 0, -0.5f, 0)
                    , Quaternion.identity, transform.parent);
            }

            Destroy(gameObject);
        } else if(collision.CompareTag("Target")) {
            Destroy(collision.gameObject);
            Instantiate(pillar, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
    }

}
