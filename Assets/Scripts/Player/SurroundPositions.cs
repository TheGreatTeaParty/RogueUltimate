using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundPositions : MonoBehaviour
{
    [SerializeField]
    GameObject _firstRow;
    [SerializeField]
    GameObject _secondRow;

    private EnemyFollowPosition[] firsRowVectors;
    private EnemyFollowPosition[] secondRowVectors;

    // Start is called before the first frame update
    void Start()
    {
        firsRowVectors = _firstRow.GetComponentsInChildren<EnemyFollowPosition>();
        secondRowVectors = _secondRow.GetComponentsInChildren<EnemyFollowPosition>();
    }

    public GameObject GetClosestPosition(Vector3 position, EnemyAI enemy)
    {
        UpdatePositions();


        EnemyFollowPosition nearest = null;
        foreach (var item in firsRowVectors)
        {
            if (item.IsAvailable())
            {
                nearest = item;
                break;
            }
        }

        if (nearest)
            return FindInTheFirstRow(position, nearest, enemy);
        else
            return FindInTheSecondRow(position, enemy);

       
    }
    private void UpdatePositions()
    {
        foreach (var item in firsRowVectors)
        {
            item.CheckPosition();
        }
    }
    private void UpdatePositionsSecondRow()
    {
        foreach (var item in secondRowVectors)
        {
            item.CheckPosition();
        }
    }

    private GameObject FindInTheFirstRow(Vector3 position, EnemyFollowPosition nearest, EnemyAI enemy)
    {
        float dist = Vector2.Distance(nearest.gameObject.transform.position, position);

        foreach (var item in firsRowVectors)
        {
            if (item.IsAvailable())
            {
                if (Vector2.Distance(item.gameObject.transform.position, position) < dist)
                {
                    nearest = item;
                    dist = Vector2.Distance(item.gameObject.transform.position, position);
                }
            }
        }
        Debug.Log(nearest);

        return nearest.SetPosition(enemy);
    }

    private GameObject FindInTheSecondRow(Vector3 position, EnemyAI enemy)
    {
        UpdatePositionsSecondRow();

        EnemyFollowPosition nearest = null;
        foreach (var item in firsRowVectors)
        {
            if (item.IsAvailable())
            {
                nearest = item;
                break;
            }
        }

        if(nearest == null) { return null; }

        float dist = Vector2.Distance(nearest.gameObject.transform.position, position);

        foreach (var item in secondRowVectors)
        {
            if (item.IsAvailable())
            {
                if (Vector2.Distance(item.gameObject.transform.position, position) < dist)
                {
                    nearest = item;
                    dist = Vector2.Distance(item.gameObject.transform.position, position);
                }
            }
        }

        return nearest.SetPosition(enemy);
    }
}
