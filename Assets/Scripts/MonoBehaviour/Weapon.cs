using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject ammoPrefab;
    static List<GameObject> ammoPool;
    public int poolSize;

    private void Awake()
    {
        if(ammoPool == null)
        {
            ammoPool = new List<GameObject>();
        }

        for(int i=0;i<poolSize;i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FireAmmo();
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

    private void FireAmmo()
    {

    }

    private void OnDestroy()
    {
        ammoPool = null;
    }
}
