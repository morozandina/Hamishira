using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    
    public float speed;
    public float acceleration;
    public float jumpForce;

    private bool facingRight = true;
    private SpriteRenderer mySpriteRenderer;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    void Start() {
        extraJumps = extraJumpsValue;

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (rb.bodyType != RigidbodyType2D.Static) {
            if (speed != 0) {
                if (speed >= 15) {
                    speed = 15 * PlayerPrefs.GetFloat("ExtraSpeed");
                } else if (speed <= -15) {
                    speed = -15 * PlayerPrefs.GetFloat("ExtraSpeed");
                } else if (isGrounded == true) {
                    speed += (acceleration * Time.deltaTime) * PlayerPrefs.GetFloat("ExtraSpeed");
                }
            } else {
                speed = 0;
            }

            if(isGrounded == true){
                extraJumps = extraJumpsValue + PlayerPrefs.GetInt("ExtraJump");
            }

            if (isGrounded == true) {
                anim.SetBool("IsJumping", false);
            } else {
                anim.SetBool("IsJumping", true);
            }
        }
    }

    void FixedUpdate() {
        if (rb.bodyType != RigidbodyType2D.Static) {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            rb.velocity = new Vector2(speed * PlayerPrefs.GetFloat("ExtraSpeed"), rb.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(speed * PlayerPrefs.GetFloat("ExtraSpeed")));

            if(facingRight == false && speed > 0) {
                Flip();
            } else if(facingRight == true && speed < 0) {
                Flip();
            }   
        }
    }

    void Flip() {
        facingRight = !facingRight;
        mySpriteRenderer.flipX = !facingRight;
    }

    public void Jump() {
        if(extraJumps > 0) {
            anim.SetTrigger("TakeOf");
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        } else if(extraJumps == 0 && isGrounded == true) {
            anim.SetTrigger("TakeOf");
            rb.velocity = Vector2.up * jumpForce;
        }
    }
}
