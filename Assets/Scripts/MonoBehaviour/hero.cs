using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hero : Character
{
    Animator animator;
    Rigidbody2D rb2D;
    [SerializeField]
    private float playerVelocity;
    private int playerDirection = 1;
    [SerializeField]
    private int attackLevel = 0;

    [SerializeField]
    private float jumpSpeed = 6f;

    [SerializeField]
    private float attackDelay = 0f;
    [SerializeField]
    private float attackCoolDown = 1f;

    public GameObject weapon;
    GameObject kunai;
    public Vector2 kunaiVelocity;
    public float kunaiDirection;

    public int damageStrength;
    public int defence;

    public HealthPoints healthPoints;

    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    public Inventory inventoryPrefab;
    Inventory inventory;

    [HideInInspector]
    public float backupInputAxisVertical;

    private Collider2D saveCollision;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        //animator.SetTrigger("start");
    }

    void Update()
    {
        movement();

        if (attackDelay >= float.Epsilon)
            attackDelay -= Time.deltaTime;

        if ((Input.GetKey(KeyCode.X) || Input.GetButtonDown("Fire3")) && attackDelay <= float.Epsilon)
        {
            attackDelay = attackCoolDown;
            if (attackLevel == 0)
            {
                animator.SetInteger("attackLevel", 1);
                attackLevel = 1;
            }
            else if (attackLevel == 1)
            {
                animator.SetInteger("attackLevel", 2);
                attackLevel = 2;
            }
            else if (attackLevel == 2)
            {
                animator.SetInteger("attackLevel", 3);
                attackLevel = 3;
            }
            StartCoroutine("attack");
        }

        if(Input.GetKey(KeyCode.C) || Input.GetButtonDown("Jump"))
        {
            jump();
        }

        if ((Input.GetKey(KeyCode.Q) || Input.GetButtonDown("Fire2")) && kunai == null)
        {
            backupInputAxisVertical = Input.GetAxisRaw("Vertical");
            throwWeapon();
        }
        if (kunai != null && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Jump")))
            teleportToKunai();
    }

    private void throwWeapon()
    {
        //Quaternion rot = playerDirection == -1 ? Quaternion.Euler(0, 0, -270f) : Quaternion.Euler(0, 0, 270f);
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        kunai = Instantiate(weapon, transform.position, rot) as GameObject;
        kunai.name = "kunai";
        kunai.GetComponent<kunai>().onRotate = true;
        kunai.GetComponent<kunai>().direction = playerDirection;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Hero onTrigger Test");
    //    Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
    //    if (hitObject != null)
    //    {
    //        bool shouldDisappear = false;
    //        switch (hitObject.itemType)
    //        {
    //            case Item.ItemType.END:
    //                shouldDisappear = true;
    //                break;
    //            case Item.ItemType.COIN:
    //                shouldDisappear = inventory.AddItem(hitObject);
    //                break;
    //            default:
    //                break;
    //        }

    //        if (shouldDisappear)
    //            collision.gameObject.SetActive(false);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Consumable consumable = collision.gameObject.GetComponent<Consumable>();
            saveCollision = collision;
            if (consumable.isForSale == false)
                pickUpItem(collision);
        }
    }

    public void throwToDestination()
    {
        animator.SetTrigger("attack");
        Rigidbody2D rigid = kunai.GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;

        if (Input.GetAxisRaw("Vertical") <= 0.5f && Input.GetAxisRaw("Vertical") >= -0.5f)
        {
            if (transform.localScale.x > 0)
            {
                kunaiDirection = 1;
            }
            else if (transform.localScale.x < 0)
            {
                kunaiDirection = -1;
            }
            kunaiVelocity = new Vector2(10f, 0f);
        }
        else
        {
            //kunai velocity 값 조정
            kunaiVelocity = new Vector2(0f, 10f);
            if (Input.GetAxisRaw("Vertical") > 0)
                kunaiDirection = 1;
            else if (Input.GetAxisRaw("Vertical") < 0)
                kunaiDirection = -1;
        }

        GameObject.Find("Sub Camera").GetComponent<CameraMove>().goFollowKunai = true;
        GameObject.Find("Sub Camera").GetComponent<Camera>().enabled = true;

        if (rigid.velocity.x <= 15f)
            rigid.velocity += kunaiVelocity * kunaiDirection * Time.deltaTime;
        else
            rigid.velocity = new Vector2(15, 0);
    }

    private void teleportToKunai()
    {
        Debug.Log("텔레포트 위치 :: " + kunai.transform.position);
        transform.position = new Vector3(kunai.transform.position.x, kunai.transform.position.y);
        Destroy(kunai);
    }

    private void movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        Vector3 moveVelocity = Vector3.zero;

        if (move > 0)
        {
            animator.SetInteger("state", (int)States.run);
            transform.localScale = new Vector3(1, 1, 1);
            moveVelocity = Vector3.right;
            playerDirection = 1;
        }
        else if (move < 0)
        {
            animator.SetInteger("state", (int)States.run);
            transform.localScale = new Vector3(-1, 1, 1);
            moveVelocity = Vector3.left;
            playerDirection = -1;
        }
        else
        {
            animator.SetInteger("state", (int)States.idle);
            moveVelocity = Vector3.zero;
        }
        transform.position += moveVelocity * playerVelocity * Time.deltaTime;
    }

    private IEnumerator attack()
    {
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(0.5f);

        if(attackLevel == 3)
            attackLevel = 0;
    }

    private void jump()
    {
        rb2D.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpSpeed);

        animator.SetTrigger("jump");
        rb2D.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    public override void AttackedByEnemy(int damage, float interval)
    {
        StartCoroutine(FlickerCharacter());
        healthPoints.value = healthPoints.value - (damage - defence);

        if (healthPoints.value <= float.Epsilon)
        {
            KillCharacter();
        }

        if (interval > float.Epsilon)
        {
            interval -= Time.deltaTime;
        }

        StopCoroutine(FlickerCharacter());
    }

    public override IEnumerator FlickerCharacter()
    {
        return base.FlickerCharacter();
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        throw new System.NotImplementedException();
    }

    public override void ResetCharacter()
    {
        throw new System.NotImplementedException();
    }

    public override void pickUpItem(Collider2D collision)
    {
        Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
        Consumable consumable = collision.gameObject.GetComponent<Consumable>();
        if (hitObject != null && consumable.isForSale == false)
        {
            bool shouldDisappear = false;

            switch (hitObject.itemType)
            {
                case Item.ItemType.COIN:
                    shouldDisappear = inventory.AddItem(hitObject);
                    break;
                case Item.ItemType.HEALTH:
                    shouldDisappear = AdjustHitPoints(hitObject.quantity);
                    break;
                case Item.ItemType.ATK_BUFF:
                    shouldDisappear = atkPowerUp();
                    inventory.AddItem(hitObject);
                    break;
                case Item.ItemType.DF_BUFF:
                    shouldDisappear = defenceUp();
                    inventory.AddItem(hitObject);
                    break;
                case Item.ItemType.END:
                    shouldDisappear = true;
                    endScene.endSceneInstance.theCurtainGoesDown = true;
                    break;
                default:
                    break;
            }

            if (shouldDisappear)
                collision.gameObject.SetActive(false);

        }
    }

    public bool AdjustHitPoints(int amount)
    {
        if (healthPoints.value < MaxHealthPoints)
        {
            healthPoints.value = healthPoints.value + amount;
        }
        return true;
    }

    public bool atkPowerUp()
    {
        damageStrength += 10;
        Debug.Log("현재 파워 : " + damageStrength);
        return true;
    }

    public bool defenceUp()
    {
        defence += 5;
        return true;
    }


}
