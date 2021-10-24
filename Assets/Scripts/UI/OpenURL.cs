using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string URL;

    public void OpenWebURL()
    {
        Application.OpenURL(URL);
    }
}
