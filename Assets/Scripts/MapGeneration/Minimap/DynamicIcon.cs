using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DynamicIcon : MonoBehaviour
{
    public float IntencityOut = 0.7f;
    public GameObject LightParent;
    [SerializeField]
    bool TurnOff = true;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private Light2D _roomLight;
    private Light2D[] _lights;

    private float _ligthIntecity;
    private bool _triggeredOn = false;
    private bool _triggeredOff = false;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        _lights = LightParent.GetComponentsInChildren<Light2D>();

        if (_roomLight)
            _ligthIntecity = _roomLight.intensity;

        if (TurnOff)
        {
            if (_roomLight)
                _roomLight.intensity = 0;
            sprite.enabled = false;
            SetLightsState(false);
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
            SetLightsState(true);
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

    private void SetLightsState(bool state)
    {
        for(int i = 0; i < _lights.Length; i++)
        {
            _lights[i].gameObject.SetActive(state);
        }
    }
}
