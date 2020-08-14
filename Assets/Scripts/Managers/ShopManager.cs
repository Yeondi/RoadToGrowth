using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager sharedInstance = null;
    public GameObject[] items;

    public GameObject[] chosenItems;

    public Shop shop;

    public bool isShopOpen = false;

    public Room room;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
            Destroy(gameObject);
        else
            sharedInstance = this;
    }

    private void Start()
    {
        chosenItems = new GameObject[3];
        resetItems();
    }

    private void Update()
    {
        if (isShopOpen)
        {
            shop.initShop(room);
        }
    }

    private void resetItems()
    {
        //여기선 아이템 목록 불러와서 돌리기만
        int overlapNum = 0;
        for (int i = 0; i < 3; i++)
        {
            int nRandom = Random.Range(0, items.Length);
            if (nRandom == overlapNum && i != 0)
            {
                i--;
                continue;
            }

            chosenItems[i] = items[nRandom];

            overlapNum = nRandom;
        }
    }
}