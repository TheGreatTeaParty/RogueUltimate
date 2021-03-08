using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitsGenerator : MonoBehaviour
{
    enum RenownLevel
    {
        Scum = 0,
        MurderHobo,
        BattleSeasoned,
        Adventurer,
        Hero,
    };

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

    private void Start()
    {
        GenerateTrait();
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
    }

    private void GenerateLastTrait(List<Trait> list)
    {
        if (Random.Range(0, 1f) < 0.9f)
        {
            Trait current_trait = list[Random.Range(0, list.Count)]; ;
            while (!IsUsed(current_trait))
            {
                current_trait = list[Random.Range(0, list.Count)];
            }
            OutcomeTraits.Add(current_trait);
        }

        else
        {
            OutcomeTraits.Clear();
            GenerateTrait();
        }
    }

    private void GenerateFirstTrait(List<Trait> list)
    {
        if (Random.Range(0, 1f) < 0.95f)
        {
            Trait current_trait = list[Random.Range(0, list.Count)]; ;
            while (!IsUsed(current_trait))
            {
                current_trait = list[Random.Range(0, list.Count)];
            }
            OutcomeTraits.Add(current_trait);
        }

        else
        {
            OutcomeTraits.Clear();
            GenerateTrait();
        }
    }
}
