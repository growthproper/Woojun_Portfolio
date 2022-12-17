using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Character_Animator : MonoBehaviour
{
    //플레이어의 애니메이터 컴포넌트, 이펙트
    public NavMeshAgent navCharacter;
    public static Animator aniPlayer;
    Monster_Animator monster;
    float moveSpeed; //앞뒤 움직임
    float rotateSpeed; // 좌우 회전속도
    float rotates;
    float move;
    public static Vector3 end;
    public GameObject WeaponEffect;

    Button attackBtn;
    [SerializeField]
    private GameObject attackCollisionPlayer; // 몬스터 가격시 물리 기능의 오브젝트
    [SerializeField]
    private GameObject StrAttackCollision;

    public AudioClip boomAttackClip;
    public AudioSource boomAttack;
    public AudioClip attackClip;
    public AudioSource hitAttack;

    void Start()
    {        
        
        moveSpeed = 3.0f;
        rotateSpeed = 100f;
        end = transform.position;
        aniPlayer = GetComponent<Animator>();
        Transform effectTr = FindEffectTr("WeaponEffect", transform);
        WeaponEffect = effectTr.gameObject;
        WeaponEffect.SetActive(false);

        boomAttack = GetComponent<AudioSource>();
        hitAttack = GetComponent<AudioSource>();
        boomAttackClip = Resources.Load<AudioClip>("Audio/boomHit");
        attackClip = Resources.Load<AudioClip>("Audio/swordEffect");

    }

    //재귀호출을 사용해서 매개변수로 전달된 이름의 Transform을 검색하여 반환
    public Transform FindEffectTr(string _findTrname, Transform _Tr)
    {
        if (_Tr.name.Equals(_findTrname))
        {
            return _Tr;
        }
        for (int i = 0; i < _Tr.childCount; i++)
        {
            Transform findTr = FindEffectTr(_findTrname, _Tr.GetChild(i));
            if (findTr != null)
                return findTr;
        }
        return null;
    }
    void Rotate()
    {
        //회전할 수치 계산
        float turn = rotates * rotateSpeed * Time.deltaTime;
        //자신의 회전값에 쿼터니언 오일러각을 곱해서 회전
        transform.rotation *= Quaternion.Euler(0, turn, 0f);
    }
    void Move()
    {
        //이동할 거리 계산
        Vector3 moveDistance = move * transform.forward * moveSpeed * Time.deltaTime;
        //이동할 거리를 현재 위치에 계속 더해줘서 이동
        transform.position += moveDistance;
    }
    //몬스터 가격할 때만 물리기능의 오브젝트를 활성화
    public void AttackCollisionPlayer()
    {
        attackCollisionPlayer.SetActive(true);
    }
    public void StrCollision()
    {
        StrAttackCollision.SetActive(true);

        //boom효과음        
        boomAttack.clip = boomAttackClip;
        boomAttack.Play();
    }
    public void WeaponOnEffect()
    {
        WeaponEffect.SetActive(true);

        //player 검 효과음
        hitAttack.clip = attackClip;
        hitAttack.Play();
    }
    public void WeaponOffEffect()
    {
        WeaponEffect.SetActive(false);
    }

    void Update()
    {
        
        //마우스 픽에 의한 마우스로 선택한 게임 공간 상의 좌표를 출력
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hits;
            if (Physics.Raycast(ray, out hits, Mathf.Infinity))
            {
                navCharacter.destination = hits.point;
                end = hits.point;
                Debug.Log("end, destination : " + hits.point);
            }
            aniPlayer.SetInteger("aniIndex", 1);
        }

        if (navCharacter.destination == transform.position)
        {
            //navCharacter.destination = transform.position;
            aniPlayer.SetInteger("aniIndex", 0);
        }
        //else if (navCharacter.destination != transform.position)
        //{
        //    aniPlayer.SetInteger("aniIndex", 1);
        //}
        //키보드 입력에 따른 캐릭터 이동하면서 Run

        //    if (navCharacter.destination == transform.position)
        //{
        //    transform.position = navCharacter.destination;
        //    aniPlayer.SetInteger("aniIndex", 0);
        //}
        //else if (navCharacter.destination != transform.position)
        //{
        //    aniPlayer.SetInteger("aniIndex", 1);
        //    if (Input.GetKeyDown(KeyCode.Z))
        //    {
        //        //캐릭터가 이동 목표 지점으로 가는 중간에 공격 모션이 진행되면 그 위치에서 멈춤
        //        navCharacter.destination = transform.position;
        //        aniPlayer.SetInteger("aniIndex", 2);
        //    }
        //}
        ////키보드 입력에 따른 캐릭터 공격1 모션
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    aniPlayer.SetInteger("aniIndex", 2);
        //}
        //if (Input.GetKeyUp(KeyCode.Z))
        //{
        //    aniPlayer.SetInteger("aniIndex", 0);


        //수평축은 회전값 할당
        //rotates = Input.GetAxis("garo");
        //가로축은 이동값 할당
        //move = Input.GetAxis("sero");
        //Rotate();
        //Move();

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    aniPlayer.SetInteger("aniIndex", 1);
        //}
        //if (Input.GetKeyUp(KeyCode.W))
        //{
        //    aniPlayer.SetInteger("aniIndex", 0);
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    aniPlayer.SetInteger("aniIndex", 5);
        //}
        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    aniPlayer.SetInteger("aniIndex", 0);
        //}

    }
}
   



