using UnityEngine;
using UnityEngine.UI;


public class PlayerImage : MonoBehaviour
{
    [SerializeField] private Image _playerImage;
    [SerializeField] private Image _armorImage;
    [SerializeField] private Image _weaponImage;

    private PlayerOnScene _playerOnScene;

    private Color _disabledColor = new Color(0, 0, 0, 0);
    private Color _normalColor = new Color(1, 1, 1, 1);


    private void Start()
    {
        _playerOnScene = PlayerOnScene.Instance;
        if (_playerOnScene.ArmorSprite != null)
        {
            _armorImage.sprite = _playerOnScene.ArmorSprite;
            _armorImage.color = _normalColor;
        }
        else
            _armorImage.color = _disabledColor;

        EquipmentItem weapon = CharacterManager.Instance.Equipment.GetWeaponItem();
        if (weapon)
        {
            _weaponImage.sprite = weapon.Sprite;
            _weaponImage.color = _normalColor;
        }    
        
        CharacterManager.Instance.onEquipmentChanged += OnEquipmentChanged;
        Invoke("LoadPlayerPicture", 0.5f);
    }
    private void LoadPlayerPicture()
    {
        _playerImage.sprite = CharacterManager.Instance.Skin.GetCurrentSkin();
        _playerImage.color = _normalColor;
    }
    private void OnEquipmentChanged(EquipmentItem newItem, EquipmentItem oldItem)
    {
        if (oldItem)
            if (oldItem.EquipmentType == EquipmentType.Armor)
                SwitchArmor();
            else if (oldItem.EquipmentType == EquipmentType.Weapon)
                SwitchWeapon();
        if (newItem)
        {
            if (newItem.EquipmentType == EquipmentType.Armor)
                SwitchArmor(newItem);
            else if (newItem.EquipmentType == EquipmentType.Weapon)
                SwitchWeapon(newItem);
        }
    }

    private void SwitchArmor(EquipmentItem newItem = null)
    {
        if (newItem)
        {
            _armorImage.sprite = _playerOnScene.ArmorSprite;
            _armorImage.color = _normalColor;
        }
        else
        {
            _armorImage.sprite = null;
            _armorImage.color = _disabledColor;
        }

        _playerImage.color = _normalColor;
    }
    private void SwitchWeapon(EquipmentItem newItem = null)
    {
        if (newItem)
        {
            _weaponImage.sprite = newItem.Sprite;
            _weaponImage.color = _normalColor;
        }
        else
        {
            _weaponImage.sprite = null;
            _weaponImage.color = _disabledColor;
        }

        _playerImage.color = _normalColor;
    }

}