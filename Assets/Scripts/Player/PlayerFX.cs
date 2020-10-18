using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    public void SpawnEffect(Transform effect)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
    }
      
}
