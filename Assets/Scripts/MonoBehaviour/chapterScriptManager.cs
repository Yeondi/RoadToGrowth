using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chapterScriptManager : MonoBehaviour
{
    public float time = 3f;
    Canvas canvas;
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        StartCoroutine("ticking");
    }

    IEnumerator ticking()
    {
        float progress = 0f;

        while(progress <= time)
        {
            progress += Time.deltaTime;
            yield return null;
        }
        canvas.enabled = false;
    }
}
