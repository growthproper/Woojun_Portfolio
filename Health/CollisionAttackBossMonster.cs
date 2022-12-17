using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAttackBossMonster : MonoBehaviour
{
    //보스 몬스터의 기본 동작시 플레이어 데미지 주는 컴포넌트
    private void OnEnable()
    {
        //보스몬스터가 플레이어 가격시 시간 간격 호출
        StartCoroutine("BossAutoDisable");
    }
    //Trigger 물리 충돌 감시
    private void OnTriggerEnter(Collider boss)
    {
        //보스몬스터가 타격하는 대상의 태그
        if (boss.gameObject.CompareTag("Player"))
        {
            boss.GetComponent<IDamageAttack>().DamagePlayer(20);
        }
    }
    private IEnumerator BossAutoDisable()
    {
        //0.1초 후에 오브젝트가 사라지도록 한다.
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
