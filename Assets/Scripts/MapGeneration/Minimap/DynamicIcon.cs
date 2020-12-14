using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DynamicIcon : MonoBehaviour
{
    public float IntencityOut = 0.7f;

    [SerializeField]
    bool TurnOff = true;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private Light2D _roomLight;

    private float _ligthIntecity;
    private bool _triggeredOn;
    private bool _triggeredOff;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        if(_roomLight)
            _ligthIntecity = _roomLight.intensity;

        if (TurnOff)
        {
            if (_roomLight)
                _roomLight.intensity = 0;
            sprite.enabled = false;
        }
    }

    private void Update()
    {
        if (_triggeredOn)
        {
            _roomLight.intensity += 0.1f;
            if (_roomLight.intensity >= _ligthIntecity)
                _triggeredOn = false;
        }

        else if (_triggeredOff)
        {
            _roomLight.intensity -= 0.1f;
            if (_roomLight.intensity <= IntencityOut)
                _triggeredOff = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!sprite.enabled)
                sprite.enabled = true;
            sprite.color = new Color(0.6f, 0.6f, 0.6f);

            //Discover a new room tirn the light on
            if (_roomLight)
                _triggeredOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sprite.color = new Color(0.3f, 0.3f, 0.3f);
            _triggeredOff = true;
        }
    }
}
