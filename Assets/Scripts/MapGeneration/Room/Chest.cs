using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IInteractable
{
    private Item itemToDrop;
    private bool _isDropped = false;

    void Start()
    {
        itemToDrop = ItemsAsset.instance.GenerateItem();
        _isDropped = false;
    }

    void IInteractable.Interact()
    {
        if (!_isDropped)
        {
            Spawn();
            _isDropped = true;
        }
    }

    void Spawn()
    {
        ItemScene.SpawnItemScene(transform.position + new Vector3(0,-0.8f), itemToDrop);
        _isDropped = true;
        Destroy(this);
    }

    public string GetActionName()
    {
        return "Open";
    }
}
