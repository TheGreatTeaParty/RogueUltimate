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
    }

    // Update is called once per frame
    void UpdateIcon(EquipmentItem newItem, EquipmentItem oldItem)
    {
        if(newItem!= null && newItem.equipmentType == EquipmentType.Weapon)
        {
            Icon.sprite = newItem.Sprite;
        }
        else
        {
            Icon.sprite = BaseIcon;
        }
    }
}
