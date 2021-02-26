using UnityEngine;
using TMPro;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private Transform text;
    [SerializeField] private ItemScene itemOnScene;
    bool _isAwake = false;
    private Rigidbody2D _rigidbody2D;
    
    private void Awake()
    {

        itemOnScene = GetComponent<ItemScene>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(new Vector2(Random.Range(-2, 2), Random.Range(-2, 2)), ForceMode2D.Impulse);

        Invoke("WakeUp", 1f);
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _isAwake == true)
        {
            bool wasPickedUp = CharacterManager.Instance.Inventory.AddItem(itemOnScene.GetItem().GetCopy());
            if (wasPickedUp)
            {
                AudioManager.Instance.Play("Collect");
                //Crete text:
                Transform name = Instantiate(text, transform.position + new Vector3(1f,1f), Quaternion.identity);
                name.GetComponent<TextMeshPro>().text = "+ " + itemOnScene.GetItem().GetCopy().name;
                Destroy(gameObject);
            }
        }
    }
    private void WakeUp()
    {
        _isAwake = true;
    }
    
    
}
