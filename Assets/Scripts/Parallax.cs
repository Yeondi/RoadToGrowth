using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Transform mainCameraPosition;

    [SerializeField]
    private float backGroundMoveSpeed;
    private float directionX;

    [SerializeField]
    private float offsetByX = 13f;

    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        directionX = Input.GetAxis("Horizontal") * backGroundMoveSpeed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x + directionX, transform.position.y);
    }
}
