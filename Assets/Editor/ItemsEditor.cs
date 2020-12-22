using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine.UIElements;
using UnityEditor.UIElements;



public class ItemsEditor : EditorWindow
{
    private ListView itemList;
    private Item[] items;
    private string[] guild;
    private string _itemName;


    [MenuItem("Window/Items Manager")]
    public static void ShowWindow()
    {
        var window = GetWindow<ItemsEditor>();
        window.titleContent = new GUIContent("Items Editor");
        window.minSize = new Vector2(650, 300);
        window.ShowPopup();
    }

    void OnEnable()
    {
        VisualTreeAsset original = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/ItemsEditorWindow.uxml");
        TemplateContainer treeAsset = original.CloneTree();
        rootVisualElement.Add(treeAsset);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/ItemsStyle.uss");
        rootVisualElement.styleSheets.Add(styleSheet);
        CreateItemsListView();

        Button AddButton = rootVisualElement.Query<Button>("Add").First();
        AddButton.clicked += AddItem;

        Button DeleteButton = rootVisualElement.Query<Button>("Delete").First();
        DeleteButton.clicked += DeleteItem;
    }

    private void CreateItemsListView()
    {
        FindAllItems(out items);

        itemList = rootVisualElement.Query<ListView>("item-list").First();
        itemList.makeItem = () => new Label();

        try
        {
            itemList.bindItem = (element, i) => (element as Label).text = items[i].ItemName;
        }
        catch
        {

        }

        itemList.itemsSource = items;
        itemList.itemHeight = 16;
        itemList.selectionType = SelectionType.Single;

        itemList.onSelectionChanged += (enumerable) =>
         {
             foreach (Object it in enumerable)
             {
                 Box ItemInfoBox = rootVisualElement.Query<Box>("item-info").First();
                 Box HeadBox = rootVisualElement.Query<Box>("head").First();
                 Box DataBox = rootVisualElement.Query<Box>("data").First();

                 ItemInfoBox.Clear();
                 HeadBox.Clear();
                 DataBox.Clear();

                 Item item = it as Item;

                 SerializedObject serializedItem = new SerializedObject(item);
                 SerializedProperty itemProperty = serializedItem.GetIterator();
                 itemProperty.Next(true);

                 while (itemProperty.NextVisible(false))
                 {
                     PropertyField prop = new PropertyField(itemProperty);

                     prop.SetEnabled(itemProperty.name != "m_Script");
                     prop.Bind(serializedItem);

                     if (itemProperty.name == "ID" || itemProperty.name == "tier" || itemProperty.name == "price" || itemProperty.name == "itemName")
                         HeadBox.Add(prop);
                     else if (itemProperty.name == "description")
                         DataBox.Add(prop);
                  
                     else
                     {
                         ItemInfoBox.Add(prop);

                         if (itemProperty.name == "sprite")
                         {
                             prop.RegisterCallback<ChangeEvent<UnityEngine.Object>>((changeEvt) => LoadItemImage(item.Sprite));
                         }
                     }
                 }

                 LoadItemImage(item.Sprite);
             }
         };
        itemList.Refresh();
    }

    private void FindAllItems(out Item[] items)
    {
        guild = AssetDatabase.FindAssets("t:Item");
        items = new Item[guild.Length];

        for(int i = 0; i < guild.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(guild[i]);
            items[i] = AssetDatabase.LoadAssetAtPath<Item>(path);
        }
    }
   
    private void LoadItemImage(Sprite texture)
    {
        var ItemPreviewImage = rootVisualElement.Query<Image>("preview").First();
        ItemPreviewImage.image = AssetPreview.GetAssetPreview(texture);
    }

    private void AddItem()
    {
        PopUp();
    }

    private void DeleteItem()
    {
        int index = itemList.selectedIndex;
        var path = AssetDatabase.GUIDToAssetPath(guild[index]);
        AssetDatabase.DeleteAsset(path);
        itemList.RemoveAt(index);
        if(index - 1 < 0)
            itemList.selectedIndex = index+1;
        else
            itemList.selectedIndex = index - 1;
        AssetDatabase.SaveAssets();
        itemList.Refresh();
        CreateItemsListView();
    }
   
