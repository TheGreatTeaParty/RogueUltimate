using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVisual : MonoBehaviour
{
    public GameObject ClosedDoor;
    public GameObject OpenDoor;


    private bool _isOpen = false;
    private bool _isBlocked = false;


    private void Open()
    {
        if (!_isOpen && !_isBlocked)
        {
            ClosedDoor.SetActive(false);
            OpenDoor.SetActive(true);
            _isOpen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
                Open();
        }
    }

    public void Close()
    {
        ClosedDoor.SetActive(true);
        OpenDoor.SetActive(false);
        _isBlocked = true;
    }

    public void Restore()
    {
        if (_isOpen)
        {
            ClosedDoor.SetActive(false);
            OpenDoor.SetActive(true);

            //Clean the memory

            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this);
        }
        _isBlocked = false;
    }
}
