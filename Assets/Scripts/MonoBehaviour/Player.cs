using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : Character
{
    public int damageStrength;
    public int defence;
    public HealthPoints healthPoints;

    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    public Inventory inventoryPrefab;
    Inventory inventory;

    private Animator animator;
    private Rigidbody2D rigid;

    MovementController movementController;

    Coroutine damageCoroutine;

    bool readyToPayment;

    private Collider2D saveCollision;

    private purchaseGuidance canPurchase;

    public void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        movementController = GameObject.Find("Player").GetComponent<MovementController>();
        canPurchase = GameObject.Find("canPurchase").GetComponent<purchaseGuidance>();
        //movementController = GameObject.Find("TPlayerObject(Clone)").GetComponent<MovementController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Consumable consumable = collision.gameObject.GetComponent<Consumable>();
            readyToPayment = true;
            saveCollision = collision;
            if (consumable.isForSale == false)
                pickUpItem(collision);
            else
            {
                canPurchase = collision.gameObject.GetComponentInChildren<purchaseGuidance>();
                canPurchase.guidance();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CanBePickedUp"))
        {
            readyToPayment = false;
            canPurchase.resetGuidance();
        }
    }

    public override void pickUpItem(Collider2D collision)
    {
        Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
        if (hitObject != null)
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
                    Debug.Log("END");
                    shouldDisappear = true;
                    break;
                default:
                    break;
            }

            if (shouldDisappear)
                collision.gameObject.SetActive(false);

        }
    }

    public override void purchaseItem(Collider2D collision)
    {
        Debug.Log("purchaseItem");
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire2")))
            {
                bool shouldDisappear = false;

                switch (hitObject.itemType)
                {
                    case Item.ItemType.HEALTH:
                        if (hitObject.price <= inventory.amountMoney)
                        {
                            shouldDisappear = AdjustHitPoints(hitObject.quantity);
                            inventory.amountMoney -= hitObject.price;
                        }
                        else
                            Debug.Log(hitObject.name + " 돈이 부족합니다.");
                        break;
                    case Item.ItemType.ATK_BUFF:
                        if (hitObject.price <= inventory.amountMoney)
                        {
                            shouldDisappear = atkPowerUp();
                            inventory.AddItem(hitObject);
                            inventory.amountMoney -= hitObject.price;
                        }
                        else
                            Debug.Log(hitObject.name + " 돈이 부족합니다.");
                        break;
                    case Item.ItemType.DF_BUFF:
                        if (hitObject.price <= inventory.amountMoney)
                        {
                            shouldDisappear = defenceUp();
                            inventory.AddItem(hitObject);
                            inventory.amountMoney -= hitObject.price;
                        }
                        else
                            Debug.Log(hitObject.name + " 돈이 부족합니다.");
                        break;
                    default:
                        break;
                }
                if (shouldDisappear)
                    collision.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if(readyToPayment && Input.GetButtonDown("Fire2"))
        {
            purchaseItem(saveCollision);
        }
    }

    public bool AdjustHitPoints(int amount)
    {
        if (healthPoints.value < MaxHealthPoints)
        {
            healthPoints.value = healthPoints.value + amount;
            print("Adjusted hitpoints by : " + amount + ". New Value " + healthPoints);
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

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());
            healthPoints.value = healthPoints.value - damage;

            if (healthPoints.value <= float.Epsilon)
            {
                KillCharacter();
                break;
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    public override void KillCharacter()
    {
        base.KillCharacter();
        animator.SetInteger(animationState, (int)MovementController.CharacterStates.die);

        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);

        SceneManager.LoadScene("DIEScene");
    }

    public override void ResetCharacter()
    {
        if (GameObject.Find("Inventory") == null)
        {
            inventory = Instantiate(inventoryPrefab);
            inventory.name = "Inventory";
        }
        else
        {
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
            }
        }
        if (GameObject.Find("HealthBar") == null)
        {
            healthBar = Instantiate(healthBarPrefab);
            healthBar.character = this;
            healthBar.name = "HealthBar";
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                healthBar= GameObject.Find("HealthBar").GetComponent<HealthBar>();
            }
        }

        healthPoints.value = startingHealthPoints;
    }

    private void OnEnable()
    {
        ResetCharacter();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (movementController.onAttack)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                damageCoroutine = StartCoroutine(enemy.DamageCharacter(damageStrength, 1.0f));
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!(movementController.onAttack) && damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }
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

    public void invincibility(Transform target)
    {
        gameObject.layer = 19; // 무적

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);

        int direction = transform.position.x - target.position.x > 0 ? 2 : -2;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 1), ForceMode2D.Impulse);

        Invoke("offInvincibility", 1.5f);
    }

    public void offInvincibility()
    {
        gameObject.layer = 13;

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
