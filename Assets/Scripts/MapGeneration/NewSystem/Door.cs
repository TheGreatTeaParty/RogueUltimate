using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Direction direction;

    [SerializeField] GameObject wall;
    [SerializeField] GameObject door;

    [SerializeField]
    private GameObject spawnPoint;

    private State _state = State.wall;

    public enum State
    {
        wall = 0,
        door,
    };

    public Vector3 GetSpawnPosition()
    {
        return spawnPoint.transform.position;
    }

    public void SetState(State state)
    {
        _state = state;
        if(_state == State.door)
        {
            door.SetActive(true);
            wall.SetActive(false);
        }
        else
        {
            door.SetActive(false);
            wall.SetActive(true);
        }
    }

    public enum Direction
    {
        None = 0,
        Top,
        Right,
        Bot,
        Left,
    }
}
