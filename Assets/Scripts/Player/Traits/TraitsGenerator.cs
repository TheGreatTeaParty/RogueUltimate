using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TraitsGenerator : MonoBehaviour
{
    public enum RenownLevel
    {
        Scum = 1,
        MurderHobo,
        BattleSeasoned,
        Adventurer,
        Hero,
    };

    public Transform Player;
    public GameObject Interface;
    [Space]
    [SerializeField]
    private RenownLevel Level;
    
    public List<Trait> P_Traits_6;
    public List<Trait> P_Traits_4;
    public List<Trait> P_Traits_2;
    [Space]
    public List<Trait> N_Traits_6;
    public List<Trait> N_Traits_4;
    public List<Trait> N_Traits_2;
    [Space]
    public List<Trait> Skip;
    [Space]
    public List<Trait> OutcomeTraits;
    [Space]
    public PointsManager pointsManager;

    private PlayerStat characterStat;
    private int _PlayerStartMoney;

    [SerializeField]
    private NamesGenerator namesGenerator;
    private string _playerName;
    [SerializeField]
    private TextMeshProUGUI _currentName;

    private void Start()
    {
        Level = (RenownLevel)AccountManager.Instance.MasterLevel;
        GenerateTrait();
        _playerName = namesGenerator.GetName();
        _currentName.text = _playerName;
    }

    private void SpawnPlayer()
    {
        _PlayerStartMoney = GenerateStartGold();
        var player = Instantiate(Player, transform.position, Quaternion.identity);
        characterStat = player.GetComponent<PlayerStat>();
        Invoke("GiveMoney", 0.1f);
    }
    private void GiveMoney()
    {
        var inventory = CharacterManager.Instance.Inventory;
        inventory.Gold = _PlayerStartMoney;
        inventory.UpdateGold();
    }
    public void CloseMenu()
    {
        SpawnPlayer();
        Interface.SetActive(true);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        characterStat.SetName(_playerName);
        characterStat.PlayerTraits.AddTrait(OutcomeTraits[0]);
        characterStat.PlayerTraits.AddTrait(OutcomeTraits[1]);
        characterStat.PlayerTraits.AddTrait(OutcomeTraits[2]);
        pointsManager.SaveChanges();
        var player = CharacterManager.Instance.Stats;
        player.SetUpPlayerInfo();
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    public void GenerateTrait()
    {
        switch (Level)
        {
            case RenownLevel.Scum: // -6 ; -4 ; +2;
                {
                    MakeTradeList(N_Traits_6, N_Traits_4, P_Traits_2);
                    break;
                }

            case RenownLevel.MurderHobo: // -6 ; -4/+4 ; +2;
                {
                    int number = Random.Range(0,2);
                    if(number == 1)
                        MakeTradeList(N_Traits_6, N_Traits_4, P_Traits_2);
                    else
                        MakeTradeList(N_Traits_6, P_Traits_4, P_Traits_2);
                    break;
                }

            case RenownLevel.BattleSeasoned: // -6/+6 ; -4/+4 ; +2/+2;
                {
                    int number = Random.Range(0, 2);

                    if (number == 1)    //+6
                    {
                        number = Random.Range(0, 2);
                        if(number == 0)     // -4
                        {
                            number = Random.Range(0, 2); //+2
                            if(number == 0)
                            {
                                MakeTradeList(P_Traits_6, N_Traits_4, P_Traits_2);
                            }
                            else                          //-2
                            {
                                MakeTradeList(P_Traits_6, N_Traits_4, N_Traits_2);
                            }
                        }
                        else                // +4
                        {
                            number = Random.Range(0, 2); //+2
                            if (number == 0)
                            {
                                MakeTradeList(P_Traits_6, P_Traits_4, P_Traits_2);
                            }
                            else                          //-2
                            {
                                MakeTradeList(P_Traits_6, P_Traits_4, N_Traits_2);
                            }
                        }
                    }

                    else                //-6
                    {
                        number = Random.Range(0, 2);
                        if (number == 0)     // -4
                        {
                            number = Random.Range(0, 2); //+2
                            if (number == 0)
                            {
                                MakeTradeList(N_Traits_6, N_Traits_4, P_Traits_2);
                            }
                            else                          //-2
                            {
                                MakeTradeList(N_Traits_6, N_Traits_4, N_Traits_2);
                            }
                        }
                        else                // +4
                        {
                            number = Random.Range(0, 2); //+2
                            if (number == 0)
                            {
                                MakeTradeList(N_Traits_6, P_Traits_4, P_Traits_2);
                            }
                            else                          //-2
                            {
                                MakeTradeList(N_Traits_6, P_Traits_4, N_Traits_2);
                            }
                        }
                    }
                    break;
                }

            case RenownLevel.Adventurer: // +6/-6 ; +4 ; -2;
                {
                    int number = Random.Range(0, 2);
                    if (number == 1)
                        MakeTradeList(P_Traits_6, P_Traits_4, N_Traits_2);
                    else
                        MakeTradeList(N_Traits_6, P_Traits_4, N_Traits_2);
                    break;
                }

            case RenownLevel.Hero: //   +6 ; +4/-4 ; +2;
                {
                    int number = Random.Range(0, 2);
                    if (number == 1)
                        MakeTradeList(P_Traits_6, N_Traits_4, P_Traits_2);
                    else
                        MakeTradeList(P_Traits_6, P_Traits_4, P_Traits_2);
                    break;
                }
        }
    }

    private int GenerateStartGold()
    {
        switch (Level)
        {
            case RenownLevel.Scum: // -6 ; -4 ; +2;
                {
                    return 100;
                }

            case RenownLevel.MurderHobo: // -6 ; -4/+4 ; +2;
                {
                    return 240;
                }

            case RenownLevel.BattleSeasoned: // -6/+6 ; -4/+4 ; +2/+2;
                {
                    return 400;
                }

            case RenownLevel.Adventurer: // +6/-6 ; +4 ; -2;
                {
                    return 600;
                }

            case RenownLevel.Hero: //   +6 ; +4/-4 ; +2;
                {
                    return 850;
                }
            default:
                return 100;
        }
    }

    private bool IsUsed(Trait trait) 
    {
        for(int  i = 0; i < OutcomeTraits.Count; ++i)
        {
            if (OutcomeTraits[i].Type == trait.Type)
                return false;
        }
        return true;
    }

    private void MakeTradeList(List<Trait> list1, List<Trait> list2, List<Trait> list3)
    {
        int number = Random.Range(0, 3);
        if (number == 0)
        {
            GenerateFirstTrait(list1);

            number = Random.Range(0, 2);
            if (number == 0)
            {
                Trait current_trait = list2[Random.Range(0, list2.Count)];
                while (!IsUsed(current_trait))
                {
                    current_trait = list2[Random.Range(0, list2.Count)];
                }
                OutcomeTraits.Add(current_trait);

                GenerateLastTrait(list3);
            }
            else
            {
                Trait current_trait = list3[Random.Range(0, list3.Count)];
                while (!IsUsed(current_trait))
                {
                    current_trait = list3[Random.Range(0, list3.Count)];
                }
                OutcomeTraits.Add(current_trait);
                GenerateLastTrait(list2);
            }
        }
        else if(number == 1)
        {
            if (number == 0)
            {
                GenerateFirstTrait(list2);

                number = Random.Range(0, 2);
                if (number == 0)
                {
                    Trait current_trait = list1[Random.Range(0, list1.Count)];
                    while (!IsUsed(current_trait))
                    {
                        current_trait = list1[Random.Range(0, list1.Count)];
                    }
                    OutcomeTraits.Add(current_trait);

                    GenerateLastTrait(list3);
                }
                else
                {
                    Trait current_trait = list3[Random.Range(0, list3.Count)];
                    while (!IsUsed(current_trait))
                    {
                        current_trait = list3[Random.Range(0, list3.Count)];
                    }
                    OutcomeTraits.Add(current_trait);
                    GenerateLastTrait(list1);
                }
            }
        }
        else
        {
            GenerateFirstTrait(list3);

            number = Random.Range(0, 2);
            if (number == 0)
            {
                Trait current_trait = list2[Random.Range(0, list2.Count)];
                while (!IsUsed(current_trait))
                {
                    current_trait = list2[Random.Range(0, list2.Count)];
                }
                OutcomeTraits.Add(current_trait);

                GenerateLastTrait(list1);
            }
            else
            {
                Trait current_trait = list1[Random.Range(0, list1.Count)];
                while (!IsUsed(current_trait))
                {
                    current_trait = list1[Random.Range(0, list1.Count)];
                }
                OutcomeTraits.Add(current_trait);
                GenerateLastTrait(list2);
            }
        }

        if (OutcomeTraits.Count == 0)
        {
            GenerateTrait();
        }
    }

    private void GenerateLastTrait(List<Trait> list)
    {
        Trait current_trait = list[Random.Range(0, list.Count)];
        int Max = 10;
        while (!IsUsed(current_trait))
        {
            current_trait = list[Random.Range(0, list.Count)];
            Max--;
        }
        if (Max == 0)
            Debug.Log("Could not find appropriate Trait");
        OutcomeTraits.Add(current_trait);

    }

    private void GenerateFirstTrait(List<Trait> list)
    {
        Trait current_trait = list[Random.Range(0, list.Count)];
        int Max = 10;
        while (!IsUsed(current_trait)&& Max > 0)
        {
            current_trait = list[Random.Range(0, list.Count)];
            Max--;
        }
        if(Max == 0)
            Debug.Log("Could not find appropriate Trait");
        OutcomeTraits.Add(current_trait);

    }
}
