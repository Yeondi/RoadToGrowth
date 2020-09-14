using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shopNpc;
    [SerializeField]
    private bool checkInit = false;

    private Text priceText;

    private GameObject npc;

    public void initShop(Room room)
    {
        //룸 가운데에 npc소환
        if (!checkInit)
        {
            Debug.Log("상점주인 소환!" + room.name);
            Vector3 npcPos = new Vector3((room.pos.x + room.pos.width) - (room.pos.width / 2), room.pos.y + 3);
            npc = Instantiate(shopNpc, npcPos, Quaternion.identity) as GameObject;
            //npc.transform.Find("Area").GetComponent<BoxCollider2D>().size = new Vector2(room.pos.x + room.pos.width, room.pos.y + room.pos.height);
            //아이템 세개 띄우고 , 돈도 띄우고
            GameObject[] ChosenItems = new GameObject[3];
            GameObject go;
            int pos = 2;
            for (int i = 0; i < 3; i++)
            {
                go = ShopManager.sharedInstance.chosenItems[i];
                ChosenItems[i] = Instantiate(go, new Vector3(npcPos.x - pos,npcPos.y), Quaternion.identity) as GameObject;
                ChosenItems[i].GetComponent<Consumable>().isForSale = true;
                //ChosenItems[i].transform.Find("Canvas").Find("priceText").GetComponent<Text>().text = ChosenItems[i].GetComponent<Consumable>().item.price.ToString();
                ChosenItems[i].transform.GetChild(1).Find("Canvas").Find("priceText").GetComponent<Text>().text = ChosenItems[i].GetComponent<Consumable>().item.price.ToString();
                pos += 2;
            }
            checkInit = true;
        }
    }

    public void initShop(float x,float width)
    {

    }
}
