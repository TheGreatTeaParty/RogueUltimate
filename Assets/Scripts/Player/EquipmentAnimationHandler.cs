using UnityEngine;

public class EquipmentAnimationHandler : MonoBehaviour
{
    public Animator weaponAnimator;

    private Sprite[] _armorAnimationSprites;
    private SpriteRenderer _armorRenderer;
    private SpriteRenderer _weaponRenderer;

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
        
        CharacterManager.Instance.onEquipmentChanged += OnWeaponChanged;
        CharacterManager.Instance.onEquipmentChanged += OnEquipmentChanged;
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
    }

    //When the equipment changed, change the Animation controller
    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new)
        {
            if (_new.EquipmentType == EquipmentType.Weapon)
            {
                _weaponEquiped = true;
                _weaponRenderer.sprite = _new.Sprite;
            }
        }
        //If we drop the weapon, clear the animation controller
        else if(_old.EquipmentType == EquipmentType.Weapon && _new == null)
        {
            weaponAnimator.gameObject.transform.rotation = Quaternion.identity;
            _weaponEquiped = false;
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

    public void RotateWeapon(Vector3 dir)
    {
        if (_weaponEquiped)
        {
            if (dir.x < 0)
            {
                weaponAnimator.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                weaponAnimator.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
                weaponAnimator.gameObject.transform.Rotate(0, 0, -90);
            }
            else
            {
                weaponAnimator.gameObject.transform.localScale = new Vector3(1, 1, 1);
                weaponAnimator.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
            }
            weaponAnimator.gameObject.transform.Rotate(0,0,-35);
           
        }
    }
}
