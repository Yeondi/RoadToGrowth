using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float MaxHealthPoints;
    public float startingHealthPoints;

    public Animator animator;
    public string animationState = "AnimationeState";

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

    public abstract void ResetCharacter();
    public abstract IEnumerator DamageCharacter(int damage, float interval);
}
