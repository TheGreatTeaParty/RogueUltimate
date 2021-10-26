using UnityEngine;

public class InteractDetaction : MonoBehaviour
{
    private InteractionUI interactionUI;
    private IInteractable interactable;
    private ITalkable talkable;
    private MaterialPropertyBlock _collideMaterial;
    private SpriteRenderer _colliderInfo;

    private void Start()
    {
        interactionUI = InterfaceManager.Instance.GetComponentInChildren<InteractionUI>();

        _collideMaterial = new MaterialPropertyBlock();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
        {
            _colliderInfo = collision.GetComponent<SpriteRenderer>();

            //Save colide material
            _colliderInfo.GetPropertyBlock(_collideMaterial);

            TurnOnOutline();

            //Save collission information to use it later in Call
            interactable = collision.GetComponent<IInteractable>();


            //Enable
            interactionUI.SetInteractActive(true);

            //Get Action ItemName and set it up 
            interactionUI.SetActionText(interactable.GetActionName());
            interactionUI.SetInteractDetaction(this);
        }

        if (collision.GetComponent<ITalkable>() != null)
        {
            _colliderInfo = collision.GetComponent<SpriteRenderer>();

            //Save colide material
            _colliderInfo.GetPropertyBlock(_collideMaterial);

            TurnOnOutline();

            //Save collission information to use it later in Call
            talkable = collision.GetComponent<ITalkable>();


            //Enable
            interactionUI.SetTalkActive(true);
            interactionUI.SetInteractDetaction(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
            DeleteInteractionData();
        if (collision.GetComponent<ITalkable>() != null)
            DeleteInteractionData();
    }

    public void DeleteInteractionData()
    {
        TurnOFFOutline();
        if (interactionUI)
        {
            interactionUI.SetInteractActive(false);
            interactionUI.SetTalkActive(false);
        }
        interactable = null;
        talkable = null;
    }

    public void CallInteraction()
    {
        interactable.Interact();
    }
    public void CallTalkAction()
    {
        talkable.Talk();
    }
    private void TurnOnOutline()
    {
        _collideMaterial.SetFloat("_Thickness", 0.007f);
        _colliderInfo.SetPropertyBlock(_collideMaterial);
    }

    private void TurnOFFOutline()
    {
        if (_colliderInfo)
        {
            _collideMaterial.SetFloat("_Thickness", 0.0f);
            _colliderInfo.SetPropertyBlock(_collideMaterial);
            _colliderInfo = null;
        }
    }

}
