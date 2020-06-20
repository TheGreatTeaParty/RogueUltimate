using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    void MoveToTarget(Pathfinding pathfinding,Vector3 target, bool _isRange);
    void HangOut(Pathfinding pathfinding, Vector3[] pathPoints = null);
}
