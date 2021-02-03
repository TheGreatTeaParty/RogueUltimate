using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuy : MonoBehaviour
{
    private SpriteRenderer renderer;
    private static readonly int Fade = Shader.PropertyToID("_Fade");

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.material = new Material(GetComponent<SpriteRenderer>().material);
    }

    public IEnumerator SlowDissolve(bool state)
    {
        if (state)
        {
            float time = 0;
            while (time <= 1)
            {
                time += Time.deltaTime;
                renderer.material.SetFloat(Fade, time);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        else
        {
            float time = 1;
            while (time >= 0)
            {
                time -= Time.deltaTime;
                renderer.material.SetFloat(Fade, time);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}
