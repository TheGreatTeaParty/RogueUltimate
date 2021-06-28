using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Items/TeleportStone")]
public class TeleportStone : UsableItem
{
    public Transform VictoryPanel;

    public override void Use()
    {
        if (SceneManager.GetActiveScene().name == "StartTavern" || SceneManager.GetActiveScene().name == "Tavern")
        {
            CharacterManager.Instance.Inventory.AddItem(this);
            return;
        }

        base.Use();
        Time.timeScale = 0;
        var UI = InterfaceManager.Instance;
        UI.HideAll();
        Instantiate(VictoryPanel);
    }
}
