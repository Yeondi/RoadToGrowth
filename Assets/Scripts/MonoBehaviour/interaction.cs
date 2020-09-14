using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum interactionType
{
    talkToNpc,
    usePortal,
    interactDevice
}
public class interaction : MonoBehaviour
{
    public bool onPortal;

    public AsyncOperation asyncScene;

    [SerializeField]
    private Sprite announcement;
    private Image loadingBar;

    //말걸기,포탈타기,장치작동
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //플레이어 키입력 == w
            onPortal = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            onPortal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onPortal = false;
        }
    }

    private void Update()
    {
        if (onPortal && (Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Fire2")))
        {
            int SceneNumber = SceneManager.GetActiveScene().buildIndex + 1;

            SceneManager.LoadScene(SceneNumber);
            //loadScene();
        }
    }

    public void loadScene()
    {
        asyncScene = SceneManager.LoadSceneAsync("Random-Dungeon");
        asyncScene.allowSceneActivation = false;
        float progressAmount = loadingBar.GetComponentInChildren<Image>().fillAmount;
        float loadTime = 0f;
        Instantiate(announcement, new Vector3(-100, 0), Quaternion.identity);
        while (!asyncScene.isDone)
        {
            //Camera.main.transform.position = new Vector3(-100, 0,-10);
            loadTime += Time.deltaTime;
            if (asyncScene.progress >= 0.9f)
            {
                progressAmount = Mathf.Lerp(progressAmount, 1, loadTime);
                if (progressAmount == 1.0f)
                {
                    asyncScene.allowSceneActivation = true;
                }
            }
            else
            {
                progressAmount = Mathf.Lerp(progressAmount, asyncScene.progress, loadTime);
                if (progressAmount >= asyncScene.progress)
                    loadTime = 0f;
            }
        }
    }
}
