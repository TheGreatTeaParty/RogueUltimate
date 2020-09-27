using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LampRender : MonoBehaviour
{
    public SpriteRenderer lampSprite;
    public Light2D lampLight;

    
    
    public void TurnTheLamp(bool state)
    {
        lampLight.enabled = state;
    }
}
