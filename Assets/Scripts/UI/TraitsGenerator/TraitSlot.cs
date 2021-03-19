using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TraitSlot : MonoBehaviour
{
    public int TradeNum;
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _description;
    public TraitsGenerator traitsGenerator;

    private Image _traitIcon;

    // Start is called before the first frame update
    void Start()
    {
        _traitIcon = GetComponentInChildren<Image>();
        var temp = traitsGenerator.OutcomeTraits[TradeNum];
        _name.text = temp.Name;
        if(_description)
            _description.text = temp.Description;
        _traitIcon.sprite = temp.Icon;
    }
}
