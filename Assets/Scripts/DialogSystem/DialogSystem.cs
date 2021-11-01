using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class DialogSystem
{
    /*TO ADD CHARACTER :
         * 1) Add enum
         * 2) Increase the ammount of count
    */
    public enum ECharacterNames
    {
        Mentor = 0,
        TavernKeeper,
        Smith,
        Master,
        Dwarf,
        Adventurer
    };
    private static int _enumCount = 6;


    private static bool _isInit = false;
    private static Dictionary<ECharacterNames, int> _currentDialogIndex;

    public static string GetDialogText(ECharacterNames characterName)
    {
        if (!_isInit)
            Init();

        int currentIndex = _currentDialogIndex[characterName];
        _currentDialogIndex[characterName]++;
        Debug.LogWarning("dialog_" + characterName.ToString() + "_" + currentIndex);
        return LocalizationSystem.GetLocalisedValue("dialog_" + characterName.ToString() + "_" + currentIndex);

    }
    public static string GetDialogTextPlaceHolder(ECharacterNames characterName)
    {
        if (!_isInit)
            Init();
        return LocalizationSystem.GetLocalisedValue("dialog_" + characterName.ToString() + "_" + "temp");
    }

    private static void Init()
    {
        _isInit = true;
        _currentDialogIndex = new Dictionary<ECharacterNames, int>(_enumCount);
        for(int  i =0; i < _enumCount; i++)
        {
            _currentDialogIndex.Add((ECharacterNames)i, 0);
        }
    }

    public static int[] GetDialogIndexArray()
    {
        if (!_isInit)
            Init();

        int[] arr = new int[_enumCount];
        int currentIndex = 0;
        for (int i = 0; i < _enumCount; i++)
        {
            if (_currentDialogIndex.TryGetValue((ECharacterNames)i, out currentIndex))
                arr[i] = currentIndex;
            else
                arr[i] = 0;
        }
        return arr;
    }

    public static void LoadDialogIndex(AccountData data)
    {
        if (!_isInit)
            Init();

        for (int i = 0; i < _enumCount; i++)
        {
            if(data.dialogIndexes!= null && i < data.dialogIndexes.Length)
                _currentDialogIndex[(ECharacterNames)i] = data.dialogIndexes[i];
        }
    }

    
}
