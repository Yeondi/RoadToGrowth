using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class purchaseGuidance : MonoBehaviour
{
    SpriteRenderer renderer;
    Text priceText;
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    public void guidance()
    {
        renderer.enabled = true;
        priceText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        priceText.enabled = true;

        priceText.text = "$ " + transform.parent.GetComponent<Consumable>().item.price;
        while (transform.localPosition.y <= 1.0f)
        {
            transform.localPosition += new Vector3(0f, 0.2f);
        }
    }

    public void resetGuidance()
    {
        while (transform.localPosition.y >= 0f)
        {
            transform.localPosition += new Vector3(0f, -0.2f);
        }
        renderer.enabled = false;
    }
}
