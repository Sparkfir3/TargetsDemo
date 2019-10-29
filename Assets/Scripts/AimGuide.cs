using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGuide : MonoBehaviour {

    private static Color32 BLUE = new Color32(0, 0, 255, 255);
    private static Color32 LIGHT_BLUE = new Color32(0, 255, 255, 255);
    private static Color32 GREEN = new Color32(0, 255, 0, 255);
    private static Color32 ORANGE = new Color32(255, 116, 0, 255);
    private static Color32 RED = new Color32(255, 0, 0, 255);

    private Transform player;
    private AbilityController abilities;
    private LineRenderer line;

    private bool renderBack = false;

    private void Awake() {
        player = transform.parent.transform;
        abilities = GetComponentInParent<AbilityController>();
        line = GetComponent<LineRenderer>();

        line.startWidth = 0.025f;
    }

    private void LateUpdate() {
        switch(abilities.currentAbility) {
            case 0:
                line.material.color = BLUE;
                renderBack = false;
                break;
            case 1:
                line.material.color = LIGHT_BLUE;
                renderBack = false;
                break;
            case 2:
                line.material.color = GREEN;
                renderBack = true;
                break;
            case 3:
                line.material.color = ORANGE;
                renderBack = false;
                break;
            case 4:
                line.material.color = RED;
                renderBack = false;
                break;
        }

        RaycastHit2D hit = Physics2D.Raycast(player.position, abilities.launchVelocity, 100f, LayerMask.GetMask("Terrain", "Target"));
        if(hit.collider != null) {
            line.SetPosition(1, hit.point);
        } else {
            line.SetPosition(1, abilities.launchVelocity * 100f);
        }
        if(renderBack) {
            hit = Physics2D.Raycast(player.position, abilities.launchVelocity * -1f, 10f, LayerMask.GetMask("Terrain", "Target"));
            if(hit.collider != null) {
                line.SetPosition(0, hit.point);
            } else {
                line.SetPosition(0, abilities.launchVelocity * -10f);
            }
        } else
            line.SetPosition(0, player.position);
    }

}
