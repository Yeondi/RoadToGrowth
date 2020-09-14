using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endScene : MonoBehaviour
{
    public static endScene endSceneInstance = null;

    public bool theCurtainGoesDown = false;
    
    string saveSceneName;

    GameObject player;
    private void Awake()
    {
        if (endSceneInstance != null && endSceneInstance != this)
            Destroy(gameObject);
        else
            endSceneInstance = this;

    }

    public void saveScene()
    {
        saveSceneName = SceneManager.GetActiveScene().name;
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && player == null)
        {
            SceneManager.LoadScene(saveSceneName);
        }

        if(theCurtainGoesDown)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
