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
    public bool onAttack;


    private int playerCurrentDirection;

    PolygonCollider2D childCollider;

    public bool onLadder;
    public float climbSpeed;
    private float climbDirection;
    private float gravityStore;

    public int nAttackCount = 1;

    Boss boss;

    Player player;
    public enum CharacterStates
    {
        walk = 1,
        attack,
        jump,
        hurt,
        die,
        fall,
        idle,
        climb,
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        childCollider = GetComponentInChildren<PolygonCollider2D>();

        gravityStore = rb2D.gravityScale;

        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
            animator.SetTrigger("doJumping");
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Fire3"))
        {
            Debug.Log("공격키 눌림");
            animator.SetTrigger("onAttack");
            attack();
        }

        if (!onAttack)
        {

            MoveCharacter();
            fall();

        }

        if(onLadder)
        {
            rb2D.gravityScale = 0f;

            climbDirection = climbSpeed * Input.GetAxisRaw("Vertical");

            rb2D.velocity = new Vector2(rb2D.velocity.x, climbDirection);
        }

        if(!onLadder)
        {
            rb2D.gravityScale = gravityStore;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 10 && rb2D.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            isJumping = false;
            Debug.Log("점프 초기화!");
        }
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

    private void MoveCharacter()
    {
        Vector3 moveVelocity = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");

        if (movement.x > 0)
        {
            moveVelocity = Vector3.right;
            renderer.flipX = false;
            animator.SetBool("isMoving", true);
            animator.SetInteger(animationState, (int)CharacterStates.walk);
            playerCurrentDirection = 1;
        }
        else if (movement.x < 0)
        {
            moveVelocity = Vector3.left;
            renderer.flipX = true;
            animator.SetBool("isMoving", true);
            animator.SetInteger(animationState, (int)CharacterStates.walk);
            playerCurrentDirection = -1;
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

    private void attack()
    {
        if (playerCurrentDirection == 1)
        {
            childCollider.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (playerCurrentDirection == -1)
        {
            childCollider.transform.localScale = new Vector3(-1, 1, 1);
        }
        onAttack = true;
        animator.SetTrigger("onAttack");
        animator.SetInteger(animationState, (int)CharacterStates.attack);
        Invoke("offAttack", 0.5f);
    }

    private void offAttack()
    {
        animator.SetBool("onAttack", false);
        onAttack = false;
        nAttackCount = 1;
    }

    public void setBossData(Boss bs)
    {
        boss = bs;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Boss")
        {
            if (onAttack && nAttackCount == 1)
            {
                collision.gameObject.GetComponent<Boss>().AttackedByEnemy(player.damageStrength, 2.0f);
                Debug.Log(player.damageStrength);
                nAttackCount = 0;
            }
        }
    }
}
