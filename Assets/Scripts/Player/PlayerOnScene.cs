using System;
using System.Collections;
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
        
        Initialize();
    }
    
    #endregion

    
    // Cache
    [HideInInspector] public PlayerStat stats;
    [HideInInspector] public PlayerSkin skin;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public PlayerAttack playerAttack;
    [HideInInspector] public PlayerFX playerFX;
    [HideInInspector] public EquipmentAnimationHandler equipmentAnimationHandler;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public AudioSource dialogueAudioSource;
    [HideInInspector] public InteractDetaction interactDetaction; 
    
    public Sprite PlayerSprite => spriteRenderer.sprite;

    public Sprite ArmorSprite
    {
        get
        {
            if (equipmentAnimationHandler == null) return null;

            return equipmentAnimationHandler.ArmorAnimationSprites?.Length > 0 
                ? equipmentAnimationHandler.ArmorAnimationSprites?[4] : null;
        }
    }
    

    private void Initialize()
    {
        // Cache
        stats = GetComponent<PlayerStat>();
        skin = GetComponent<PlayerSkin>();
        rb = Instance.GetComponent<Rigidbody2D>();
        playerAttack = Instance.GetComponent<PlayerAttack>();
        playerFX = Instance.GetComponent<PlayerFX>();
        equipmentAnimationHandler = Instance.GetComponentInChildren<EquipmentAnimationHandler>();
        playerMovement = Instance.GetComponent<PlayerMovement>();
        
        var audioSources = GetComponents<AudioSource>();
        audioSource = audioSources[0];
        dialogueAudioSource = audioSources[1];
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactDetaction = Instance.GetComponentInChildren<InteractDetaction>();

    }

    private void Start()
    {
        SetStats();
        SetSkin();
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
    
    /// <summary>
    /// Set stats to CharacterManager
    /// </summary>
    public void SetStats()
    {
        CharacterManager.Instance.SetStats(stats);
    }
    public void SetSkin()
    {
        CharacterManager.Instance.SetSkin(skin);
    }
}
