using UnityEngine;

public class EquipmentAnimationHandler : MonoBehaviour
{
    public Animator weaponAnimator;

    private Sprite[] _armorAnimationSprites;
    private Sprite[] _weaponAnimationSprites;
    private SpriteRenderer _armorRenderer;
    private SpriteRenderer _weaponRenderer;


    private RuntimeAnimatorController _weaponController;
    private Vector2 _direction;
    private PlayerMovement _playerMovement;
    private bool _armorEquiped = false;
    private bool _weaponEquiped = false;
    private SpriteRenderer _playerRenderer;

    public Sprite[] ArmorAnimationSprites => _armorAnimationSprites;


    private void Start()
    {
        SpriteRenderer[] array = GetComponentsInChildren<SpriteRenderer>();
        _weaponRenderer = array[0];
        _armorRenderer = array[1];

        _playerRenderer = PlayerOnScene.Instance.GetComponent<SpriteRenderer>();
        _playerMovement = PlayerOnScene.Instance.GetComponent<PlayerMovement>();
        
        CharacterManager.Instance.onEquipmentChanged += OnWeaponChanged;
        CharacterManager.Instance.onEquipmentChanged += OnEquipmentChanged;
        
        PlayerOnScene.Instance.playerAttack.onAttacked += AttackAnimation;
    }
    
    private void Update()
    {
        if (_weaponController is null) return;
     
        _direction = _playerMovement.GetDirection();
        int x = 0;
        int y = 0;

        if (_direction.x > 0.5f)
        {
            x = 1;
            y = 0;
            weaponAnimator.SetFloat("Horizontal", x);
            weaponAnimator.SetFloat("Vertical", y);
        }
        else if (_direction.x < -0.5f)
        {
            x = -1;
            y = 0;
            weaponAnimator.SetFloat("Horizontal", x);
            weaponAnimator.SetFloat("Vertical", y);
        }
        else
        {
            weaponAnimator.SetFloat("Horizontal", _direction.x);
            weaponAnimator.SetFloat("Vertical", _direction.y);
        }
    }

    // Take last digits in player sprite and put armor sprites with the same index
    private void LateUpdate()
    {
        if (_armorEquiped)
        {
            string index = "";
            if (_playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 2] != '_')
            {
                if (_playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 3] != '_')
                    index += _playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 3];
                index += _playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 2];
            }
            index += _playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 1];

            if (ArmorAnimationSprites.Length == 0)
                Debug.LogWarning("Missing Animation Sprites");
            
            else
            {
                if (int.TryParse(index, out int j))
                {
                    if (j >= ArmorAnimationSprites.Length)
                    {
                        Debug.LogWarning($"Sprite with Index: {j} does not exist in Animation Sprites!");
                        _armorRenderer.sprite = null;
                    }
                    else
                    {
                        _armorRenderer.sprite = ArmorAnimationSprites[j];
                      
                        //Move it on the top of the player Sprite
                        _armorRenderer.sortingOrder = _playerRenderer.sortingOrder + 1;
                    }
                }
            }
            
        }

        if (_weaponEquiped && weaponAnimator.GetInteger("Set") > 0)
        {
            string index = "";
            if (_playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 2] != '_')
            {
                if (_playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 3] != '_')
                    index += _playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 3];
                index += _playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 2];
            }
            index += _playerRenderer.sprite.name[_playerRenderer.sprite.name.Length - 1];

            if (_weaponAnimationSprites.Length == 0)
                Debug.LogWarning("Missing Animation Sprites");

            else
            {
                if (int.TryParse(index, out int j))
                {
                    if (j >= _weaponAnimationSprites.Length)
                    {
                        return;
                    }
                    else
                    {
                        _weaponRenderer.sprite = _weaponAnimationSprites[j];

                        //Move it on the top of the player Sprite
                        if (8 <=j && j <= 11 || 24 <= j && j <= 27 || 40 <= j && j <= 43 || 56 <= j && j <= 59
                            || 72 <= j && j <= 75)
                        {
                            _weaponRenderer.sortingOrder = _playerRenderer.sortingOrder + 2;
                        }
                        else
                        {
                            _weaponRenderer.sortingOrder = _playerRenderer.sortingOrder - 2;
                        }
                    }
                }
            }
        }

    }

    //When the equipment changed, change the Animation controller
    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new)
        {
            if (_new.EquipmentType == EquipmentType.Weapon)
            {
                _weaponController = _new.EquipmentAnimations;
                weaponAnimator.runtimeAnimatorController = _weaponController as RuntimeAnimatorController;

                _weaponEquiped = true;
                _weaponAnimationSprites = _new.Animation;
            }
        }
        //If we drop the weapon, clear the animation controller
        else if(_old.EquipmentType == EquipmentType.Weapon && _new == null)
        {
            weaponAnimator.gameObject.transform.rotation = Quaternion.identity;
            weaponAnimator.runtimeAnimatorController = null as RuntimeAnimatorController;
            _weaponController = null;

            _weaponEquiped = false;
            _weaponAnimationSprites = null;
            _weaponRenderer.sprite = null;
        }
    }

    private void OnEquipmentChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new)
        {
            if (_new.EquipmentType == EquipmentType.Armor)
            {
                _armorEquiped = true;
                _armorAnimationSprites = _new.Animation;
            }
        }

        else if (_old.EquipmentType == EquipmentType.Armor && _new == null)
        {
            _armorEquiped = false;
            _armorRenderer.sprite = null;
            _armorAnimationSprites = null;
        }
    }

    //When attack, trigger the isAttack animation
    private void AttackAnimation(AttackType type,int set)
    {
        if (_weaponController != null)
        {
            weaponAnimator.SetTrigger("Attack");
            weaponAnimator.SetInteger("Set", set);
        }
        
    }
    public void RotateRangeWeapon(Vector3 dir)
    {
        weaponAnimator.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
    }
    
}
