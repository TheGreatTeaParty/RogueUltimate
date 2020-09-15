using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractDetaction : MonoBehaviour
{
    private InteractionUI interactionUI;
    private IInteractable interactable;

    
    private void Start()
    {
        interactionUI = InterfaceOnScene.Instance.GetComponentInChildren<InteractionUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IInteractable>() != null)
        {
            //Save collission information to use it later in Call
            interactable = collision.GetComponent<IInteractable>();

            //Enable
            interactionUI.SetActive(true);

            //Get Action Name and set it up 
            interactionUI.SetText(interactable.GetActionName());

            interactionUI.SetInteractDetaction(this);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
        {
            //Disable
            interactionUI.SetActive(false);
            interactable = null;
        }
    }
    
    public void CallInteraction()
    {
        interactable.Interact();
    }
    
}
