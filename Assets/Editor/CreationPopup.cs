using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class CreationPopup : EditorWindow
{
    public Action<ItemType> onReceivedData;
    public ItemsEditor itemsEditor;
    private ItemType itemType;
    private string itemName;

    private static void Init()
    {
        CreationPopup window = ScriptableObject.CreateInstance<CreationPopup>();
        window.minSize = new Vector2(300,150);
        window.titleContent.text = "Item Creation options:";
        window.ShowModalUtility();
    }
    
    void OnGUI()
    {
        itemName = EditorGUILayout.TextField("Item`s name: ", itemName);

        itemType = (ItemType)EditorGUILayout.EnumPopup("Type of item: ", itemType);

        if (GUILayout.Button("Create"))
            if(itemName.Length > 1)
            {
                itemsEditor.SetItemName(itemName);
                onReceivedData?.Invoke(itemType);
                Close();
            }
            else
            {
                Debug.LogWarning("Please, fill the name text area!");
            }

    }

    public enum ItemType
    {
        HealthPotion = 0,
        ManaPotion,
        StaminaPotion,
        Armor,
        Amulet,
        Ring,
        MagicWeapon,
        MeleWeapon,
        RangeWeapon,
    };
}
