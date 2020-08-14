using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropItem : MonoBehaviour
{
    public GameObject[] items;
    private GameObject go;
    private Rigidbody2D rb2d;
    private CircleCollider2D circle;

    public bool isDropItem = false;

    public void enemyDrop(Vector2 EnemyPos)
    {
        isDropItem = true;
        int nRandom = Random.Range(0, items.Length);
        go = Instantiate(items[nRandom], EnemyPos, Quaternion.identity) as GameObject;
        circle = go.GetComponent<CircleCollider2D>();
        Invoke("getItem", 0.5f);

        rb2d = go.GetComponent<Rigidbody2D>();

        int nLeftRight = Random.Range(-3, 4);
        int nUpDown = Random.Range(0, 3);

        nLeftRight *= 6;
        nUpDown *= 6;

        rb2d.AddForce(new Vector2(nLeftRight, nUpDown));
    }

    public void getItem()
    {
        circle.enabled = true;
        Debug.Log(circle.enabled);
    }

    public void BossDrop(Vector2 BossPos)
    {
        isDropItem = true;
        Debug.Log("대충 보스가 좋은 아이템 드랍함");
    }
}
