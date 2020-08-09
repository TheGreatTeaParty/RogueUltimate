﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public GameObject Interface;

    [Space]
    public TextMeshProUGUI NameText;
    public Slider healthStat;
    public Slider staminaStat;
    public Slider manaStat;
    public Camera camera;
    public Transform[] prefabs;
    public GameObject[] options;
    int Index;

    private bool _isChanged = false;


    void Update()
    { 
        for (int i = 0; i < options.Length; i++)
        {
            if (i == Index)
            {
                if (!_isChanged)
                {
                    camera.transform.position = options[i].transform.position + new Vector3(0.8f, 0f, -1f);

                    //Change slider value (We can chane options to prefabs later, when we will create more characters)
                    NameText.text = prefabs[i].GetComponent<PlayerStat>().Name;
                    healthStat.value = prefabs[i].GetComponent<PlayerStat>().maxHealth;
                    staminaStat.value = prefabs[i].GetComponent<PlayerStat>().maxStamina;
                    manaStat.value = prefabs[i].GetComponent<PlayerStat>().maxMana;
                    _isChanged = true;
                }
            }
        }
    }

    public void SwapRight()
    {
        if (Index < options.Length - 1)
        {
            Index++;
        }
        else
        {
            Index = 0;
        }
        
        _isChanged = false;
    }

    public void SwapLeft()
    {
        if (Index > 0)
        {
            Index--;
        }
        else
        {
            Index = options.Length - 1;
        }
        _isChanged = false;
    }

    public void StartGame()
    {
        Instantiate(prefabs[Index], options[Index].transform.position, Quaternion.identity);
        Destroy(options[Index]);
        Interface.SetActive(true);
        Destroy(gameObject);
    }

}
