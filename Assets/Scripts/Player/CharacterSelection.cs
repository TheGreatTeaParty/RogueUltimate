using UnityEngine;
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
    public Camera selectionCamera;
    public float speed = 1.5f;
    public Transform[] prefabs;
    public GameObject[] options;
    int Index;

    private bool _isChanged = false;
    private Vector3 dir = Vector3.zero;

    void Update()
    { 
        for (int i = 0; i < options.Length; i++)
        {
            if (i == Index)
            {
                if (!_isChanged)
                {
                    //Change slider value (We can chane options to prefabs later, when we will create more characters)
                    NameText.text = prefabs[i].GetComponent<PlayerStat>().CharacterName;
                    healthStat.value = prefabs[i].GetComponent<PlayerStat>().MaxHealth;
                    staminaStat.value = prefabs[i].GetComponent<PlayerStat>().MaxStamina;
                    manaStat.value = prefabs[i].GetComponent<PlayerStat>().MaxMana;
                    _isChanged = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        dir = ((options[Index].transform.position + new Vector3(0.8f,0)) - selectionCamera.transform.position).normalized;
        dir.z = 0f;

        if (Vector3.Distance(selectionCamera.transform.position,options[Index].transform.position) > 0.8f)
            selectionCamera.transform.position += dir * speed * Time.deltaTime;
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
        Destroy(selectionCamera.gameObject);
        Destroy(gameObject);
    }

}
