using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WeaponDisplayIcon : MonoBehaviour
{
    [FormerlySerializedAs("BaseIcon")] 
    [SerializeField] private Sprite baseIcon;
    [FormerlySerializedAs("Icon")] 
    [SerializeField] private Image icon;
    
    // Cache
    private Equipment _equipment;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Make delegate function from CharacterManager be equal to UpdateUI fun
        CharacterManager.Instance.onEquipmentChanged += UpdateIcon;
        _equipment = CharacterManager.Instance.Equipment;
        UpdateIconOnStart();
    }

    // Update is called once per frame
    void UpdateIcon(EquipmentItem newItem, EquipmentItem oldItem)
    {
        if(newItem!= null && newItem.EquipmentType == EquipmentType.Weapon)
        {
            icon.sprite = newItem.Sprite;
        }
        else if(newItem == null)
        {
            icon.sprite = baseIcon;
        }
    }

    void UpdateIconOnStart()
    {
        EquipmentItem equipmentItem = _equipment.equipmentSlots[(int)EquipmentType.Weapon].Item as EquipmentItem;
        icon.sprite = equipmentItem != null ? equipmentItem.Sprite : baseIcon;
    }
    
}
