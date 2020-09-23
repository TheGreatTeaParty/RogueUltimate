using System;
using UnityEngine;


public class PlayerCamera : MonoBehaviour
{
   #region Singleton
    
    public static PlayerCamera Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(Instance);
            Instance = this;
            DontDestroyOnLoad(this);
        }
        
    }
    
    #endregion

    
    private Transform _target;

    public GameObject prefab;
    public float smoothSpeed;
    public Vector3 offset;

    
    // Cache
    private Vector3 _desiredPosition;
    private Vector3 _smoothedPosition;

    private void Start()
    {
        if (gameObject.CompareTag("Player") && prefab)
        {
            Instantiate(prefab, transform.position, Quaternion.identity, null);
        }

        _target = FindObjectOfType<KeepOnScene>().gameObject.transform;
        transform.position = _target.position + offset;
    }

    private void FixedUpdate()
    {
        _desiredPosition = _target.position + offset; 
        _smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = _smoothedPosition;
        
        //transform.LookAt(_target);
    }
    
}