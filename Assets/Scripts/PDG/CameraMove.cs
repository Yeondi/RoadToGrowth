using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject player;
    private GameObject kunai;
    private bool initiate;
    public bool goFollowKunai;

    private Camera mainCam;
    private Camera subCam;

    private void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        subCam = GameObject.Find("Sub Camera").GetComponent<Camera>();
    }

    public bool followingKunai()
    {
        if (goFollowKunai == true)
        {
            kunai = GameObject.Find("kunai");
            return true;
        }
        return false;
    }

    public void initScript()
    {
        initiate = true;
        player = GameObject.Find("Player");
    }

    public void cameraSwitching()
    {
        if (mainCam.enabled == true)
            subCam.enabled = false;
        if (subCam.enabled == true)
            mainCam.enabled = false;
    }

    void Update()
    {
        if (initiate)
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        if (followingKunai() && kunai != null)
            this.gameObject.transform.position = new Vector3(kunai.transform.position.x, kunai.transform.position.y, -10);

        cameraSwitching();
    }
}
