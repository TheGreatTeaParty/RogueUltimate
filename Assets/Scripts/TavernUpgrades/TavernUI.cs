using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TavernUI : MonoBehaviour
{
    public TextMeshProUGUI CoinsValue;
    public TextMeshProUGUI RenownValue;

    private Inventory inventory;
    private Animator animator;
    private float _prevGold = -1;
    private float _prevRenown = -1;

    // Start is called before the first frame update
    void Start()
    {
        inventory = CharacterManager.Instance.Inventory;
        inventory.OnGoldChanged += UpdateGold;
        animator = GetComponent<Animator>();
        AccountManager.Instance.OnRenownChanged += UpdateRenown;
        UpdateOnStart();
    }

    private void UpdateGold(float gold)
    {
        if (_prevGold != gold)
        {
            if (animator != null) animator.SetTrigger("GoldChanged");
            _prevGold = gold;
        }
        CoinsValue.text = gold.ToString();
    }
    private void UpdateRenown(float renown)
    {
        if (_prevRenown != renown)
        {
            if (animator != null) animator.SetTrigger("RenownChanged");
            _prevRenown = renown;
        }
        RenownValue.text = renown.ToString();
    }
    private void UpdateOnStart()
    {
        CoinsValue.text = inventory.Gold.ToString();
        RenownValue.text = AccountManager.Instance.Renown.ToString();
    }
}
