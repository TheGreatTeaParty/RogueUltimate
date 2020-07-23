using System;
using UnityEngine;

// Change it on Interface
public class TradeAction : MonoBehaviour
{
    //Bind Function removed to TavernKeeper.Interact()

    // true - buy, false - sell
    public void Trade()
    {
        var tradeManager = TradeManager.Instance;
        
        if (tradeManager.State && tradeManager.currentItem != null)
            tradeManager.Buy();
        else
            tradeManager.Sell();
    }

    public void Close()
    {
        var tradeManager = TradeManager.Instance;

        tradeManager.CloseTooltip();
        tradeManager.UI.SetActive(false);
        tradeManager.currentItem = null;
    }

} 