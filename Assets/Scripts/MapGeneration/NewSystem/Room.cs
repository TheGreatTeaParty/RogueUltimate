using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    public int index = 0;
    public RoomType _type;
    public Room[] _nextRoom;
    public int count;

    public Room(RoomType roomType)
    {
        _type = roomType;
        count = 0;
        _nextRoom = new Room[3];
        _nextRoom[0] = null;
        _nextRoom[1] = null;
        _nextRoom[2] = null;
    }

    public void AddNextRoom(Room next)
    {
        _nextRoom[count] = next;
        count++;
    }
}

public enum RoomType
{
    Start = 0,
    Normal,
    Hub,
    Reward,
    Shop,
    Event,
    Boss,
};
