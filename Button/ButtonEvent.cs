using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class ButtonEvent : MonoBehaviour
{
    public static Animator playerAni;
    GameObject player;
    NavMeshAgent navPlayer;
    Button attackBtn;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAni = player.GetComponent<Animator>();
        navPlayer = player.GetComponent<NavMeshAgent>();
    }    
    public void PlayerAttackButton()
    {
        playerAni.SetInteger("aniIndex", 2);
        Debug.Log("기본 공격 버튼 클릭");
        navPlayer.enabled = true;
    }
    public void PlayerStopAttackButton()
    {
        navPlayer.enabled = true;
        playerAni.SetInteger("aniIndex", 0);
        Debug.Log("기본 공격 중지");
    }
}
