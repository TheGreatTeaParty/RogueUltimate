using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickHandle : MonoBehaviour
{
    [SerializeField] GameObject[] joysticks; //1 - move 2- shoot button 3- shoot joystick


    private void Start()
    {
        EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
    }
    private void OnEquipmentChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if(_new == null)
        {
            joysticks[1].gameObject.SetActive(true);
            joysticks[2].gameObject.SetActive(false);
        }

        else
        {
            if(_new.Echo() != WeaponType.mele && _new.Echo() != WeaponType.nothing)
            {
                joysticks[1].gameObject.SetActive(false);
                joysticks[2].gameObject.SetActive(true);
            }

            else
            {
                joysticks[1].gameObject.SetActive(true);
                joysticks[2].gameObject.SetActive(false);
            }
        }
    }
}
