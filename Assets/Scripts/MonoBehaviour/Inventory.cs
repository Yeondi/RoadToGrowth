using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab;

    public const int numSlots = 5;

    Image[] itemImages = new Image[numSlots];
    Item[] items = new Item[numSlots];

    GameObject[] slots = new GameObject[numSlots];

    public int amountMoney = 0;

    [SerializeField]
    private Text moneyAmount;

    public struct saveData
    {
        public string itemName;
        public int amount;
    }

    saveData[] saveDatas = new saveData[numSlots];

    void Start()
    {
        CreateSlots();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        moneyAmount.text = "$ "+amountMoney;
    }

    public void CreateSlots()
    {
        if (slotPrefab != null)
        {
            for (int i = 0; i < numSlots; i++)
            {
                GameObject newSlot = Instantiate(slotPrefab);
                newSlot.name = "ItemSlot_" + i;

                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);

                slots[i] = newSlot;

                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }

    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (itemToAdd.itemType == Item.ItemType.COIN)
            {
                amountMoney += 10;
                return true;
            }
            if (items[i] != null && items[i].itemType == itemToAdd.itemType && itemToAdd.Stackable == true)
            {
                items[i].quantity = items[i].quantity + 1;

                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();

                Text quantityText = slotScript.qtyText;

                quantityText.enabled = true;

                quantityText.text = items[i].quantity.ToString();

                saveDatas[i].itemName = itemToAdd.objectName;
                saveDatas[i].amount = items[i].quantity;



                return true;
            }
            if (items[i] == null)
            {
                //아이템을 빈 슬롯에 추가
                items[i] = Instantiate(itemToAdd);
                items[i].quantity = 1;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                saveDatas[i].itemName = itemToAdd.objectName;
                saveDatas[i].amount = items[i].quantity;
                return true;
            }
        }
        return false;
    }
}
