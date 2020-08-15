using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLayerRenderer : MonoBehaviour
{
    [SerializeField]
    private int baseLayer = 5000;

    [SerializeField]
    private float offSet = 0f;

    [SerializeField]
    private bool runOnlyOnce = false;

    private Renderer myRenderer;

    
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        InvokeRepeating("RenderObject", 0.1f, 0.3f);
    }

    private void RenderObject()
    {
        myRenderer.sortingOrder = (int)(baseLayer - transform.position.y - offSet);
        if (runOnlyOnce)
            Destroy(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - offSet, transform.position.z),0.05f);
    }
}
