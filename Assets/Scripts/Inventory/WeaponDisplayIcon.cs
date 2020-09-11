using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplayIcon : MonoBehaviour
{
    [SerializeField]
    private Sprite BaseIcon;
    [SerializeField]
    private Image Icon;

    private EquipmentManager equipmentManager;

    // Start is called before the first frame update
    void Start()
    {
        equipmentManager = EquipmentManager.Instance;
        //Make delegate function from InventoryManager be equal to UpdateUI fun
        equipmentManager.onEquipmentChanged += UpdateIcon;
        UpdateIconOnStart();

    }

    // Update is called once per frame
    void UpdateIcon(EquipmentItem newItem, EquipmentItem oldItem)
    {
        if(newItem!= null && newItem.equipmentType == EquipmentType.Weapon)
        {
            Icon.sprite = newItem.Sprite;
        }
        else if(newItem == null)
        {
            Icon.sprite = BaseIcon;
        }
    }

    void UpdateIconOnStart()
    {
        if (equipmentManager.currentEquipment[(int)EquipmentType.Weapon] != null)
        {
            Icon.sprite = equipmentManager.currentEquipment[(int)EquipmentType.Weapon].Sprite;
        }
        else
        {
            Icon.sprite = BaseIcon;
        }
    }
}
