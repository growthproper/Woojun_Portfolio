using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossMonster_Animator : MonoBehaviour
{
    //보스 몬스터의 이동 및 공격 모션 및 이펙트 효과
    public Animator BossMonsterAni;
    private SkinnedMeshRenderer meshRenderer;
    private Color originColor;
    //Character_Animator형 변수 지정
    GameObject player;
    float elapsed;
    float rotateSpeed;
    float moveSpeed;
    public GameObject BossAttackEffect;
    [SerializeField]
    public GameObject AttackCollisionBossMonster; // 플레이어 가격시 물리 기능의 오브젝트
    [SerializeField]
    private GameObject bossJumpAttackCollision;
    enum BossMonsterState
    {
        Idle,
        Move,
        Attack,
        JumpingAttack,
        Return
    }
    BossMonsterState bossMonsterState;
    public float findBossDistance = 10f;
    public float attackBossDistance = 2f;
    Vector3 originBossPos;
    Quaternion originBossRot;
    public float moveBossDistance = 20f;
    NavMeshAgent navBossMonster;

    public AudioClip bossBoomClip;
    public AudioSource bossBoom;
    private void Awake()
    {
        //player변수에 Character_Animator형 스크립트 컴포넌트를 가져옴. 게임 오브젝트는 플레이어 오브젝트
        player = GameObject.FindGameObjectWithTag("Player");
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        //몬스터 애니메이터의 Animator컴포넌트 가져옴
        BossMonsterAni = transform.GetComponent<Animator>();

        GameObject attackCollisionBossMonster = transform.GetChild(2).gameObject;
        AttackCollisionBossMonster = attackCollisionBossMonster;

        GameObject bossJumpCollisionObj = transform.GetChild(4).gameObject;
        bossJumpAttackCollision = bossJumpCollisionObj;

        Transform bossEffectTr = FindBossEffectTr("BossAttackEffect", transform);
        BossAttackEffect = bossEffectTr.gameObject;
        BossAttackEffect.SetActive(false);

        bossBoom = GetComponent<AudioSource>();
        bossBoomClip = Resources.Load<AudioClip>("Audio/bossBoom");
    }
    void Start()
    {
        elapsed = 0f;
        rotateSpeed = 5.0f;
        moveSpeed = 2.0f;
        originColor = meshRenderer.material.color;
        bossMonsterState = BossMonsterState.Idle;
        originBossPos = transform.position;
        originBossRot = transform.rotation;
        navBossMonster = GetComponent<NavMeshAgent>();
    }
    //재귀호출을 사용해서 매개변수로 전달된 이름의 Transform을 검색하여 반환
    public Transform FindBossEffectTr(string _findTrname, Transform _Tr)
    {
        if (_Tr.name.Equals(_findTrname))
        {
            return _Tr;
        }
        for (int i = 0; i < _Tr.childCount; i++)
        {
            Transform findTr = FindBossEffectTr(_findTrname, _Tr.GetChild(i));
            if (findTr != null)
                return findTr;
        }
        return null;
    }
    //보스 몬스터가 기본 공격 동작시 Effect 효과
    public void BossAttackOnEffect()
    {
        BossAttackEffect.SetActive(true);
    }
    public void BossAttackOffEffect()
    {
        BossAttackEffect.SetActive(false);
    }
    //AttackCollisionBossMonster 물리 오브젝트 활성화 함수
    //몬스터가 공격 모션시 함수 호출
    public void BossAttackCollision()
    {
        AttackCollisionBossMonster.SetActive(true);
    }
    public void BossJumpAttackCollision()
    {
        bossJumpAttackCollision.SetActive(true);
        //보스 몬스터가 점프 공격시 오디오 재생
        bossBoom.clip = bossBoomClip;
        bossBoom.Play();
    }
    void Update()
    {
        // 현재 상태를 체크하여 해당 상태별로 정해진 기능을 수행하게 하고 싶다.
        switch (bossMonsterState)
        {
            case BossMonsterState.Idle:
                Idle();
                break;
            case BossMonsterState.Move:
                Move();
                break;
            case BossMonsterState.Attack:
                Attack();
                break;
          
            case BossMonsterState.Return:
                Return();
                break;
        }
    }
    void Idle()
    {
        // 만일, 플레이어와의 거리가 액션 시작 범위 이내라면 Move 상태로 전환한다.
        if (Vector3.Distance(transform.position, player.transform.position) < findBossDistance)
        {
            bossMonsterState = BossMonsterState.Move;
            // 이동 애니메이션으로 전환하기
            BossMonsterAni.SetInteger("aniBoss", 1);
        }
    }
    void Move()
    {
        // 만일 현재 위치가 초기 위치에서 이동 가능 범위를 넘어간다면...
        if (Vector3.Distance(transform.position, originBossPos) > moveBossDistance)
        {
            // 현재 상태를 Return 상태로 전환한다.
            bossMonsterState = BossMonsterState.Return;
        }

        // 만일, 플레이어와의 거리가 공격 범위 밖이라면 플레이어를 향해 이동한다.
        else if (Vector3.Distance(transform.position, player.transform.position) > attackBossDistance)
        {

            // 네비게이션으로 접근하는 최소 거리를 공격 가능 거리로 설정한다.
            navBossMonster.stoppingDistance = attackBossDistance;

            // 네비게이션 목적지를 플레이어의 위치로 설정한다.
            navBossMonster.destination = player.transform.position;
        }
        // 그렇지 않다면, 현재 상태를 Attack 상태로 전환한다.
        else
        {
            bossMonsterState = BossMonsterState.Attack;

            // 공격 대기 애니메이션 플레이
            BossMonsterAni.SetInteger("aniBoss", 0);

            // 네비게이션 에이전트의 이동을 멈추고 경로를 초기화한다.
            navBossMonster.isStopped = true;
            navBossMonster.ResetPath();
        }
    }
    void Attack()
    {
        
        // 만일, 플레이어가 공격 범위 이내에 있다면 플레이어를 공격한다.
        if (Vector3.Distance(transform.position, player.transform.position) < attackBossDistance)
        {       // 공격 애니메이션 플레이
            elapsed += Time.deltaTime;
            
            BossMonsterAni.SetInteger("aniBoss", 2);
            if(elapsed >= 7f)
            {
                //elapsed -= 0.1f;
                
                BossMonsterAni.SetTrigger("JumpingAttack");
                elapsed = 0f;
            }
        }
        // 그렇지 않다면, 현재 상태를 Move 상태로 전환한다(재 추격 실시).
        else
        {
            bossMonsterState = BossMonsterState.Move;
            // 이동 애니메이션 플레이
            BossMonsterAni.SetInteger("aniBoss", 1);
        }
    }
    void Return()
    {
        // 만일, 초기 위치에서의 거리가 0.1f 이상이라면 초기 위치 쪽으로 이동한다.
        if (Vector3.Distance(transform.position, originBossPos) > 0.1f)
        {
            // 네비게이션 목적지를 초기 저장된 위치로 설정한다.
            navBossMonster.destination = originBossPos;

            // 네비게이션으로 접근하는 최소 거리를 0으로 설정한다.
            navBossMonster.stoppingDistance = 0;
        }
        // 그렇지 않다면, 자신의 위치를 초기 위치로 조정하고 현재 상태를 대기 상태로 전환한다.
        else
        {
            // 네비게이션 에이전트의 이동을 멈추고 경로를 초기화한다.
            navBossMonster.isStopped = true;
            navBossMonster.ResetPath();

            // 위치 값과 회전 값을 초기 상태로 변환한다.
            transform.position = originBossPos;
            transform.rotation = originBossRot;
            bossMonsterState = BossMonsterState.Idle;
            // 대기 애니메이션으로 전환하는 트랜지션을 호출한다.
            BossMonsterAni.SetInteger("aniBoss", 0);
        }
    }
}
