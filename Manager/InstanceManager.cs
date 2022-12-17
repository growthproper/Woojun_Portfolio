using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//카메라 생성(씬 이동 및 씬 활성화 될때)
//ResourceManager에서 Camera 로드하여 생성
public class InstanceManager : SingleTon<InstanceManager>
{
    //카메라, 몬스터, 캔버스 생성하는 매니저
    //캐릭터형 컴포넌트 리스트 characterList 생성, ResourceManager 싱글톤 인스턴스의 LoadMonster()함수 호출
    public void Initialize()
    {
        ResourceManager.instance.LoadCamera();
        ResourceManager.instance.LoadMonster();
        
    }
    //Monster 캐릭터 생성 함수 CreateMonster 함수 선언
    public void CreateMonster(string _name)
    {
        //ResourceManager 싱글톤 인스턴스의 GetMonster함수에서 Monster 게임오브젝트의 이름을 찾아 가져온다.
        GameObject MonsterObj = ResourceManager.instance.GetMonster(_name);
        
        //MonsterObj 변수에 가져온 게임 오브젝트가 저장되어 있다면 if문 실행
        if (MonsterObj != null)
        {
            //씬에 생성
            for(int i = 0; i < 5; i++)
            {
                GameObject createObj = GameObject.Instantiate<GameObject>(MonsterObj);
                createObj.transform.position = new Vector3(Random.Range(-3, 11), Random.Range(0, 0), Random.Range(-44, -30));
                Monster_Animator monsterAniScript = createObj.AddComponent<Monster_Animator>();
                MonsterLife monsterLifeScript = createObj.AddComponent<MonsterLife>();
                Debug.Log("몬스터");
            }
        }
    }
    public void CreateBossMonster()
    {
        GameObject bossMonsterObj = ResourceManager.instance.GetBossMonster();
        bossMonsterObj.transform.position = new Vector3(3, -5, 73);
        bossMonsterObj.transform.rotation = Quaternion.Euler(0, 180, 0);
        GameObject createBossMonsterObj = GameObject.Instantiate<GameObject>(bossMonsterObj);
        BossMonster_Animator bossMonsterAniScript = createBossMonsterObj.AddComponent<BossMonster_Animator>();
        BossMonsterLife bossMonsterLifeScript = createBossMonsterObj.AddComponent<BossMonsterLife>();
    }
    public void CreateCamera(string _name)
    {
        //ResourceManager 싱글톤 인스턴스의 GetCanvas함수에서 Canvas 게임오브젝트의 이름을 찾아 가져온다.
        GameObject CameraObj = ResourceManager.instance.GetCamera(_name);

        if (CameraObj != null)
        {
            //CanvasObj 변수에 가져온 게임 오브젝트가 저장되어 있다면 if문 실행
            GameObject.Instantiate<GameObject>(CameraObj);
        }
    }
}
