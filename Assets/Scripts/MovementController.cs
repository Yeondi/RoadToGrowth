using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float jumpSpeed = 5.0f;

    Vector2 movement = new Vector2();
    float jump;

    Animator animator;
    string animationState = "AnimationState";

    Rigidbody2D rb2D;
    SpriteRenderer renderer;

    bool isJumping;
    bool isFalling;

    public enum CharacterStates
    {
        walk = 1,
        attack,
        jump,
        hurt,
        die,
        fall,
        idle,
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
            animator.SetTrigger("doJumping");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attach : " + collision.gameObject.layer);

        if (collision.gameObject.layer == 10 && rb2D.velocity.y < 0)
            animator.SetBool("isJumping", false);

        if (collision.gameObject.layer == 9 && rb2D.velocity.y < 0)
        {
            animator.SetBool("isFalling", false);
            isFalling = false;
        }

        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            if (rb2D.velocity.y < 0 && rb2D.velocity.x != 0)
            {
                animator.SetInteger(animationState, (int)CharacterStates.walk);
            }
            else
                animator.SetInteger(animationState, (int)CharacterStates.idle);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        Jump();
        fall();
    }

    private void MoveCharacter()
    {
        Vector3 moveVelocity = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");

        jump = Input.GetAxisRaw("Vertical");

        if (movement.x > 0)
        {
            moveVelocity = Vector3.right;
            renderer.flipX = false;
            animator.SetBool("isMoving", true);
            animator.SetInteger(animationState, (int)CharacterStates.walk);
        }
        else if (movement.x < 0)
        {
            moveVelocity = Vector3.left;
            renderer.flipX = true;
            animator.SetBool("isMoving", true);
            animator.SetInteger(animationState, (int)CharacterStates.walk);
        }
        else
        {
            moveVelocity = Vector3.zero;
            animator.SetBool("isMoving", false);
            animator.SetInteger(animationState, (int)CharacterStates.idle);
        }

        transform.position += moveVelocity * movementSpeed * Time.deltaTime;
    }

    private void Jump()
    {
        if (!isJumping)
            return;

        rb2D.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpSpeed);

        animator.SetInteger(animationState, (int)CharacterStates.jump);
        rb2D.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    private void fall()
    {
        if (rb2D.velocity.y < 0)
        {
            isFalling = true;
            //animator.SetBool("isFalling", true);
            animator.SetInteger(animationState, (int)CharacterStates.fall);
        }
    }

    public Transform getTransform()
    {
        return GetComponent<GameObject>().transform;
    }
}
