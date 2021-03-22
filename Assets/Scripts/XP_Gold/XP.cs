using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XP : MonoBehaviour
{
    public int XPAmount;

    [SerializeField] private Transform text;

    private bool _isAwake = false;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(new Vector2(Random.Range(-2, 2), Random.Range(-2, 2)), ForceMode2D.Impulse);

        Invoke("WakeUp", 1f);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _isAwake == true)
        {
            AudioManager.Instance.Play("Collect");
            //
            PlayerOnScene.Instance.stats.GainXP(XPAmount);

            //Crete text:
            Transform name = Instantiate(text, transform.position + new Vector3(1f, 1f), Quaternion.identity);
            name.GetComponent<TextMeshPro>().text = "+ " + XPAmount.ToString() + " XP";
            Destroy(gameObject);
        }
    }
    private void WakeUp()
    {
        _isAwake = true;
    }
}
