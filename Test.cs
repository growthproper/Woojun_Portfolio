using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.position - player.position;
        transform.position = player.position + dir.normalized * 10.0f;
        transform.LookAt(player.transform.position);

    }
}
