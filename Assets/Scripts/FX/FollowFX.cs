using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFX : MonoBehaviour
{
    private GameObject Player;

    // Start is called before the first frame update
    void Awake()
    {
        Player = KeepOnScene.Instance.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Player.transform.position;
    }
}
