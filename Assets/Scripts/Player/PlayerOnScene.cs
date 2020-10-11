using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnScene : MonoBehaviour
{
    #region Singleton
    
    public static PlayerOnScene Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
    
    #endregion

    
    // Cache
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public PlayerAttack playerAttack;
    [HideInInspector] public PlayerFX playerFX;
    [HideInInspector] public EquipmentAnimationHandler equipmentAnimationHandler;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public InteractDetaction interactDetaction; 
    
    public Sprite PlayerSprite => spriteRenderer.sprite;
    public Sprite ArmorSprite 
        => equipmentAnimationHandler.AnimationSprites.Length > 0 ? equipmentAnimationHandler.AnimationSprites?[0] : null;


    private void Start()
    {
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
        equipmentAnimationHandler.gameObject.SetActive(false);
        spriteRenderer.enabled = false;
    }

    public void ShowPlayer()
    {
        spriteRenderer.enabled = true;
        equipmentAnimationHandler.gameObject.SetActive(true);
    }

}
