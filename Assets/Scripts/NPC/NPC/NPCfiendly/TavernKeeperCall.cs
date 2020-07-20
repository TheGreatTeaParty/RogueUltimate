using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernKeeperCall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            TavernKeeper.Instance.Call(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            TavernKeeper.Instance.Call(false);
    }
}
