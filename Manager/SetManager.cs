using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;
public class SetManager : MonoBehaviour
{
    GameObject PlayerObj; //플레이어 오브젝트
    GameObject CameraObj; //카메라 오브젝트
    Transform PlayerPos; //플레이어 오브젝트의 위치
    GameObject ButtonImageObj; //버튼 UI 오브젝트
    void Start()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        CameraObj = GetComponent<GameObject>();
        PlayerPos = PlayerObj.transform;


        
        InstanceManager.instance.Initialize();
        
       if(SceneManager.GetActiveScene().name == "Dungeon")
        {
            InstanceManager.instance.CreateMonster("Monster");
            InstanceManager.instance.CreateBossMonster();
        }

        //플레이어 오브젝트에 카메라 붙이기
        //플레이어 게임 오브젝트가 씬에 없다면 Player 태그의 게임 오브젝트를 찾아 PlayerObj 변수에 저장
        if (PlayerObj != null) // 플레이어 게임 오브젝트가 씬에 있다면 카메라 프리팹을 생성하여 freeLookCamera의 Follow, LookAt을 플레이어로 지정
        {
            //if (SceneManager.GetActiveScene().name == "Dungeon")
            //{
            //    InstanceManager.instance.CreateMonster("Monster");
            //}
            InstanceManager.instance.CreateCamera("Camera");
            CameraController.freeLookCameraComponent.Follow = PlayerPos;
            CameraController.freeLookCameraComponent.LookAt = PlayerPos;
        }
    }
}
