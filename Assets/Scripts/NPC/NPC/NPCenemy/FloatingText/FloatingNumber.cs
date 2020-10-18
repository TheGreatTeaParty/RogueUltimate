using UnityEngine;
using TMPro;

public class FloatingNumber : MonoBehaviour
{
    [SerializeField] private Transform textPb;


    private void Start()
    {
        GetComponent<EnemyStat>().onReceivedDamage += ReceiveDamage;
    }

    private void ReceiveDamage(float damage)
    {
        var position = transform.position;
        
        textPb.GetComponent<TextMeshPro>().text = damage.ToString();
        textPb.GetComponent<TextMeshPro>().color = new Color32(255, 168, 0, 255);
        Instantiate(textPb, new Vector3(position.x, position.y + 1, position.z), Quaternion.identity);
    }

}
