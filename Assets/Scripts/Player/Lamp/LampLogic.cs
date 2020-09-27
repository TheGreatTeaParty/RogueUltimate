using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LampLogic : MonoBehaviour
{
    public float timeToBurn = 30f;
    public float TimeToActivate = 1f;

    private float timeLeft;
    private LampRender lampRender;

    private bool _courantineStarted = false;
    
    void Start()
    {
        timeLeft = timeToBurn;
        lampRender = GetComponent<LampRender>();

        //turn the light on
        lampRender.TurnTheLamp(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
            timeLeft -= Time.deltaTime;
        else
        {
            //Turn the light off
            lampRender.TurnTheLamp(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BurnItems"))
        {
            if (!_courantineStarted)
            {
                StartCoroutine(ActionTimer(collision));
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BurnItems"))
        {
            StopAllCoroutines();
            _courantineStarted = false;
        }
    }

    IEnumerator ActionTimer(Collider2D collision)
    {
        _courantineStarted = true;
        yield return new WaitForSeconds(TimeToActivate);
        collision.GetComponent<BurningObject>().BurnTheObject();
        _courantineStarted = false;
    }
}
