using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRun : MonoBehaviour
{
    public float jumpForce = 10f;
    public float wallJumpPush = 5f;
    public LayerMask wallLayer;

    private Rigidbody2D rb;
    private bool isTouchingWall;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isTouchingWall = Physics2D.Raycast( transform.position, Vector2.right * transform.localScale.x, 0.6f, wallLayer );
        isGrounded = Physics2D.Raycast( transform.position, Vector2.down, 1.1f, wallLayer );

        if( Input.GetKeyDown( KeyCode.Space ) && isTouchingWall && !isGrounded )
        {
            WallJumpAction();
        }
    }

    void WallJumpAction()
    {
        rb.velocity = new Vector2( -Mathf.Sign( transform.localScale.x ) * wallJumpPush, jumpForce );
    }
}
