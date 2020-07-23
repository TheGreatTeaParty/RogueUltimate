using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IInteractable
{
    private Item itemToDrop;
    private bool _isDropped = false;
    private Animator animator;

    void Start()
    {
        itemToDrop = ItemsAsset.instance.GenerateItem();
        _isDropped = false;
        animator = GetComponent<Animator>();
    }

    void IInteractable.Interact()
    {
        if (!_isDropped)
        {
            Spawn();
            _isDropped = true;
            animator.SetTrigger("_isOpen");
        }
    }

    void Spawn()
    {
        ItemScene.SpawnItemScene(transform.position, itemToDrop);
        _isDropped = true;
        Destroy(this);
    }

    public string GetActionName()
    {
        return "Open";
    }
}
