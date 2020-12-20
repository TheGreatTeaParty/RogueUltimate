using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonName : MonoBehaviour
{
    public string Name;
    public float DisplayTime = 2f;

    [SerializeField]
    private TextMeshProUGUI label;

    [SerializeField]
    private GameObject DisplayTab;

   
    public void DisplayName()
    {
        DisplayTab.SetActive(true);
        label.text = name;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(DisplayTime);
        DisplayTab.SetActive(false);
        Destroy(gameObject);
    }
    
}
