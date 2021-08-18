using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightWin : MonoBehaviour
{
    public Transform VictoryPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var UI = InterfaceManager.Instance;
            UI.HideAll();
            Instantiate(VictoryPanel);
        }
    }
}
