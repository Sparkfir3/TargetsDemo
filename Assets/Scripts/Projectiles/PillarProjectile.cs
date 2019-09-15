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
            Deactivate();
            StartCoroutine(Burst(collision.gameObject));
        } else if(collision.CompareTag("Target")) {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void Deactivate() {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        //Destroy(this.gameObject);
    }

    private IEnumerator Burst(GameObject wall) {
        //GameObject leftBurst = Instantiate(pillar, transform.position + new Vector3(0.5f, 0), Quaternion.identity, transform);
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }

}
