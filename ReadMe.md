# Woojun_Portfolio
# 제작 기간
  * 4개월
  * 2022년 09월 ~ 2022년 12월
# 설명
## 클래스 구성
  * ButtonEvent : 공격 버튼을 누르면 공격 애니메이션 모션 동작
  * CameraController : 시네머신 카메라를 좌우 방향키를 눌러 시점을 이동할 수 있는 기능
  * BossMonsterLife : 보스 몬스터의 체력, 사망, 이펙트, 데미지의 기능 구현
  * MonsterLife : 몬스터의 체력, 사망, 이펙트, 데미지의 기능 구현
  * PlayerLife : 플레이어의 체력, 이펙트, 데미지의 기능 구현
  * IDamageAttack : Life에 관한 인터페이스
  * LifeHealth : Life에 관한 기반 클래스
  * CollisionAttackBossMonster, CollisionAttackMonster, CollisionAttackPlayer, StrCollisionAttack, StrCollisionBossAttack : 공격 Trigger 물리 충돌 감지하여 데미지 함수 호출
  * getItemBox, getItemBoxSlot, setItemBox, setItemBoxSlot : 인벤토리 기능 구현
  * ItemRotate : 아이템 생성시 아이템의 회전
  * InstanceManager : 캐릭터, 카메라, 캔버스의 씬 생성 매니저
  * ResourceManager : 캐릭터 등의 생성시 리소스 로드 매니저
  * SetManager : FreeLook 카메라를 씬이 변경될때 플레이어를 바라보게 하는 기능
  * BossMonster_Animator, Character_Animator, Monster_Animator : 캐릭터의 애니메이터 모션 동작
  * DonDestroyObject : Canvas, Player가 씬 이동시 파괴되지않고 이동하는 기능
  * LoadingSceneController : 씬 이동시 로딩화면 기능
  * SceneLoader : 각 씬의 포탈에 Trigger 물리 충격시 씬 이동하는 기능
  
  ### 몬스터와의 전투 씬 : 이펙트 효과, 체력, 검 이펙트  
  ![포폴 녹화22-12-19-15-09-08](https://user-images.githubusercontent.com/89292360/208359520-ace7c7f1-408d-49e7-bf5e-0b5e3aa5d052.gif)

  ### 씬 이동 : 로딩화면 구현
  ![포폴 녹화22-12-19-15-21-38](https://user-images.githubusercontent.com/89292360/208360880-3033c1d2-fc92-4fe9-8c4e-85be4b3262ba.gif)
  
  ### 인벤토리 : 아이템 장착시 공격력과 방어력 증가
  ![포폴 녹화22-12-19-15-18-36](https://user-images.githubusercontent.com/89292360/208360532-5b193b23-ad44-4f4f-bcf5-f03e0edfa095.gif)



