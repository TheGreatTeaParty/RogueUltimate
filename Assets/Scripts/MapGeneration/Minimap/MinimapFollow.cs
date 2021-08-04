using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 newposition = player.transform.position;
            newposition.z = transform.position.z;
            transform.position = newposition;
        }
    }
}
