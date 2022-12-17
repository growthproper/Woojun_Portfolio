using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Monster_Animator : MonoBehaviour
{
    //몬스터 애니메이터 컴포넌트

    //Animator 컴포넌트 가져오기 위한 변수 지정
    public Animator monsterAni;
    private SkinnedMeshRenderer meshRenderer;
    private Color originColor;

    //Character_Animator형 변수 지정
    GameObject player;
    float rotateSpeed;
    float moveSpeed;
    [SerializeField]
    public GameObject AttackCollisionMonster; // 플레이어 가격시 물리 기능의 오브젝트
    //AI 동작 관련 enum
    enum MonsterState
    {
        Idle,
        Move,
        Attack,
        Return
    }
    MonsterState monsterState;
    public float findDistance = 8f;
    public float attackDistance = 2f;
    Vector3 originPos;
    Quaternion originRot;
    public float moveDistance = 20f;
    NavMeshAgent navMonster;
    private void Awake()
    {
        //player변수에 Character_Animator형 스크립트 컴포넌트를 가져옴. 게임 오브젝트는 플레이어 오브젝트
        player = GameObject.FindGameObjectWithTag("Player");
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        //몬스터 애니메이터의 Animator컴포넌트 가져옴
        monsterAni = transform.GetComponent<Animator>();

        GameObject attackCollisionMonster = transform.Find("AttackCollisionMonster").gameObject;
        AttackCollisionMonster = attackCollisionMonster;
    }
    void Start()
    {
        rotateSpeed = 5.0f;
        moveSpeed = 2.0f;        
        originColor = meshRenderer.material.color;

        monsterState = MonsterState.Idle;
        originPos = transform.position;
        originRot = transform.rotation;
        navMonster = GetComponent<NavMeshAgent>();
     }

    //AttackCollisionMonster 물리 오브젝트 활성화 함수
    //몬스터가 공격 모션시 함수 호출
    public void AttackCollisionMons()
    {
        AttackCollisionMonster.SetActive(true);
    }
    public void AttackCollisionOffMons()
    {
        AttackCollisionMonster.SetActive(false);
    }
    //void MonsterMove()
    //{
    //    //플레이어의 위치에서 거리를 더하여 그 위치에 몬스터 위치를 저장
    //    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
        
    //}
    //void MonsterRotate()
    //{
    //    Vector3 tmpEnd = transform.position;
    //    tmpEnd.y = transform.position.y;
    //    Vector3 dir = tmpEnd - transform.position;
    //    Vector3 newDir = Vector3.RotateTowards(transform.forward, dir.normalized, Time.deltaTime * rotateSpeed, 0);
    //    Quaternion rotation = Quaternion.LookRotation(newDir.normalized);
    //    transform.rotation = rotation;
    //}
    private void Update()
    {
        // 현재 상태를 체크하여 해당 상태별로 정해진 기능을 수행하게 하고 싶다.
        switch (monsterState)
        {
            case MonsterState.Idle:
                Idle();
                break;
            case MonsterState.Move:
                Move();
                break;
            case MonsterState.Attack:
                Attack();
                break;
            case MonsterState.Return:
                Return();
                break;
        }
    }
    void Idle()
    {
        // 만일, 플레이어와의 거리가 액션 시작 범위 이내라면 Move 상태로 전환한다.
        if (Vector3.Distance(transform.position, player.transform.position) < findDistance)
        {
            monsterState = MonsterState.Move;
            // 이동 애니메이션으로 전환하기
            monsterAni.SetInteger("aniMonster", 1);
        }
    }
    void Move()
    {
        // 만일 현재 위치가 초기 위치에서 이동 가능 범위를 넘어간다면...
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            // 현재 상태를 Return 상태로 전환한다.
            monsterState = MonsterState.Return;           
        }

        // 만일, 플레이어와의 거리가 공격 범위 밖이라면 플레이어를 향해 이동한다.
        else if (Vector3.Distance(transform.position, player.transform.position) > attackDistance)
        {
           
            // 네비게이션으로 접근하는 최소 거리를 공격 가능 거리로 설정한다.
            navMonster.stoppingDistance = attackDistance;

            // 네비게이션 목적지를 플레이어의 위치로 설정한다.
            navMonster.destination = player.transform.position;
        }
        // 그렇지 않다면, 현재 상태를 Attack 상태로 전환한다.
        else
        {
            monsterState = MonsterState.Attack;

            // 공격 대기 애니메이션 플레이
            monsterAni.SetInteger("aniMonster", 0);

            // 네비게이션 에이전트의 이동을 멈추고 경로를 초기화한다.
            navMonster.isStopped = true;
            navMonster.ResetPath();
        }
    }
    void Attack()
    {
        // 만일, 플레이어가 공격 범위 이내에 있다면 플레이어를 공격한다.
        if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {       // 공격 애니메이션 플레이
               monsterAni.SetInteger("aniMonster", 2);
        }
        // 그렇지 않다면, 현재 상태를 Move 상태로 전환한다(재 추격 실시).
        else
        {
            monsterState = MonsterState.Move;
            // 이동 애니메이션 플레이
            monsterAni.SetInteger("aniMonster", 1);
        }
    }
    void Return()
    {
        // 만일, 초기 위치에서의 거리가 0.1f 이상이라면 초기 위치 쪽으로 이동한다.
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            // 네비게이션 목적지를 초기 저장된 위치로 설정한다.
            navMonster.destination = originPos;

            // 네비게이션으로 접근하는 최소 거리를 0으로 설정한다.
            navMonster.stoppingDistance = 0;
        }
        // 그렇지 않다면, 자신의 위치를 초기 위치로 조정하고 현재 상태를 대기 상태로 전환한다.
        else
        {
            // 네비게이션 에이전트의 이동을 멈추고 경로를 초기화한다.
            navMonster.isStopped = true;
            navMonster.ResetPath();

            // 위치 값과 회전 값을 초기 상태로 변환한다.
            transform.position = originPos;
            transform.rotation = originRot;
            monsterState = MonsterState.Idle;
            // 대기 애니메이션으로 전환하는 트랜지션을 호출한다.
            monsterAni.SetInteger("aniMonster", 0);
        }
    }
    //private void FixedUpdate()
    //{
    //    float distance = Vector3.Distance(transform.position, player.transform.position);

    //    if (distance <= 4.0f)
    //    {
    //        MonsterMove();
    //        transform.LookAt(player.transform.position);
    //        if (player.gameObject.activeSelf == false)
    //        {
    //            monsterAni.SetInteger("aniMonster", 0);
    //        }
    //        else
    //        {
    //            monsterAni.SetInteger("aniMonster", 2);
    //        }
    //    }
    //    else if (distance <= 6.0f)
    //    {
    //        MonsterRotate();
    //        monsterAni.SetInteger("aniMonster", 1);
    //        MonsterMove();

    //    }
    //    else
    //    {
    //        monsterAni.SetInteger("aniMonster", 0);
    //    }
    //}
}
