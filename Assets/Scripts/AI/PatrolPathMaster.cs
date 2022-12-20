using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPathMaster
{
    PatrolPath[] _patrolPaths;





    public void SetPatrolPaths(PatrolPath[] patrolPaths)
    {
        _patrolPaths = patrolPaths;
    }


    public PathData GetClostestPath(Vector3 postion)
    {
        PathData closest = new PathData(0, Mathf.Infinity, null, false);
        foreach (PatrolPath path in _patrolPaths)
        {
            PathData data = path.GetDataOfClostestPatrolPoint(postion);
            if (data.distance < closest.distance) { closest = data; }
        }
        return closest;
    }

}
