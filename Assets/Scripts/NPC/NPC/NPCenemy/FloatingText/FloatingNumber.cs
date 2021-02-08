using UnityEngine;
using TMPro;

public class FloatingNumber : MonoBehaviour
{
    [SerializeField] private Transform textPb;

    private void Start()
    {
        GetComponent<EnemyStat>().onReceivedDamage += ReceiveDamage;
    }

    private void ReceiveDamage(float damage,bool _isCrit)
    {
        var position = transform.position;
        
        Transform text = Instantiate(textPb, new Vector3(position.x, position.y + 1, position.z), Quaternion.identity);
        text.GetComponent<TextMeshPro>().text = ((int)damage).ToString();
       

        if (!_isCrit)
            text.GetComponent<TextMeshPro>().color = new Color32(255, 168, 0, 255);
        else
        {
            text.GetComponent<TextMeshPro>().fontSize = 6;
            text.GetComponent<TextMeshPro>().color = new Color32(218, 33, 45, 255);
        }

    }

}
