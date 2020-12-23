using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance = null;

    private Dictionary<SettingsKeys, string> stringSettings;

    public enum SettingsKeys
    {
        isKeyboardAllowed = 0
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Debug.LogError("Error while creating \"SettingsManager\" object. It's already exists!");
            Destroy(this);
        }

        InitializeManager();
    }

    private void InitializeManager()
    {
        stringSettings = new Dictionary<SettingsKeys, string>();
        DontDestroyOnLoad(gameObject);
        
        LoadSettings();
    }

    /// <summary>
    ///  Add key/value pair to settings dictionary and save them into PlayerPrefs.
    /// </summary>
    /// <param name="setting">Key/Value pair of changable setting.</param>
	public void ChangeSetting(KeyValuePair<SettingsKeys, string> setting)
	{
        stringSettings.Remove(setting.Key);
        stringSettings.Add(setting.Key, setting.Value);
        SaveSettings();
	}

    /// <summary>
    ///  Add key/value pair to settings dictionary and save them into PlayerPrefs.
    /// </summary>
    /// <param name="setting">Key/Value pair of changable setting.</param>
	public void ChangeSetting(KeyValuePair<SettingsKeys, bool> setting)
    {
        stringSettings.Remove(setting.Key);
        stringSettings.Add(setting.Key, setting.Value.ToString());
        SaveSettings();
    }

    /// <summary>
    ///  Add key and value to settings dictionary and save them into PlayerPrefs.
    /// </summary>
	public void ChangeSetting(SettingsKeys key, bool value)
    {
        stringSettings.Remove(key);
        stringSettings.Add(key, value.ToString());
        SaveSettings();
    }

    /// <summary>
    ///  Add key and value to settings dictionary and save them into PlayerPrefs.
    /// </summary>
	public void ChangeSetting(SettingsKeys key, string value)
    {
        stringSettings.Remove(key);
        stringSettings.Add(key, value.ToString());
        SaveSettings();
    }

    private void LoadSettings()
	{
        foreach(string settingKey in Enum.GetNames(typeof(SettingsKeys)))
		{
            // TODO: Make something with default value setter.
            stringSettings.Add((SettingsKeys)Enum.Parse(typeof(SettingsKeys), settingKey), PlayerPrefs.GetString(settingKey, "true"));
		}
        Debug.Log("Settings have been loaded.");
        Debug.Log(stringSettings[SettingsKeys.isKeyboardAllowed]);
	}

    private void SaveSettings()
	{
        foreach(KeyValuePair<SettingsKeys, string> setting in stringSettings)
		{
            PlayerPrefs.SetString(Enum.GetName(typeof(SettingsKeys), (int)setting.Key), setting.Value);
		}
        Debug.Log("Settings have been saved.");
        PlayerPrefs.Save();
	}
}
