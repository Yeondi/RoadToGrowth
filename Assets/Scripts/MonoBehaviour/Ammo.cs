using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int damageInflicted;
    float timer = 0f;
    [HideInInspector]
    public Vector2 originalSpawnPosition;

    private void Start()
    {
        originalSpawnPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            Player player = collision.gameObject.GetComponent<Player>();

            StartCoroutine(player.DamageCharacter(damageInflicted, 1.0f));
            if (gameObject == null)
                Debug.Log("이게 에런가?");
            gameObject.SetActive(false);
        }
    }
}
