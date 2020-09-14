using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance = null;
    public RPGCameraManager cameraManager;

    public SpawnPoint playerSpawnPoint;

    public ShopManager shopManager;

    public endScene endScene;


    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
            Destroy(gameObject);
        else
            sharedInstance = this;

    }

    void Start()
    {
        SetupScene();
    }

    //void Update()
    //{
    //    if(Input.GetKey("escape"))
    //    {
    //        Application.Quit();
    //    }
    //}

    public void SetupScene()
    {
        SpawnPlayer();
        EndScene();
    }

    public void SpawnPlayer()
    {
        if(playerSpawnPoint != null)
        {
            GameObject player = playerSpawnPoint.SpawnObject();
            player.name = "Player";
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }

    public void EndScene()
    {
        if(endScene != null)
        {
            endScene.saveScene();
        }
    }
}
