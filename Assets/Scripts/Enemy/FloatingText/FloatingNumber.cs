using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingNumber : MonoBehaviour
{
    [SerializeField] private Transform textPb;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<EnemyStat>().onRecievedDamage += RecieveDamage;
    }

    private void RecieveDamage(int damage,bool type)
    {
        this.damage = damage;
        textPb.GetComponent<TextMeshPro>().text = damage.ToString();

        if (type)
        {
            textPb.GetComponent<TextMeshPro>().color = new Color32(255, 168, 0, 255);
            Instantiate(textPb, new Vector3(transform.position.x - 0.4f, transform.position.y + 1, transform.position.z), Quaternion.identity);
        }
        else
        {
            textPb.GetComponent<TextMeshPro>().color = new Color32(137, 91, 255, 255);
            Instantiate(textPb, new Vector3(transform.position.x + 0.4f, transform.position.y + 1, transform.position.z), Quaternion.identity);
        }
    }
    public int GetDamage()
    {
        return damage;
    }
}
