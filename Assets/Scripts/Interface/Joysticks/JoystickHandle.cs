using UnityEngine;

public class JoystickHandle : MonoBehaviour
{
    [SerializeField] GameObject[] joysticks; //1 - move 2- shoot button 3- shoot joystick


    private void Start()
    {
        CharacterManager.Instance.onEquipmentChanged += OnEquipmentChanged;
    }
    
    
    private void OnEquipmentChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new) 
        {
            if (_new.EquipmentType == EquipmentType.Weapon)
            {
                if (_new.Echo() != AttackType.Melee && _new.Echo() != AttackType.None)
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
        else if (!_new && _old.EquipmentType == EquipmentType.Weapon)
        {
            joysticks[1].gameObject.SetActive(true);
            joysticks[2].gameObject.SetActive(false);
        }
        
    }
    
    
}
