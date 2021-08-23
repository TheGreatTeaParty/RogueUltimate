using UnityEngine;
using UnityEngine.UI;


public class PlayerImage : MonoBehaviour
{
    [SerializeField] private Image _playerImage;
    [SerializeField] private Image _armorImage;

    private Color _disabledColor = new Color(0, 0, 0, 0);
    private Color _normalColor = new Color(1, 1, 1, 1);


    private void Start()
    {
        if (PlayerOnScene.Instance.ArmorSprite != null)
        {
            _armorImage.sprite = PlayerOnScene.Instance.ArmorSprite;
            _armorImage.color = _normalColor;
        }
        else
            _armorImage.color = _disabledColor;
        
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
        
        if (newItem)
        {
            if (newItem.EquipmentType == EquipmentType.Armor)
                SwitchArmor(newItem);
        }
    }

    private void SwitchArmor(EquipmentItem newItem = null)
    {
        if (newItem)
        {
            _armorImage.sprite = PlayerOnScene.Instance.ArmorSprite;
            _armorImage.color = _normalColor;
        }
        else
        {
            _armorImage.sprite = null;
            _armorImage.color = _disabledColor;
        }

        _playerImage.color = _normalColor;
    }

}