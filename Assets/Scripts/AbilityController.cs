using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour {

    public Transform parent;
	public int[] abilityCount = new int[5];
    public Text[] abilityNum = new Text[5];
    public float projectileSpeed;

    private GameObject laser, iceBlock, boomerang, pillarProjectile, fireball;
    private RectTransform selectionPos;

    [HideInInspector] public int currentAbility;
    [HideInInspector] public Vector3 launchVelocity;

    private void Awake() {
        laser = Resources.Load<GameObject>("Laser");
        iceBlock = Resources.Load<GameObject>("Ice Block");
        boomerang = Resources.Load<GameObject>("Boomerang");
        pillarProjectile = Resources.Load<GameObject>("Pillar Projectile");
        fireball = Resources.Load<GameObject>("Fireball");
    }

    private void Start() {
        abilityNum = GameManager.gm.menuCanvas.GetComponent<CanvasManager>().abilityNum;
        selectionPos = GameManager.gm.selectArrow.GetComponent<RectTransform>();
        UpdateAllUI();
    }

    private void Update() {
        // Select using numbers -----------------------------------------------------------------
        if(Input.GetKeyDown(KeyCode.Alpha1))
            UpdateSelection(0);
        else if(Input.GetKeyDown(KeyCode.Alpha2))
            UpdateSelection(1);
        else if(Input.GetKeyDown(KeyCode.Alpha3))
            UpdateSelection(2);
        else if(Input.GetKeyDown(KeyCode.Alpha4))
            UpdateSelection(3);
        else if(Input.GetKeyDown(KeyCode.Alpha5))
            UpdateSelection(4);
        // Select using scroll ------------------------------------------------------------------
        if(Input.mouseScrollDelta.y > 0 || Input.GetButtonDown("Scroll Left"))
            UpdateSelection(currentAbility - 1);
        else if(Input.mouseScrollDelta.y < 0 || Input.GetButtonDown("Scroll Right"))
            UpdateSelection(currentAbility + 1);
        // Attack --------------------------------------------------------------------------------
        launchVelocity = Vector3.Normalize(GetMousePos() - transform.position);
        if(Input.GetButtonDown("Fire") && !GameManager.gm.IsPaused) {
            switch(currentAbility) {
                case 0:
                    if(abilityCount[0] != 0) {
                        // Laser
                        GameObject laserObj = Instantiate(laser, GetLaunchPosition(launchVelocity), GetLaunchRotation(launchVelocity), parent);
                        laserObj.GetComponent<Rigidbody2D>().velocity = launchVelocity * projectileSpeed;
                        UpdateSingleUI(0, -1);
                    }
                    break;
                case 1:
                    if(abilityCount[1] != 0) {
                        // Ice Burst
                        GameObject iceObj = Instantiate(iceBlock, GetLaunchPosition(launchVelocity), GetLaunchRotation(launchVelocity), parent);
                        iceObj.GetComponent<Rigidbody2D>().velocity = launchVelocity * projectileSpeed * 0.6f;
                        UpdateSingleUI(1, -1);
                    }
                    break;
                case 2:
                    if(abilityCount[2] != 0) {
                        // Boomerang
                        GameObject boomObj = Instantiate(boomerang, GetLaunchPosition(launchVelocity), GetLaunchRotation(launchVelocity), parent);
                        boomObj.GetComponent<Rigidbody2D>().velocity = launchVelocity * projectileSpeed * 0.5f;
                        UpdateSingleUI(2, -1);
                    }
                    break;
                case 3:
                    if(abilityCount[3] != 0) {
                        // Pillars
                        GameObject pillarObj = Instantiate(pillarProjectile, GetLaunchPosition(launchVelocity), GetLaunchRotation(launchVelocity), parent);
                        pillarObj.GetComponent<Rigidbody2D>().velocity = launchVelocity * projectileSpeed * 0.6f;
                        UpdateSingleUI(3, -1);
                    }
                    break;
                case 4:
                    if(abilityCount[4] != 0) {
                        // Explosion
                        GameObject fireballObj = Instantiate(fireball, GetLaunchPosition(launchVelocity), GetLaunchRotation(launchVelocity), parent);
                        fireballObj.GetComponent<Rigidbody2D>().velocity = launchVelocity * projectileSpeed * 0.8f;
                        UpdateSingleUI(4, -1);
                    }
                    break;
            }
        }
    }

    private Vector3 GetMousePos() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }

    private Vector3 GetLaunchPosition(Vector3 velocity) {
        Vector3 pos = transform.position + (velocity * 0.25f);
        return pos;
    }

    private Quaternion GetLaunchRotation(Vector3 velocity) {
        float r = Vector3.Angle(velocity, Vector3.up);
        if(velocity.x > 0)
            r *= -1f;
        return Quaternion.Euler(0, 0, r);
        
    }

    private void UpdateSelection(int num) {
        if(num >= 5)
            num = 0;
        else if(num <= -1)
            num = 4;
        currentAbility = num;
        selectionPos.localPosition = new Vector2(15 + (num * 30), 42.5f);
    }

    public void SetAbilities(int a, int b, int c, int d, int e) {
        abilityCount[0] = a;
        abilityCount[1] = b;
        abilityCount[2] = c;
        abilityCount[3] = d;
        abilityCount[4] = e;
        UpdateAllUI();
    }

    private void UpdateAllUI() {
        for(int i = 0; i < 5; i++) {
            if(abilityCount[i] == -1)
                abilityNum[i].text = "∞";
            else if(abilityCount[i] == 0)
                abilityNum[i].text = "X";
            else
                abilityNum[i].text = abilityCount[i].ToString();
        }
    }

    private void UpdateSingleUI(int slot, int amountChanged) {
        if(abilityCount[slot] >= 0) {
            abilityCount[slot] += amountChanged;
            if(abilityCount[slot] == 0)
                abilityNum[slot].text = "X";
            else
                abilityNum[slot].text = abilityCount[slot].ToString();
        }
    }

}
