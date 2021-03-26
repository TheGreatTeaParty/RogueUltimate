using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectSlot : MonoBehaviour
{
    public Image Icon;
    public Effect Effect;

    public void SetEffect(Effect _effect)
    {
        Effect = _effect;
        Icon.sprite = _effect.Icon;
    }

    public void DeleteEffect()
    {
        Destroy(gameObject);
    }
}
