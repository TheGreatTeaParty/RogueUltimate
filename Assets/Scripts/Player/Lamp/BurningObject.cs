using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class BurningObject : MonoBehaviour
{
    public float timeToBurn = 20f;
    public Light2D Light;

    private bool _startToBurn = false;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        Light.enabled = false;
        timeLeft = timeToBurn;
    }

    // Update is called once per frame
    void Update()
    {
        if (_startToBurn)
            timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
            Destroy(gameObject);
    }

    public void BurnTheObject()
    {
        _startToBurn = true;
        Light.enabled = true;
    }

}
