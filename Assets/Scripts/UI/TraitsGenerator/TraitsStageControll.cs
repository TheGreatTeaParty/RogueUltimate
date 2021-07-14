using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitsStageControll : MonoBehaviour
{
    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;
    }

    public void TurnONStage1()
    {
        Stage1.SetActive(true);
    }
    public void TurnOFFStage1()
    {
        Stage1.SetActive(false);
    }

    public void TurnONStage2()
    {
        Stage2.SetActive(true);
    }
    public void TurnOFFStage2()
    {
        Stage2.SetActive(false);
    }
    public void TurnONStage3()
    {
        Stage3.SetActive(true);
    }
    public void TurnOFFStage3()
    {
        Stage3.SetActive(false);
    }

    public void FlipSound()
    {
        audioManager.Play("PageFlip");
    }

    public void OpenSound()
    {
        audioManager.Play("Book");
    }


}
