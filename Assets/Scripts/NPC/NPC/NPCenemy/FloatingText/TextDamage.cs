using UnityEngine;


public class TextDamage : MonoBehaviour
{
    private void Start()
    {
        if (gameObject.transform.parent)
            Destroy(gameObject.transform.parent.gameObject, 1f);
        Destroy(gameObject, 1f);
    }
    private void Update()
    {
        float moveYSpeed = 1.25f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
    }
}
