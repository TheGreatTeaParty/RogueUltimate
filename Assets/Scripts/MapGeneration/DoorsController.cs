using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;

    private Collider2D collider;
    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else if(collision.tag == "Player")
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
