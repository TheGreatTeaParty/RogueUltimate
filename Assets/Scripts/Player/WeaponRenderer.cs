using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    private PlayerMovment playerMovment;
    private Renderer PlayerSprite;
    private Renderer WeaponSprite;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        playerMovment = KeepOnScene.instance.GetComponent<PlayerMovment>();
        PlayerSprite = KeepOnScene.instance.GetComponent<Renderer>();
        WeaponSprite = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = playerMovment.GetDirection();

        if(direction.y > 0.01f && (direction.x < 0.2f && direction.x > - 0.2f))
        {
            WeaponSprite.sortingOrder = PlayerSprite.sortingOrder - 1;
        }

        else
        {
            WeaponSprite.sortingOrder = PlayerSprite.sortingOrder + 1;
        }
    }
}
