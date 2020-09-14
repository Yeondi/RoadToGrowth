using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
    //private enum LadderPart
    //{
    //    complete,
    //    bottom,
    //    top
    //};

    //[SerializeField]
    //LadderPart part = LadderPart.complete;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.GetComponent<MovementController>())
    //    {
    //        MovementController player = collision.GetComponent<MovementController>();
    //        switch(part)
    //        {
    //            case LadderPart.complete:
    //                player.onLadder = true;
    //                player.ladder = this;
    //                break;
    //            case LadderPart.bottom:
    //                player.bottomLadder = true;
    //                break;
    //            case LadderPart.top:
    //                player.topLadder = true;
    //                break;
    //            default:
    //                Debug.Log("debug : ladder ontriggerEnter");
    //                break;
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<MovementController>())
    //    {
    //        MovementController player = collision.GetComponent<MovementController>();
    //        player.GetComponent<Rigidbody2D>().gravityScale = player.gravityStore;
    //        switch (part)
    //        {
    //            case LadderPart.complete:
    //                player.onLadder = false;
    //                break;
    //            case LadderPart.bottom:
    //                player.bottomLadder = false;
    //                break;
    //            case LadderPart.top:
    //                player.topLadder = false;
    //                break;
    //            default:
    //                Debug.Log("debug : ladder ontriggerExit");
    //                break;
    //        }
    //    }
    //}

    [SerializeField]
    private bool checkPlayer = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<MovementController>())
        {
            MovementController player = collision.GetComponent<MovementController>();
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.onLadder = true;
            Debug.Log("온 엔터");
            checkPlayer = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<MovementController>())
        {
            //MovementController player = collision.GetComponent<MovementController>();
            //player.GetComponent<Rigidbody2D>().gravityScale = 0;
            //player.onLadder = true;
            //Debug.Log("온 스테이");
            checkPlayer = true;
        }
    }

    private void Update()
    {
        if(checkPlayer)
        {
            MovementController player = GameObject.Find("Player").GetComponent<MovementController>();
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.onLadder = true;
        }
        else
        {
            MovementController player = GameObject.Find("Player").GetComponent<MovementController>();
            player.GetComponent<Rigidbody2D>().gravityScale = player.gravityStore;
            player.onLadder = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<MovementController>())
        {
            //MovementController player = collision.GetComponent<MovementController>();
            //player.GetComponent<Rigidbody2D>().gravityScale = player.gravityStore;
            //player.onLadder = false;
            checkPlayer = false;
        }
    }
}
