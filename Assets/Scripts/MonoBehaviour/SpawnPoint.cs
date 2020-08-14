using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float repeatInterval; // 일정한 간격을 두고 스폰을 위한 인터벌값

    void Start()
    {
        if(repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }
    }

    public GameObject SpawnObject()
    {
        if(prefabToSpawn != null)
        {
            GameObject spawn = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            spawn.name = "Shade";
            return spawn; //인스턴스화 :: Quaternion.identity == 회전이 없음
        }
        return null;
    }
}
