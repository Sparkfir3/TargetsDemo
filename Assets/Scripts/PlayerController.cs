using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed, jumpHeight, wallJumpHeight, /*wallSlideSpeed, */ baseGrav, shortHopGrav, fallGrav, floatGrav;
    public LayerMask terrainMask;

    [HideInInspector] public bool floating = false;

    private float groundedSkin = 0.05f;
    private int isWallJumping = 0; // 0 = false, 1 = left, 2 = right
    private bool grounded, jump, wallLeft, wallRight;
    private Rigidbody2D rb;
    private Vector2 playerSize;
    private Vector2 movement = Vector2.zero;

    private void Awake() {
        playerSize = GetComponent<BoxCollider2D>().size;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        // Check for Walls --------------------------------------------------
        wallLeft = Physics2D.OverlapBox((Vector2)transform.position + (Vector2.left * playerSize.x) / 2f, new Vector2(groundedSkin, playerSize.y * 0.975f), 0f, terrainMask);
        wallRight = Physics2D.OverlapBox((Vector2)transform.position + (Vector2.right * playerSize.x) / 2f, new Vector2(groundedSkin, playerSize.y * 0.975f), 0f, terrainMask);
        // Jumping --------------------------------------------------
        if(Input.GetButtonDown("Jump"))
            jump = true;
    }

    private void FixedUpdate() {
        // Basic Movement --------------------------------------------------
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f)
            movement = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
        else
            movement = new Vector2(0, rb.velocity.y);
        // Wall Sliding
        /*if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f && (wallLeft || wallRight) && rb.velocity.y < -0.1f) {
            movement.y = wallSlideSpeed;
        }*/
        // Jumping --------------------------------------------------
        if(jump) {
            if(grounded) {
                movement.y = jumpHeight;
                grounded = false;
            } else if(wallLeft) {
                movement.y = wallJumpHeight;
                StartCoroutine(WallJump(true));
                //movement = new Vector2(-moveSpeed, rb.velocity.y);
            } else if(wallRight) {
                movement.y = wallJumpHeight;
                StartCoroutine(WallJump(false));
                //movement = new Vector2(moveSpeed, rb.velocity.y);
            }
            jump = false;
        } else
            grounded = Physics2D.OverlapBox((Vector2)transform.position + (Vector2.down * playerSize.y) / 2f, new Vector2(playerSize.x * 0.975f, groundedSkin), 0f, terrainMask);
        // Apply Wall Jump
        if(isWallJumping == 1) { // Left
            movement.x = moveSpeed;
        } else if(isWallJumping == 2) { // Right
            movement.x = -moveSpeed;
        }
        // Apply Movement --------------------------------------------------
        // Check for Wall Collision
        if(wallLeft && movement.x < -0.05f)
            movement.x = 0;
         else if(wallRight && movement.x > 0.05f)
            movement.x = 0;
        // Apply
        rb.velocity = movement;
        // Gravity --------------------------------------------------
        if(!floating) {
            if(rb.velocity.y < 0)
                rb.gravityScale = fallGrav;
            else if(rb.velocity.y > 0.05 && !Input.GetButton("Jump"))
                rb.gravityScale = shortHopGrav;
            else
                rb.gravityScale = baseGrav;
        } else
            rb.gravityScale = -floatGrav;
    }

    private IEnumerator WallJump(bool left) {
        if(left)
            isWallJumping = 1;
        else
            isWallJumping = 2;
        /*for(float i = 0; i < 0.5f; i += Time.fixedDeltaTime) {
            if(left)
                movement = new Vector2(-moveSpeed, rb.velocity.y);
            else
                movement = new Vector2(moveSpeed, rb.velocity.y);
            yield return new WaitForFixedUpdate();
        }*/
        yield return new WaitForSeconds(0.25f);
        isWallJumping = 0;
    }
}
