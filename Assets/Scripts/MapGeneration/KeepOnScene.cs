using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnScene : MonoBehaviour
{
    public static KeepOnScene Instance;

    // Cache
    public PlayerAttack playerAttack;
    public PlayerFX playerFX;
    public EquipmentAnimationHandler equipmentAnimationHandler;
    public PlayerMovement playerMovement;
    public AudioSource audioSource;
    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject); 
        
        // Cache
        playerAttack = Instance.GetComponent<PlayerAttack>();
        playerFX = Instance.GetComponent<PlayerFX>();
        equipmentAnimationHandler = Instance.GetComponentInChildren<EquipmentAnimationHandler>();
        playerMovement = Instance.GetComponent<PlayerMovement>();
        audioSource = Instance.GetComponent<AudioSource>();
    }

}
