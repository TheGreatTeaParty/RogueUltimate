using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour,IInteractable
{
    private Item itemToDrop;
    private bool _isDropped = false;
    private Animator animator;
    private AudioSource OpenAudio;
    [SerializeField] private ParticleSystem openEffect;

    void Start()
    {
        itemToDrop = ItemsAsset.instance.GenerateItem();
        _isDropped = false;
        animator = GetComponent<Animator>();
        OpenAudio = GetComponent<AudioSource>();
    }

    void IInteractable.Interact()
    {
        if (!_isDropped)
        {
            _isDropped = true;
            animator.SetTrigger("_isOpen");
            OpenAudio.Play();
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
        yield return new WaitForSeconds(0.34f);
        openEffect.Play();
        yield return new WaitForSeconds(0.24f);
        Spawn();
    }
}
