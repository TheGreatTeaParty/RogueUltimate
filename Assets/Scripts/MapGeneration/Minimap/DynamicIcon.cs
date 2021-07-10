using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DynamicIcon : MonoBehaviour
{
    public GameObject LightParent;
    public GameObject DoorParent;


    [SerializeField]
    bool TurnOff = true;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private Light2D _roomLight;
    private Light2D[] _lights;
    private SpriteRenderer[] _doorIcons;

    private float IntencityOut = 0.7f;
    private bool _triggeredOn = false;
    private bool _triggeredOff = false;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        _lights = LightParent.GetComponentsInChildren<Light2D>();
        _doorIcons = DoorParent.GetComponentsInChildren<SpriteRenderer>();

        if (TurnOff)
        {
            SetDoorsIconState(false);
            sprite.enabled = false;
            SetLightsState(false);
            if (_roomLight)
                _roomLight.intensity = 0;
        }
    }


    private void Update()
    {

        if (_triggeredOn)
        {
            _roomLight.intensity += 0.1f;
            if(_roomLight.intensity >= 1.4)
            {
                _triggeredOn = false;
            }
        }

        else if (_triggeredOff)
        {
            _roomLight.intensity -= 0.1f;
            if(_roomLight.intensity <= IntencityOut)
            {
                _triggeredOff = false;
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (sprite)
            {
                if (!sprite.enabled)
                    sprite.enabled = true;
                sprite.color = new Color(0.6f, 0.6f, 0.6f);
            }
            SetDoorsIconState(true);

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
            if (_roomLight)
                _triggeredOff = true;
            DungeonMusic.Instance.TurnTheCombatMusicOFF();
        }
    }

    private void SetLightsState(bool state)
    {
        if (_lights != null)
        {
            for (int i = 0; i < _lights.Length; i++)
            {
                _lights[i].gameObject.SetActive(state);
            }
        }
    }

    private void SetDoorsIconState(bool state)
    {
        if (_doorIcons != null)
        {
            for (int i = 0; i < _doorIcons.Length; i++)
            {
                if (_doorIcons[i].gameObject.layer == 10)
                    _doorIcons[i].enabled = state;
            }
        }
    }
}
