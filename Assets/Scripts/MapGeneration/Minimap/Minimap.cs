using UnityEngine;

public class Minimap : MonoBehaviour
{
    public static Minimap instance;

    private void Awake()
    {
        if(instance!= null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void ShowMap()
    {
       gameObject.SetActive(true);
    }
    public void HideMap()
    {
        gameObject.SetActive(false);
    }
}
