using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAttackMonster : MonoBehaviour
{
    //몬스터가 플레이어에게 데미지 주는 컴포넌트
    private void OnEnable()
    {
        //몬스터가 플레이어 가격시 시간 간격 호출
        //StartCoroutine("EnemyAutoDisable");
    }
    //Trigger 물리 충돌 감시
    private void OnTriggerEnter(Collider player)
    {
        //몬스터가 타격하는 대상의 태그
        if (player.CompareTag("Player"))
        {
            player.GetComponent<IDamageAttack>().DamagePlayer(10);
        }
    }   
}
