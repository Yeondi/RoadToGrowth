using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Trap : MonoBehaviour
{
    Player player;
    Enemy enemy;
    public enum testA
    {
        spikeSmall = 10,
        spikeLarge = 30,
        Magma = 9999,
        water = 100
    }
    public testA test;

    Coroutine onHitRoutine;
    bool onHit;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
            player.AttackedByEnemy((int)test, 2.0f);
            onHit = true;
        }                       
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.AttackedByEnemy((int)test, 2.0f);
            onHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(onHit)
            StopCoroutine(onHitRoutine);
    }

}
