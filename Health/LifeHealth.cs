using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHealth : MonoBehaviour, IDamageAttack
{
   //체력에 대한 선언
    protected float startingHealth = 500f; // 시작 체력
    protected float health { get; set; } // 현재 체력
    public bool dead { get; protected set; } // 사망 상태
    // 생명체가 활성화될때 상태를 리셋
    protected virtual void OnEnable()
    {
        // 사망하지 않은 상태로 시작
        dead = false;
        // 체력을 시작 체력으로 초기화
        health = startingHealth;
    }
    // 플레이어 데미지 처리
    public virtual void DamagePlayer(float damage)
    {
        // 체력이 3 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 3 && !dead)
        {
            DiePlayer();
        }
    }
    public virtual void DamageStrPlayer(float damage)
    {
        // 체력이 25 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 25 && !dead)
        {
            DiePlayer();
        }
    }
    //몬스터 데미지 처리
    public virtual void DamageMonster(float damage)
    {
        // 체력이 10 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 10 && !dead)
        {
            DieMonster();
        }
    }
    public virtual void DamageStrMonster(float damage)
    {
        // 체력이 25 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 25 && !dead)
        {
            DieMonster();
        }
    }
    //보스 몬스터 데미지 처리
    public virtual void DamageBossMonster(float damage)
    {
        // 체력이 10 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 10 && !dead)
        {
            DieBossMonster();
        }
    }
    public virtual void DamageStrBossMonster(float damage)
    {
        // 체력이 25 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 25 && !dead)
        {
            DieBossMonster();
        }
    }
    // 플레이어 사망 처리
    public virtual void DiePlayer()
    {
        // 사망 상태를 참으로 변경
        dead = true;
    }
    //몬스터 사망 처리
    public virtual void DieMonster()
    {
        // 사망 상태를 참으로 변경
        dead = true;
    }
    //몬스터 사망 처리
    public virtual void DieBossMonster()
    {
        // 사망 상태를 참으로 변경
        dead = true;
    }
}
