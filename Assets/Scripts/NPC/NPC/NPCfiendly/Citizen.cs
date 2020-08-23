using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Citizen : MonoBehaviour, IInteractable
{
    [SerializeField]
    private DialogSystem dialogSystem;
    [SerializeField]
    private GameObject dialogWindow;
    [SerializeField]
    private GameObject buttonContinue;
    [SerializeField]
    private Dialog dialog;
    

    void IInteractable.Interact()
    {
        InterfaceOnScene.instance.gameObject.SetActive(false);
        dialogWindow.SetActive(true);
        buttonContinue.SetActive(true);
        dialogSystem.StartDialog(dialog);

    }

    string IInteractable.GetActionName() 
    {
        return "Talk";
    }
}   
