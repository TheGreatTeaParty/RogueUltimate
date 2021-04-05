using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvadeVisual : MonoBehaviour
{
    public PlayerStat playerStat;
    public Transform textPb;

    // Start is called before the first frame update
    void Start()
    {
        playerStat.OnEvadeTriggered += Evade;
    }

    private void Evade()
    {
        var position = transform.position;

        Transform text = Instantiate(textPb, new Vector3(position.x, position.y + 0.5f, position.z), Quaternion.identity);
        TextMeshPro temp = text.GetComponent<TextMeshPro>();
        temp.text = "Evade";
        temp.fontSize = 2;
        temp.color = new Color32(166, 166, 166, 255);
    }
}
