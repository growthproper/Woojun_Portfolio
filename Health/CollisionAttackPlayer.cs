using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollisionAttackPlayer : MonoBehaviour
{
  //플레이어 공격 동작시 몬스터와 보스몬스터 데미지 주는 컴포넌트
    private void OnEnable()
    {
        //플레이어가 몬스터 가격시 시간 간격 호출
        StartCoroutine("PlayerAutoDisable");
    }
    // Trigger 물리 충돌 감시
    private void OnTriggerEnter(Collider monster)
    {
        //플레이어가 타격하는 대상의 태그
        if (monster.gameObject.CompareTag("Monster"))
        {
            monster.GetComponent<IDamageAttack>().DamageMonster(10);
        }
        else if (monster.gameObject.CompareTag("BossMonster"))
        {
            monster.GetComponent<IDamageAttack>().DamageBossMonster(15);
        }
        
    }
   
    private IEnumerator PlayerAutoDisable()
    {
        //0.1초 후에 오브젝트가 사라지도록 한다.
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }    
}
