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
            _isDropped = true;
            animator.SetTrigger("_isOpen");
            StartCoroutine(ChestAnimationWait());
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

    IEnumerator ChestAnimationWait()
    {
        yield return new WaitForSeconds(0.58f);
        Spawn();
    }
}
