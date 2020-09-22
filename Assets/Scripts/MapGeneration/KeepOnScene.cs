using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnScene : MonoBehaviour
{
    public static KeepOnScene Instance;

    // Cache
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public PlayerAttack playerAttack;
    [HideInInspector] public PlayerFX playerFX;
    [HideInInspector] public EquipmentAnimationHandler equipmentAnimationHandler;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public InteractDetaction interactDetaction;


    private void Awake()
    {
        if (Instance == null)
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
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactDetaction = Instance.GetComponentInChildren<InteractDetaction>();
    }

    public void HidePlayer()
    {
        spriteRenderer.enabled = false;
        // armor here
    }

    public void ShowPlayer()
    {
        spriteRenderer.enabled = true;
        // armor here
    }

}
