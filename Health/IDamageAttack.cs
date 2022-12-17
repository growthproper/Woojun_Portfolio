using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAttack
{
    //플레이어 데미지
    void DamagePlayer(float damage);
    void DamageStrPlayer(float damage);
    //몬스터 데미지
    void DamageMonster(float damage);
    void DamageStrMonster(float damage); //이펙트 효과
    //보스몬스터 데미지
    void DamageBossMonster(float damage);
    void DamageStrBossMonster(float damage); //이펙트 효과

    //부활 기능
    void Rebirth(float newBirth);
    //플레이어 Die
    void DiePlayer();
    //몬스터 Die
    void DieMonster();
    //보스몬스터 Die
    void DieBossMonster();
}
