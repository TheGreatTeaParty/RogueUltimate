using UnityEngine;

public class Transparent : MonoBehaviour
{
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    
    
    void Start()
    {
        sprite = GetComponentInParent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            sprite.color = new Color(1f, 1f, 1, 0.25f);
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            sprite.color = new Color(1f, 1f, 1f,1f);
    }
    
}
