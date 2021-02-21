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
        
        Transform text = Instantiate(textPb, new Vector3(Random.Range(position.x +0.5f,position.x - 0.5f), position.y + 1, position.z), Quaternion.identity);
        TextMeshPro temp = text.GetComponent<TextMeshPro>();
        temp.text = ((int)damage).ToString();
       

        if (!_isCrit)
            temp.color = new Color32(255, 168, 0, 255);
        else
        {
            temp.fontSize = 6;
            temp.color = new Color32(218, 33, 45, 255);
        }

    }

}
