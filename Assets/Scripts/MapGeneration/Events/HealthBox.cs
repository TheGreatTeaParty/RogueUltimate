using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBox : MonoBehaviour, IInteractable
{
    public Sprite empty;

    private SpriteRenderer spriteRenderer;

    public string GetActionName()
    {
        return "Drink";
    }

    public void Interact()
    {
        var player = CharacterManager.Instance.Stats;
        player.CurrentHealth = player.Strength.MaxHealth.Value;
        spriteRenderer.sprite = empty;
        AudioManager.Instance.Play("Drink");
        Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
}
