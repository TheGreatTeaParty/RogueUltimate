using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorVisual : MonoBehaviour
{
    public GameObject RoomEnteringTarget;

    public GameObject ClosedDoor;
    public GameObject OpenDoor;


    private bool _isOpen = false;
    private bool _isBlocked = false;
    private AudioSource _audioSource;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Open()
    {
        if (!_isOpen && !_isBlocked)
        {
            ClosedDoor.SetActive(false);
            OpenDoor.SetActive(true);
            _audioSource.Play();
            _isOpen = true;
            PushPlayerToTheRoom();
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
        _audioSource.Play();
        _isBlocked = true;
    }
    public void PushPlayerToTheRoom()
    {
        StartCoroutine(PlayerOnScene.Instance.playerMovement.DisablePlayerControllWithoutStopping(1.1f, (RoomEnteringTarget.transform.position - PlayerOnScene.Instance.playerMovement.PlayerCollider.bounds.center).normalized));
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
