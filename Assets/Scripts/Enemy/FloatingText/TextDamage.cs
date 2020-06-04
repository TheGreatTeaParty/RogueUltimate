using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextDamage : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
        Destroy(this.gameObject, 1f);
    }
}
