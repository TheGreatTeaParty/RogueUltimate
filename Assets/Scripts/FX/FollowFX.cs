using UnityEngine;

public class FollowFX : MonoBehaviour
{
    private GameObject Player;

    // Start is called before the first frame update
    void Awake()
    {
        Player = PlayerOnScene.Instance.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Player.transform.position;
    }
}
