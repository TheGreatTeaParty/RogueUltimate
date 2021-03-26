using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollButton : MonoBehaviour
{
    PlayerMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        movement = CharacterManager.Instance.Stats.playerMovement;
    }

    public void Roll()
    {
        movement.Roll();
    }
}
