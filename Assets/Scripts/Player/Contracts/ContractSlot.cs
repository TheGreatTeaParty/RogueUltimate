using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContractSlot : TradeSlot
{
    public GameObject Child;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Value;

    [Space]
    public Contract.contractType type;

    public override Item Item {

        get => item;
        set
        {
            
            item = value;
            if (item == null || (item as Contract).type != type)
            {
                item = null;
                Description.text = "";
                Value.text = "";
                Child.SetActive(false);
            }
            else
            {
                Child.SetActive(true);
                Description.text = (item as Contract).Description;
                Value.text = (item as Contract).Price.ToString();
            }

        }
    }
}
