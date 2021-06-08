using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitsDatabase : MonoBehaviour
{
    #region Singleton
    public static TraitsDatabase Instance;


    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
        DontDestroyOnLoad(this);
    }

    #endregion

    public Trait[] allTraits;


    public Trait GetTraitByID(int ID)
    {
        for (int i = 0; i < allTraits.Length; i++)
            if (ID == allTraits[i].ID) return allTraits[i];

        return null;
    }
}
