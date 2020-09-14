using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HealthPoints healthPoints;
    [HideInInspector]
    public Player character;

    public Image meterImage;

    public Text hpText;

    private float maxHealthPoints;

    void Start()
    {
        maxHealthPoints = character.MaxHealthPoints;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (character != null)
        {
            meterImage.fillAmount = healthPoints.value / maxHealthPoints;

            hpText.text = (meterImage.fillAmount * 100) + "%";
        }
    }
}
