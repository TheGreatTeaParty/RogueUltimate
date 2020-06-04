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

    private void RecieveDamage(int damage)
    {
        this.damage = damage;
        textPb.GetComponent<TextMeshPro>().text = damage.ToString();
        Instantiate(textPb, new Vector3(transform.position.x,transform.position.y+1,transform.position.z), Quaternion.identity);
    }
    public int GetDamage()
    {
        return damage;
    }
}
