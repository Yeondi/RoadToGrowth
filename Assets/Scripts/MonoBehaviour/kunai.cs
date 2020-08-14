using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kunai : MonoBehaviour
{
    public bool onRotate = false;
    public int direction = 1;
    [SerializeField]
    [Range(1f,1000f)]
    float rotateSpeed = 50f;

    [SerializeField]
    float deleteCooldown = 5f;
    private void Update()
    {
        deleteCooldown -= Time.deltaTime;
        if(deleteCooldown <= float.Epsilon)
        {
            Destroy(this.gameObject);
            deleteCooldown = 3.5f;
        }    
        if(onRotate)
        {
            if (direction == 1)
                transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);
            else if (direction == -1)
                transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);

        }
        throwKunai();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            if (this.gameObject != null)
                Destroy(this.gameObject);
        }
    }

    public void throwKunai()
    {
        if (direction == 1 && (transform.localEulerAngles.z >= 260f && transform.localEulerAngles.z <= 290f))
        {
            onRotate = false;
            transform.localEulerAngles = new Vector3(0, 0, 270);
            GameObject.Find("Player").GetComponent<hero>().throwToDestination();
        }
        else if (direction == -1 && (transform.localEulerAngles.z >= 80f && transform.localEulerAngles.z <= 100f))
        {
            onRotate = false;
            transform.localEulerAngles = new Vector3(0, 0, 90);
            GameObject.Find("Player").GetComponent<hero>().throwToDestination();
        }
    }
}
