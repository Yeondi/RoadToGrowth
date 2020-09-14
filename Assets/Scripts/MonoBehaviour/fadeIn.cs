using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeIn : MonoBehaviour
{
    public float FadeTime = 2f;
    Image fadeImg;
    float fStart;
    float fEnd;
    float fTime = 0f;
    bool isPlaying = false;

    private void Awake()
    {
        fadeImg = GetComponent<Image>();
        outStartFadeAnim();
        //inStartFadeAnim();
    }

    public void outStartFadeAnim()
    {
        if(isPlaying == true)
        {
            return;
        }
        fStart = 1f;
        fEnd = 0f;

        StartCoroutine("fadeOutPlay");
    }

    IEnumerator fadeOutPlay()
    {
        isPlaying = true;

        Color fadeColor = fadeImg.color;

        fTime = 0f;

        fadeColor.a = Mathf.Lerp(fStart, fEnd, fTime);

        while(fadeColor.a > 0f)
        {
            fTime += Time.deltaTime/FadeTime;
            fadeColor.a = Mathf.Lerp(fStart, fEnd, fTime);
            fadeImg.color = fadeColor;
            yield return null;
        }

        isPlaying = false;
        hideCanvas();
    }

    private void hideCanvas()
    {
        GameObject go= GameObject.Find("fadeCanvas");
        go.SetActive(false);
    }
}
