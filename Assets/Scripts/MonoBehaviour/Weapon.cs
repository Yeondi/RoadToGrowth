using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject ammoPrefab;
    static List<GameObject> ammoPool;
    public int poolSize;
    public float weaponVelocity;

    public bool onFire;


    private void Awake()
    {
        if (ammoPool == null)
        {
            ammoPool = new List<GameObject>();
        }

        for(int i=0;i<poolSize;i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.name = "Bullet_" + i.ToString("00");
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }

    GameObject SpawnAmmo(Vector3 location)
    {
        foreach(GameObject ammo in ammoPool)
        {
            if(ammo.activeSelf == false)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }
        }
        return null;
    }

    public void FireAmmo(Vector3 position)
    {
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject ammo = SpawnAmmo(transform.position);

        if(ammo != null)
        {
            onFire = true;
            Arc arcScript = ammo.GetComponent<Arc>();

            float travelDuration = 1.0f / weaponVelocity;

            StartCoroutine(arcScript.TravelArc(position, travelDuration));
        }
    }

    private void OnDestroy()
    {
        ammoPool = null;
    }
}
