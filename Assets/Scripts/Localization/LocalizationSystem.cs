using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem
{
    public enum Language
    {
        English,
        Russian
    }

    public static Language language = Language.English;

    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localizedRU;

    public static bool isInit;

    private static CSVloader csvloader;

    public static void Init()
    {
        csvloader = new CSVloader();
        csvloader.LoadCSV();

        localisedEN = csvloader.GetDictionaryValues("en");
        localizedRU = csvloader.GetDictionaryValues("ru");

        isInit = true;
    }

    public static string GetLocalisedValue(string key)
    {
        if (!isInit) { Init(); }
        string value = key;

        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Russian:
                localizedRU.TryGetValue(key, out value);
                break;
        }
        return value;
    }
    public static string GetLocalisedValue(string key, string data)
    {
        if (!isInit) { Init(); }
        string value = key;

        switch (language)
        {
            case Language.English:
                {
                    if (!localisedEN.TryGetValue(key, out value))
                        csvloader.WriteDictionaryValue(data);
                    break;
                }
            case Language.Russian:
                {
                    if (!localisedEN.TryGetValue(key, out value))
                        csvloader.WriteDictionaryValue(data);
                    break;
                }
        }
        return value;
    }
}
