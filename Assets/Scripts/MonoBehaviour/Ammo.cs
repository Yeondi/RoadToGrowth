using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int damageInflicted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is BoxCollider2D)
        {
            Player player = collision.gameObject.GetComponent<Player>();

            StartCoroutine(player.DamageCharacter(damageInflicted, 0.0f));

            gameObject.SetActive(false);
        }
    }
}
