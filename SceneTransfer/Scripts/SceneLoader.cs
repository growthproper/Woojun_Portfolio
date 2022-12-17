using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    //씬 이동 컴포넌트
    void Start()
    {
        Character_Animator.end = transform.position;
    }
    //씬 이동 중간 로딩 화면 호출 함수
    public void MoveScene(string _SceneName)
    {
        LoadingSceneController.LoadScene(_SceneName);
        Debug.Log(_SceneName);
    }
    //Portal에 물리 충돌하면 각각의 씬으로 이동,
    //씬 이동 후 transform.position은 Portal에서 일정거리 떨어진 위치로 초기화
    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("Portal"))
        {
            Debug.Log(other.gameObject.tag);
            Character_Animator.end = new Vector3(0, 5, -55);
            transform.position = Character_Animator.end;
            transform.rotation = Quaternion.Euler(0, 0, 0f);
            MoveScene("Dungeon");
            Debug.Log("Dungeon 위치" + transform.position);
        }
        if (other.gameObject.CompareTag("Dungeon_Portal"))
        {
            Character_Animator.end = new Vector3(-12, 0, -45);
            transform.position = Character_Animator.end;
            transform.rotation = Quaternion.Euler(0, 90, 0f);
            MoveScene("Village");
            Debug.Log("Village 위치" + transform.position);
        }
    }
}
