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
    [HideInInspector] public PlayerStat stats;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public PlayerAttack playerAttack;
    [HideInInspector] public PlayerFX playerFX;
    [HideInInspector] public EquipmentAnimationHandler equipmentAnimationHandler;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public InteractDetaction interactDetaction; 
    
    public Sprite PlayerSprite => spriteRenderer.sprite;

    public Sprite ArmorSprite
    {
        get
        {
            if (equipmentAnimationHandler == null) return null;

            return equipmentAnimationHandler.ArmorAnimationSprites?.Length > 0 
                ? equipmentAnimationHandler.ArmorAnimationSprites?[0] : null;
        }
    }


    private void Start()
    {
        // Cache
        stats = GetComponent<PlayerStat>();
        rb = Instance.GetComponent<Rigidbody2D>();
        playerAttack = Instance.GetComponent<PlayerAttack>();
        playerFX = Instance.GetComponent<PlayerFX>();
        equipmentAnimationHandler = Instance.GetComponentInChildren<EquipmentAnimationHandler>();
        playerMovement = Instance.GetComponent<PlayerMovement>();
        audioSource = Instance.GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactDetaction = Instance.GetComponentInChildren<InteractDetaction>();
        //Set stats to CharacterManager
        SetStats();
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
    public void SetStats()
    {
        CharacterManager.Instance.SetStats(stats);
    }
}
