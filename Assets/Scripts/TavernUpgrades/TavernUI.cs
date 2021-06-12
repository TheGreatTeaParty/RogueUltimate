using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TavernUI : MonoBehaviour
{
    public TextMeshProUGUI CoinsValue;
    public TextMeshProUGUI RenownValue;
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = CharacterManager.Instance.Inventory;
        inventory.OnGoldChanged += UpdateGold;
        AccountManager.Instance.OnRenownChanged += UpdateRenown;
        UpdateOnStart();
    }

    private void UpdateGold(float gold)
    {
        CoinsValue.text = gold.ToString();
    }
    private void UpdateRenown(float renown)
    {
        RenownValue.text = renown.ToString();
    }
    private void UpdateOnStart()
    {
        CoinsValue.text = inventory.Gold.ToString();
        RenownValue.text = AccountManager.Instance.Renown.ToString();
    }
}
