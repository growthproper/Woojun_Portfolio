using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingleTon<ResourceManager>
{
    //리소스를 로드하는 리소스 로드 매니저
    //GameObject형 List monsterList 선언
    List<GameObject> monsterList;
    // Monster 프리팹이 있는 경로의 폴더 이름
    string monsterFolder = "Monster";
    //Camera
    List<GameObject> CameraList;
    string cameraFolder = "Prefab";

    //Effect
    List<GameObject> EffectList;
    string effectFolder = "Effect";
    public void LoadMonster()
    {
        monsterList = new List<GameObject>();
        GameObject[] monsFolder = Resources.LoadAll<GameObject>(monsterFolder);
        //monsFolder 변수 안에 차례대로 검색하여 monsterList에 추가함.
        foreach (GameObject one in monsFolder)
        {
            monsterList.Add(one);
        }
    }
    //monsterList에 저장된 GameObject를 이름으로 검색하는 함수
    public GameObject GetMonster(string _name)
    {
        return monsterList.Find(o => (o.gameObject.name.Equals(_name)));
    }
    public GameObject GetBossMonster()
    {   
        return Resources.Load<GameObject>("Boss/Boss");
    }
   
    //카메라 로드 함수
    public void LoadCamera()
    {
        CameraList = new List<GameObject>();
        GameObject[] camerasFolder = Resources.LoadAll<GameObject>(cameraFolder);
        //canvasesFolder 변수 안에 차례대로 검색하여 canvasList 추가함.
        foreach (GameObject one in camerasFolder)
        {
            CameraList.Add(one);
        }
    }
    //CameraList 저장된 GameObject를 이름으로 검색하는 함수
    public GameObject GetCamera(string _name)
    {
        return CameraList.Find(o => (o.gameObject.name.Equals(_name)));
    }

    //이펙트
    public void LoadEffect()
    {
        EffectList = new List<GameObject>();
        GameObject[] effect = Resources.LoadAll<GameObject>(effectFolder);
        //monsFolder 변수 안에 차례대로 검색하여 monsterList에 추가함.
        foreach (GameObject one in effect)
        {
            EffectList.Add(one);
        }
    }
    //EffectList에 저장된 GameObject를 이름으로 검색하는 함수
    public GameObject GetEffect(string _name)
    {
        return EffectList.Find(o => (o.gameObject.name.Equals(_name)));
    }
}
