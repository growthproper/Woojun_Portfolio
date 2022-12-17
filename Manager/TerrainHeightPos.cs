using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainHeightPos
{
    //지형 높이에 상관없이 배치해주는 함수
    static public Vector3? GetTerrainHeightPos(Vector3 _origin)
    {
        Vector3 origin = _origin;
        origin.y += 200f;
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Monster");
        layerMask = ~layerMask;
        if(Physics.Raycast(origin, Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            return hit.point;
        }
        return null;
    }
}
