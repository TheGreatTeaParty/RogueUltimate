using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    private ContractSlot[] playerSlots;
    private ContractHolder playerContracts;

    public NavigatorButton NavigatorButton;

    // Start is called before the first frame update
    void Start()
    {
        playerContracts = CharacterManager.Instance.Stats.PlayerContracts;
        playerSlots = GetComponentsInChildren<ContractSlot>();
        NavigatorButton.onWindowChanged += UpdateUI;
    }
  
    void UpdateUI(WindowType windowType, NavigatorButton navigatorButton)
    {
        int j = 0;
        for (int i = 0; i < playerContracts.contracts.Capacity; i++)
        {
            if (i < playerContracts.contracts.Count)
            {
                if (playerContracts.contracts[i].type == Contract.contractType.Regular)
                {
                    playerSlots[j].Item = playerContracts.contracts[i];
                    ++j;
                }
            }
            else
            {
                playerSlots[j].Item = null;
                ++j;
            }

        }
        for (int i = 0; i < playerContracts.contracts.Count; i++)
        {
            if (playerContracts.contracts[i].type == Contract.contractType.Major)
                playerSlots[3].Item = playerContracts.contracts[i];
        }
    }
}
