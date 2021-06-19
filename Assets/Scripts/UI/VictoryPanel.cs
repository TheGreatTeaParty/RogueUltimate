using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class VictoryPanel : MonoBehaviour
{
    public TextMeshProUGUI TreasureValue;
    [Space]
    public int ValuePerTreasure;
    [Space]
    public Transform template;
    public GameObject TreasureParent;
    [Space]
    public Button button;

    private CharacterManager characterManager;
    private Tuple <Item, int>[] treasures;
    private int treasureTotal;
    private Animator animator;

    private bool isFinished = false;
    private int _displayIndex = 0;

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
    }

    private void DisplayTreasure(Tuple<Item, int> tuple)
    {
        if (tuple.Item2 >= 1)
        {
            SpawnTreasure(tuple.Item1);
            StartCoroutine(WaitAndCalculate(tuple.Item1, tuple.Item2));
        }
    }

    private void SpawnTreasure(Item item)
    {
       var CreatedItem = Instantiate(template, TreasureParent.transform);
       CreatedItem.GetComponentsInChildren<Image>()[1].sprite = item.Sprite;
    }

    private void CalculateTreasureValue(Item item,int count)
    {
        animator.SetTrigger("TreasureCount");
        treasureTotal += count * item.Price;
        TreasureValue.text = treasureTotal.ToString();
    }

    private IEnumerator WaitAndCalculate(Item item, int count)
    {
        yield return new WaitForSeconds(0.5f);
        CalculateTreasureValue(item, count);
        isFinished = true;
    }

    private void Update()
    {
        if (isFinished && _displayIndex < treasures.Length)
        {
            isFinished = false;
            DisplayTreasure(treasures[_displayIndex]);
            _displayIndex++;
        }
        else if(isFinished && _displayIndex >= treasures.Length)
        {
            button.interactable = true;
        }
    }

    public void StartToDisplayTreasures()
    {
        isFinished = true;
    }
    public void CloseResults()
    {
        AccountManager.Instance.Renown += treasureTotal;
        LevelManager.Instance.LoadScene("Tavern");
    }
}
