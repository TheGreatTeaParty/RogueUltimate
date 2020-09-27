using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Lamp")]

public class Lamp : EquipmentItem
{
    [SerializeField] float TimeToBurn;
}
