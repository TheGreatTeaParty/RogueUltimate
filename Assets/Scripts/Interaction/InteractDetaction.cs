using UnityEngine;

public class InteractDetaction : MonoBehaviour
{
    private InteractionUI interactionUI;
    private IInteractable interactable;
    private MaterialPropertyBlock _collideMaterial;
    private SpriteRenderer _colliderInfo;

    
    private void Start()
    {
        interactionUI = InterfaceManager.Instance.GetComponentInChildren<InteractionUI>();
        _collideMaterial = new MaterialPropertyBlock();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IInteractable>() != null)
        {
            _colliderInfo = collision.GetComponent<SpriteRenderer>();

            //Save colide material
            _colliderInfo.GetPropertyBlock(_collideMaterial);


            //Save collission information to use it later in Call
            interactable = collision.GetComponent<IInteractable>();

            //turn on the outline
            TurnOnOutline();

            //Enable
            interactionUI.SetActive(true);

            //Get Action ItemName and set it up 
            interactionUI.SetText(interactable.GetActionName());

            interactionUI.SetInteractDetaction(this);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
        {
            DeleteInteractionData();
        }
    }

    public void DeleteInteractionData()
    {
        TurnOFFOutline();
        interactionUI.SetActive(false);
        interactable = null;
    }

    public void CallInteraction()
    {
        interactable.Interact();
    }

    private void TurnOnOutline() 
    {
        _collideMaterial.SetFloat("_Thickness", 0.01f);
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
