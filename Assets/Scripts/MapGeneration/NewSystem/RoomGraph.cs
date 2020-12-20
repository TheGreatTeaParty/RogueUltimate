using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnLogic))]
public class RoomGraph : MonoBehaviour
{
    public int RoomNum;
   

    //Spawn a reward room every 4 rooms
    //Ammount of hubs cant be bigger than the number of rewards

    [SerializeField]
    private int _rewardCoeficient = 4;

    [SerializeField]
    private int _eventNumbers = 2;

    [HideInInspector]
    public List<Room> rooms;


    private int hubCount = 0;
    private int _rewardCount = 0;
    private int _rewardTotal = 0;

   
    public void GenerateGraph()
    {
        AddRoom(RoomType.Start);

        for (int i = 1; i < RoomNum; i++)
        {
            if (_rewardCount == _rewardCoeficient)
            {
                //Create Reward room
                CreateRewardRoom(i);

                //Return back for couple rooms and split to another tree
                AddTree(i - Random.Range(1, 3));
                i++;
                continue;
            }

            //Add another room and assign it to the prev room
            AddRoom(GenerateType());
            rooms[i - 1].AddNextRoom(rooms[i]);
        }

        //Add shop to the end:
        //Using the list inject the SHop at back:
        AddRoom(RoomType.Shop);
        rooms[rooms.Count - 2].AddNextRoom(rooms[rooms.Count-1]);

        //Add event to the graph 2d stage!
       InjectEvents();
    }
    
    private RoomType GenerateType()
    {
        int number = Random.Range(0, 2);
        switch (number)
        {
            case 0:
                {
                    return RoomType.Normal;
                }
            case 1:
                {
                    if (hubCount != 0 && hubCount > _rewardTotal)
                        return RoomType.Normal;

                    hubCount++;
                    return RoomType.Hub;
                }
            default:
                return RoomType.Normal;
        }
    }

    private void AddRoom(RoomType roomType)
    {
        Room temple = new Room(roomType);
        temple.index = rooms.Count;
        rooms.Add(temple);
        _rewardCount++;
    }

    private void AddTree(int index)
    {
        AddRoom(GenerateType());
        rooms[index].AddNextRoom(rooms[rooms.Count-1]);
    }

    private void CreateRewardRoom(int index)
    {
        AddRoom(RoomType.Reward);
        rooms[index - 1].AddNextRoom(rooms[index]);
        _rewardCount = 0;
        _rewardTotal++;
    }

    private void InjectEvents()
    {
        //Choose the appropriate event to spawn:
        //20% chance to add room to normal room or hub
        int total = 0;

        for (int i = 2; i < RoomNum; i++)
        {
            //If we got 20 percents to spawn an event:
            if ((Random.value > 0.8f && total < _eventNumbers) ||
                (rooms[i].count == 0 && total < _eventNumbers))
            {
                //Create Event room
                AddRoom(RoomType.Event);
                rooms[i].AddNextRoom(rooms[rooms.Count - 1]);
                i++;
                total++;
            }
        }

    }
    public void DeleteGraph()
    {
        Destroy(this);
    }
}