    private void PopUp()
    {
        CreationPopup window = CreateInstance<CreationPopup>();
        window.minSize = new Vector2(450, 300);
        window.maxSize = new Vector2(450, 300);
        window.titleContent.text = "Item Creation options:";
        window.itemsEditor = this;
        window.Show();
        window.onReceivedData += HandleType;

    }

    private void HandleType(CreationPopup.ItemType itemType)
    {
       
       switch (itemType)
        {
            case CreationPopup.ItemType.HealthPotion:
                {
                    HealthPotion asset = CreateInstance<HealthPotion>();
                    asset.ItemName = _itemName;
                    AssetDatabase.CreateAsset(asset, $"Assets/Prefabs/Items/Potions/" + _itemName + ".asset");
                    break;
                }
            case CreationPopup.ItemType.ManaPotion:
                {
                    ManaPotion asset = CreateInstance<ManaPotion>();
                    asset.ItemName = _itemName;
                    AssetDatabase.CreateAsset(asset, $"Assets/Prefabs/Items/Potions/" + _itemName + ".asset");
                    break;
                }
            case CreationPopup.ItemType.StaminaPotion:
                {
                    StaminaPotion asset = CreateInstance<StaminaPotion>();
                    asset.ItemName = _itemName;
                    AssetDatabase.CreateAsset(asset, $"Assets/Prefabs/Items/Potions/" + _itemName + ".asset");
                    break;
                }
            case CreationPopup.ItemType.Amulet:
                {
                    EquipmentItem asset = CreateInstance<EquipmentItem>();
                    asset.ItemName = _itemName;
                    asset.EquipmentType = EquipmentType.Amulet;
                    AssetDatabase.CreateAsset(asset, $"Assets/Prefabs/Items/Amulets/" + _itemName + ".asset");
                    break;
                }
            case CreationPopup.ItemType.Ring:
                {
                    EquipmentItem asset = CreateInstance<EquipmentItem>();
                    asset.EquipmentType = EquipmentType.Ring;
                    asset.ItemName = _itemName;
                    AssetDatabase.CreateAsset(asset, "Assets/Prefabs/Items/Rings/" + _itemName + ".asset");
                    break;
                }
            case CreationPopup.ItemType.Armor:
                {
                    EquipmentItem asset = CreateInstance<EquipmentItem>();
                    asset.ItemName = _itemName;
                    asset.EquipmentType = EquipmentType.Armor;
                    AssetDatabase.CreateAsset(asset, $"Assets/Prefabs/Items/Armor/" + _itemName + ".asset");
                    break;
                }
            case CreationPopup.ItemType.MeleWeapon:
                {
                    MeleeWeapon asset = CreateInstance<MeleeWeapon>();
                    asset.ItemName = _itemName;
                    asset.EquipmentType = EquipmentType.Weapon;
                    AssetDatabase.CreateAsset(asset, $"Assets/Prefabs/Items/Weapons/" + _itemName + ".asset");
                    break;
                }
            case CreationPopup.ItemType.MagicWeapon:
                {
                    MagicWeapon asset = CreateInstance<MagicWeapon>();
                    asset.ItemName = _itemName;
                    asset.EquipmentType = EquipmentType.Weapon;
                    AssetDatabase.CreateAsset(asset, $"Assets/Prefabs/Items/Weapons/" + _itemName + ".asset");
                    break;
                }
            case CreationPopup.ItemType.RangeWeapon:
                {
                    RangeWeapon asset = CreateInstance<RangeWeapon>();
                    asset.ItemName = _itemName;
                    asset.EquipmentType = EquipmentType.Weapon;
                    AssetDatabase.CreateAsset(asset, $"Assets/Prefabs/Items/Weapons/" + _itemName + ".asset");
                    break;
                }
        }

        AssetDatabase.SaveAssets();
        CreateItemsListView();
    }

    public void SetItemName(string name)
    {
        _itemName = name;
    }
}
