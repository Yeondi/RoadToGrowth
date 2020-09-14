using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    float healthPoints;
    public int damageStrength;
    public float attackCoolDown;
    public float attackDelay;
    public float movementSpeed;

    public float wanderCoolDown;
    public float wanderDelay;

    Animator animator;
    private Rigidbody2D rb2d;
    private Sensor_Bandit m_groundSensor;
    public bool onAttack = false;
    private bool grounded = false;
    private bool combatIdle = false;
    private bool isDead = false;

    private int revivalCount = 3;

    public int currentDirection = 0;

    public int nAttackCount = 1;

    [HideInInspector]
    public Transform player;

    [HideInInspector]
    public Player playerObject;

    public dropItem drop;

    void Start()
    {
        ResetCharacter();
        animator = GetComponent<Animator>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.Find("Player").transform;
        playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb2d = GetComponent<Rigidbody2D>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>().setBossData(this);
    }

    void Update()
    {
        if (attackDelay <= 0.1f)
        {
            onAttack = true;
        }
        if (attackDelay >= 0)
        {
            attackDelay -= Time.deltaTime;
        }

        if (wanderDelay >= 0)
        {
            wanderDelay -= Time.deltaTime;
        }
        Debug.Log("Boss hp : " + healthPoints);
        if (rb2d.velocity == Vector2.zero)
        {
            animator.SetFloat("behaviour", 2.0f);
        }

        if (onAttack)
        {
            Transform melee = transform.Find("melee");
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(melee.position, new Vector2(1.0f, 1.8f), 0);

            for (int i = 0; i < collider2Ds.Length; i++)
            {
                if (collider2Ds[i].tag == "Player" && melee.GetComponent<BoxCollider2D>().enabled == true && nAttackCount == 1)
                {
                    playerObject.AttackedByEnemy(damageStrength, 2.0f);
                    nAttackCount = 0;
                    onAttack = false;
                    break;
                }
            }
        }
    }

    public override void AttackedByEnemy(int damage, float interval)
    {
        StartCoroutine(FlickerCharacter());
        healthPoints = healthPoints - damage;

        if (healthPoints <= float.Epsilon)
        {
            if (revivalCount == 0)
            {
                KillCharacter();
                drop.BossDrop(transform.position);
            }
            revivalCount--;
            StartCoroutine(revival());
        }

        if (interval > float.Epsilon)
        {
            interval -= Time.deltaTime;
        }

        StopCoroutine(FlickerCharacter());
    }

    private IEnumerator revival()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1.0f);
        animator.SetTrigger("Recover");
        yield return new WaitForSeconds(5.0f);
        ResetCharacter();
    }

    public void direction(float target, float baseobj)
    {
        if (target < baseobj)
        {
            //animator.SetInteger("direction", -1);
            transform.localScale = new Vector3(5, 5, 0);
        }
        else
        {
            //animator.SetInteger("direction", 1);
            transform.localScale = new Vector3(-5, 5, 0);
        }
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            animator.SetTrigger("Hurt");

            healthPoints -= damage;

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
                break;
        }
    }

    public override void ResetCharacter()
    {
        healthPoints = startingHealthPoints;
    }
}
