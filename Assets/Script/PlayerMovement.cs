using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 8f;
    private enum MovementState { idle, run, jump, fall, attack}
    private MovementState state = MovementState.idle;
    bool isGrounded = true;
    bool isJump = false;
    bool collisionVerticalWall = false;
    bool isAttack = false;
    private string GROUND_TAG = "Ground";
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        if (!collisionVerticalWall || dirX <0f) rb.velocity = new Vector2(dirX*moveSpeed,rb.velocity.y);
        if (Input.GetButtonDown("Jump") && isJump == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            isJump = true;
        }
        if (Input.GetKeyDown(KeyCode.J)) isAttack = true;
        //if(collisionVerticalWall = true && Input.GetButtonDown("Jump"))
        //{
           // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //collisionVerticalWall = false;
           // isJump = true;
       // }
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        if (dirX > 0f)
        {
            if (isGrounded) state = MovementState.run;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            if (isGrounded) state = MovementState.run;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -.1f )
        {
            state = MovementState.fall;
        }
        if (isAttack)
        {
            state = MovementState.attack;
            isAttack = false;
        }
        anim.SetInteger("state", (int)state);
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
            isJump = false;
            collisionVerticalWall = false;
        }
        if (collision.gameObject.CompareTag("VerticalWall"))
        {
            collisionVerticalWall = true;
        }
    }
}
