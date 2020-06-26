using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public HealthPoints healthPoints;

    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    public Inventory inventoryPrefab;
    Inventory inventory;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
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
                    default:
                        break;
                }

                if (shouldDisappear)
                    collision.gameObject.SetActive(false);

            }
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

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
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
    }

    public override void ResetCharacter()
    {
        inventory = Instantiate(inventoryPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;

        healthPoints.value = startingHealthPoints;
    }

    private void OnEnable()
    {
        ResetCharacter();
    }
}
