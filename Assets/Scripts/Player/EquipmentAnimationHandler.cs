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
    }

    private void Update()
    {
        if (_weaponController is null) return;

        _direction = _playerMovement.GetDirection();

        weaponAnimator.SetFloat("Horizontal", _direction.x);
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

        if (_weaponEquiped && !weaponAnimator.GetBool("Attack"))
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
                return;

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
                        _weaponRenderer.sortingOrder = _playerRenderer.sortingOrder + 2;

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

    public void RotateRangeWeapon(Vector3 dir)
    {
        weaponAnimator.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
    }
}
