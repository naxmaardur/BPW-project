
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField]
    Vector3[] _path;
    [SerializeField]
    bool _loop;
    public Vector3[] Path { get { return _path; } }
    public bool Loop { get { return _loop; } }


    public PathData GetDataOfClostestPatrolPoint(Vector3 position)
    {
        int index = 0;
        float distance = Mathf.Infinity;
        for(int i =0; i < _path.Length; i++)
        {
            if(Vector3.Distance(position,_path[i]) < distance)
            {
                distance = Vector3.Distance(position, _path[i]);
                index = i;
            }
        }

        return new PathData(index,distance, _path,_loop);
    }



    private void OnDrawGizmos()
    {
        
        for (int i = 0; i < _path.Length; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(_path[i], new Vector3(1, 1, 1));
            Gizmos.color = Color.red;
            if (i < _path.Length - 1)
            {
                Gizmos.DrawLine(_path[i], _path[i + 1]);
            }
            else if (_loop)
            {
                Gizmos.DrawLine(_path[i], _path[0]);
            }
        }
    }
}

public struct PathData
{
    public int index;
    public float distance;
    public Vector3[] path;
    public bool loop;

    public PathData(int index, float distance, Vector3[] path, bool loop)
    {
        this.index = index;
        this.distance = distance;
        this.path = path;
        this.loop = loop;
    }
}
