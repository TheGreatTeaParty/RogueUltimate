using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TraitSlotUI : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI Name;
    [SerializeField]
    private TextMeshProUGUI Description;

    public void SetTraitProperties(Sprite _icon, string name, string description)
    {
        icon.sprite = _icon;
        Name.SetText(name);
        Description.SetText(description);
    }
}
