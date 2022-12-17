using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    //추적할 대상
    public Transform target;
    //public Character_Animator player;
    //카메라와의 거리   
    public float dist = 20f;
    //카메라의 높이 
    //public float height = 1f;

    private Vector3 offset;
    private Transform tr;
   
    // Use this for initialization
    
    void Start()
    {
        //offset = player.transform.position;
        offset = transform.position - target.transform.position;
    }

    //Update is called once per frame
    void Update()
    {

        //카메라 위치 설정
        //tr.position = transform.position - (1 * Vector3.back * dist) + (Vector3.up * height);
        //tr.LookAt(tr.position);
    }
    void LateUpdate()
    {
        transform.position = target.transform.position + offset;

        //Vector3 delta = player.transform.position - offset;
        //transform.position += delta;
        //offset = player.transform.position;
    }
}
