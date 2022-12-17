using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StrCollisionAttack : MonoBehaviour
{
    //플레이어의 BoomAttack 공격 물리 적용 컴포넌트
    private void OnEnable()
    {
        //플레이어가 몬스터 가격시 시간 간격 호출
        StartCoroutine("PlayerDisable");
    }
    // Trigger 물리 충돌 감시
    private void OnTriggerEnter(Collider strMonster)
    {
        //플레이어가 타격하는 대상의 태그
        if (strMonster.gameObject.CompareTag("Monster"))
        {
            strMonster.GetComponent<IDamageAttack>().DamageStrMonster(25);
        }
        if (strMonster.gameObject.CompareTag("BossMonster"))
        {
            strMonster.GetComponent<IDamageAttack>().DamageStrBossMonster(30);
        }
    }
    private IEnumerator PlayerDisable()
    {
        //0.1초 후에 오브젝트가 사라지도록 한다.
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
