using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TourchLight : MonoBehaviour
{
    public float minInt = 3f, maxInt = 5f;

    Light2D tourchLight;
    float _lightInt;

    
    void Start()
    {
        tourchLight = GetComponent<Light2D>();
    }

    void Update()
    {
        _lightInt = Random.Range(minInt, maxInt);
        tourchLight.intensity = _lightInt;
    }
}
