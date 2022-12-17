using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StrCollisionBossAttack : MonoBehaviour
{
    //보스몬스터의 점프 공격 동작시 플레이어에 데미지 주는 컴포넌트
    private void OnEnable()
    {
        //플레이어가 몬스터 가격시 시간 간격 호출
        StartCoroutine("BossDisable");

    }
    // Trigger 물리 충돌 감시
    private void OnTriggerEnter(Collider other)
    {
        //플레이어가 타격하는 대상의 태그
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<IDamageAttack>().DamageStrPlayer(25);
        }
    }

    private IEnumerator BossDisable()
    {
        //0.1초 후에 오브젝트가 사라지도록 한다.
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
