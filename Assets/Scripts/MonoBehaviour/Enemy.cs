using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    float healthPoints;

    public int damageStrength;

    Coroutine damageCoroutine;

    public float attackCoolDown;
    public float attackDelay;
    public float movementSpeed;

    [HideInInspector]
    public Transform player;
    
    private Animator animator;

    [HideInInspector]
    public Vector2 backToPosition;


    private SpriteRenderer renderer;

    public int nAttackCount = 1;

    public dropItem drop;
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        backToPosition = transform.position;
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(attackDelay >= 0)
        {
            attackDelay -= Time.deltaTime;
        }

        if(attackDelay >= 3.9f)
        {
            Weapon weapon = gameObject.GetComponent<Weapon>();
            weapon.onFire = false;
        }

    }
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());

            healthPoints = healthPoints - damage;

            if (healthPoints <= float.Epsilon)
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

    public override void ResetCharacter()
    {
        healthPoints = startingHealthPoints;
    }

    private void OnEnable()
    {
        ResetCharacter();
    }

    public void direction(float target , float baseobj)
    {
        if (target < baseobj)
        {
            animator.SetInteger("direction", -1);
            renderer.flipX = false;
        }
        else
        {
            animator.SetInteger("direction", 1);
            renderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            MovementController movement = collision.gameObject.GetComponent<MovementController>();
            if (damageCoroutine == null && movement.onAttack == false)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
                player.invincibility(gameObject.transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    public override void AttackedByEnemy(int damage, float interval)
    {
        StartCoroutine(FlickerCharacter());

        healthPoints = healthPoints - damage;

        if (healthPoints <= float.Epsilon)
        {
            KillCharacter();
        }

        if (interval > float.Epsilon)
        {
            interval -= Time.deltaTime;
        }

        StopCoroutine(FlickerCharacter());
    }

    public override void KillCharacter()
    {
        base.KillCharacter();
        //아이템 드랍
        drop.enemyDrop(transform.position);
    }

}
