using UnityEngine;


public class TextDamage : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    private void Update()
    {
        float moveYSpeed = 1.25f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
    }
}
