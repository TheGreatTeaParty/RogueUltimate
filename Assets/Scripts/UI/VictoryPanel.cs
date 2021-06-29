using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class VictoryPanel : MonoBehaviour
{
    public TextMeshProUGUI TreasureValue;
    public TextMeshProUGUI ContractValue;
    [Space]
    public int ValuePerTreasure;
    [Space]
    public Transform template;
    public GameObject TreasureParent;
    public GameObject ContractParent;
    [Space]
    public Button button;

    private CharacterManager characterManager;
    private Tuple <Item, int>[] treasures;
    private List<Item> contracts;
    private int treasureTotal;
    private int contractTotal;
    private Animator animator;

    private bool isFinished = false;
    private bool isContractFinished = false;
    private int _displayIndex = 0;
    private int TOTAL = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterManager = CharacterManager.Instance;
        treasures = new Tuple<Item, int>[5];
        button.interactable = false;

        for (int i = 0; i < treasures.Length; ++i)
        {
            treasures[i] = Tuple.Create<Item,int>(null, 0);
        }

        CountTreasures();
        CountContracts();
    }

    private void CountTreasures()
    {
       for( int i = 0; i < characterManager.Inventory.Items.Count; ++i)
        {
            EquipmentItem equipmentItem = characterManager.Inventory.Items[i] as EquipmentItem;
            UsableItem usableItem = characterManager.Inventory.Items[i] as UsableItem;

            if (equipmentItem == null && usableItem == null)
            {
                for (int j = 0; j < treasures.Length; j++)
                {
                    if (treasures[j].Item1 == null)
                    {
                        treasures[j] = Tuple.Create(characterManager.Inventory.Items[i], 1);
                        TOTAL++;
                        break;
                    }
                    else if (treasures[j].Item1 == characterManager.Inventory.Items[i])
                    {
                        treasures[j] = Tuple.Create(characterManager.Inventory.Items[i], treasures[j].Item2 + 1);
                        break;
                    }
                }
            }
        }

        foreach (var item in treasures)
        {
            if(item.Item1 != null)
                characterManager.Inventory.RemoveItemCompletly(item.Item1);
        }
    }

    private void CountContracts()
    {
        contracts = new List<Item>();
        for(int i = 0; i < characterManager.Stats.PlayerContracts.contracts.Count; ++i)
        {
            if (characterManager.Stats.PlayerContracts.contracts[i]._currentScore >= characterManager.Stats.PlayerContracts.contracts[i].AimScore)
            {
                contracts.Add(characterManager.Stats.PlayerContracts.contracts[i]);
                characterManager.Stats.PlayerContracts.Remove(characterManager.Stats.PlayerContracts.contracts[i]);
            }
        }
    }

    private void DisplayTreasure(Tuple<Item, int> tuple)
    {
        if (tuple.Item2 >= 1)
        {
            SpawnTreasure(tuple.Item1, tuple.Item2);
            StartCoroutine(WaitAndCalculate(tuple.Item1, tuple.Item2));
        }
    }
    private void DisplayContract(Item item)
    {
        SpawnContract(item);
        StartCoroutine(WaitAndCalculate(item));
    }

    private void SpawnTreasure(Item item, int count)
    {
       var CreatedItem = Instantiate(template, TreasureParent.transform);
       CreatedItem.GetComponentsInChildren<Image>()[1].sprite = item.Sprite;
       CreatedItem.GetComponentInChildren<TextMeshProUGUI>().text = "x" + count.ToString();
    }
    private void SpawnContract(Item item) {
        var CreatedItem = Instantiate(template, ContractParent.transform);
        CreatedItem.GetComponentsInChildren<Image>()[1].sprite = item.Sprite;
    }

    private void CalculateTreasureValue(Item item,int count)
    {
        animator.SetTrigger("TreasureCount");
        treasureTotal += count * item.Price;
        TreasureValue.text = treasureTotal.ToString();
    }

    private void CalculateContractValue(Item item)
    {
        animator.SetTrigger("KillCount");
        contractTotal += item.Price;
        ContractValue.text = contractTotal.ToString();
    }

    private IEnumerator WaitAndCalculate(Item item, int count)
    {
        yield return new WaitForSeconds(0.5f);
        CalculateTreasureValue(item, count);
        isFinished = true;
    }
    private IEnumerator WaitAndCalculate(Item item)
    {
        yield return new WaitForSeconds(0.5f);
        CalculateContractValue(item);
        isContractFinished = true;
    }

    private void Update()
    {
        if(isContractFinished && _displayIndex < contracts.Count)
        {
            isContractFinished = false;
            DisplayContract(contracts[_displayIndex]);
            _displayIndex++;
        }
        else if(isContractFinished && _displayIndex >= contracts.Count)
        {
            _displayIndex = 0;
            isFinished = true;
            isContractFinished = false;
        }

        if (isFinished && _displayIndex < TOTAL)
        {
            isFinished = false;
            DisplayTreasure(treasures[_displayIndex]);
            _displayIndex++;
        }
        else if(isFinished && _displayIndex >= TOTAL)
            button.interactable = true;
        
    }

    public void StartToDisplayTreasures()
    {
        isContractFinished = true;
    }

    public void CloseResults()
    {
        AccountManager.Instance.Renown += treasureTotal;
        LevelManager.Instance.LoadScene("Tavern");
    }
}
