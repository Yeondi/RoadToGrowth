﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float MaxHealthPoints;
    public float startingHealthPoints;

    public string animationState = "AnimationeState";

    public enum States
    {
        idle = 1,
        run,
        jump,
        attack,
    }

    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

    public abstract void ResetCharacter();
    public abstract IEnumerator DamageCharacter(int damage, float interval);
    public virtual IEnumerator FlickerCharacter()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public virtual void pickUpItem(Collider2D collision)
    {

    }

    public virtual void purchaseItem(Collider2D collision)
    {

    }


    public abstract void AttackedByEnemy(int damage, float interval);

}
