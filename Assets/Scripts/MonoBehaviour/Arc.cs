using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    Enemy enemy;

    public IEnumerator TravelArc(Vector3 destination,float duration)
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        if (enemy.GetComponent<Weapon>().onFire)
            yield return null;

        Vector3 startPosition = transform.position;

        float percentComplete = 0.0f;

        while(percentComplete < 1.0f)
        {
            percentComplete += Time.deltaTime / duration;

            transform.position = Vector3.Lerp(startPosition, destination, percentComplete);

            yield return null;
        }
        gameObject.SetActive(false);
        enemy.GetComponent<Weapon>().onFire = false;
    }
}
