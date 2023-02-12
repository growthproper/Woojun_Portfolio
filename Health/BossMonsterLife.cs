using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BossMonsterLife : LifeHealth
{
    //보스 몬스터가 데미지 입었을 때의 체력과 사망, 오디오, 이펙트 처리 컴포넌트
    public TextMeshProUGUI bossHP;
    private Animator animatorBoss;
    private BossMonster_Animator bossMonsterController;
    private SkinnedMeshRenderer meshRenderer;
    private Color originColor;
    public Slider bossMonsterLifeSlider; //몬스터의 체력 게이지

    public TextMeshProUGUI attackText; //방어력
    public float attackNumber; //방어력 능력치
    public Transform attack;
    public GameObject inventoryCanvasObj;
    public Transform inventoryObj;

    GameObject bossHealthObj;
    GameObject canvasObj;
    GameObject player;
    GameObject hpText;

    public GameObject HitBossExplosion;
    public Transform HitBossExplosionPos;

    public AudioClip dieBossClip;
    public AudioSource dieBossMonster;

    public GameObject strongSword;
    public GameObject strongShield;
    GameObject strongSwordObj;
    GameObject strongShieldObj;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvasObj = GameObject.FindGameObjectWithTag("BossMonsterHealthSlider");
        hpText = canvasObj.transform.GetChild(0).GetChild(2).gameObject;
        bossHP = hpText.GetComponent<TextMeshProUGUI>();
        
        //공격 받을시 메시 색상 변경 처리
        animatorBoss = transform.GetComponent<Animator>();
        meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        originColor = meshRenderer.material.color;
        bossMonsterController = transform.GetComponent<BossMonster_Animator>();

        //공격 받을시 이펙트 처리
        HitBossExplosion = Resources.Load<GameObject>("Effect/HitExplosion");
        HitBossExplosionPos = transform.GetChild(3);        

        //사망하면 아이템 활성
        inventoryCanvasObj = GameObject.FindGameObjectWithTag("Canvas");
        inventoryObj = inventoryCanvasObj.transform.Find("Inventory/AbilityText/attackText");
        attack = inventoryObj.transform.GetChild(0);
        attackText = attack.GetComponent<TextMeshProUGUI>();

        //사망시 오디오 처리
        dieBossMonster = GetComponent<AudioSource>();
        dieBossClip = Resources.Load<AudioClip>("Audio/bossMonDeath");

        //아이템
        strongShield = Resources.Load<GameObject>("Item/strongShield");
        strongSword = Resources.Load<GameObject>("Item/strongSword");
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        bossHealthObj = GameObject.Find("Canvas").gameObject;
        bossMonsterLifeSlider = bossHealthObj.GetComponentInChildren<Slider>();
        startingHealth = 350f;      
        health = startingHealth;
        //플레이어 조작을 받는 컴포넌트 활성화
        bossMonsterController.enabled = true;
    }    
    public override void DamageBossMonster(float damage)
    {
        base.DamageBossMonster(damage);

        attackNumber = float.Parse(attackText.text);
        //체력이 감소되거나 피격 애니메이션이 재생되는 등의 코드를 작성
        Debug.Log("보스 몬스터의 체력이 " + damage + "만큼 감소했습니다.");
        if (health > 0)
        {
            health -= (damage + attackNumber);
            health = Mathf.Max(health, 0);
            bossMonsterLifeSlider.value = health / startingHealth;
            bossHP.text = $"{health}/{startingHealth}";
        }
        // 색상 변경
        StartCoroutine("BossOnHitColor");        
    }
    public override void DamageStrBossMonster(float damage)
    {
        base.DamageStrBossMonster(damage);        
        Instantiate(HitBossExplosion, HitBossExplosionPos.position, Quaternion.identity);
        
        attackNumber = float.Parse(attackText.text);
        
        Debug.Log("보스 몬스터의 체력이 " + damage + "만큼 감소했습니다.");
        if (health > 0)
        {
            health -= (damage + attackNumber);
            health = Mathf.Max(health, 0);
            bossMonsterLifeSlider.value = health / startingHealth;
            bossHP.text = $"{health}/{startingHealth}";
        }
        // 색상 변경
        StartCoroutine("BossOnHitColor");
        
    }
    public override void DieBossMonster()
    {
        // LifeHealth의 DieMonster() 실행(사망 적용)
        base.DieBossMonster();

        //애니메이터의 DiePlayer 트리거를 발동시켜 사망 애니메이션 재생
        animatorBoss.SetTrigger("BossDie");
        //사망 효과음
        dieBossMonster.clip = dieBossClip;
        dieBossMonster.Play();
        StartCoroutine("BossActiveFalse");

        //플레이어 조작을 받는 컴포넌트 비활성화
        bossMonsterController.enabled = false;
    }
    //색상변경 시간 간격 코루틴
    private IEnumerator BossOnHitColor()
    {
        //색을 빨간색으로 변경한 후 0.1초 후에 원래 색상으로 변경
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.color = originColor;
    }
    //몬스터가 죽고나서 5초후에 비활성
    private IEnumerator BossActiveFalse()
    {
        //DieMonster();
        yield return new WaitForSeconds(4f);

        //보스 몬스터 죽으면 비활성 후 아이템 생성
        strongSwordObj = Instantiate(strongSword, transform.position + new Vector3(1, 1, 0), Quaternion.identity);
        strongSwordObj.AddComponent<ItemRotate>();
        //아이템 생성 후 아이템 게임 오브젝트의 (Clone) 없애기
        int strongSwordIndex = strongSwordObj.name.IndexOf("(Clone)");
        if (strongSwordIndex > 0)
        {
            strongSwordObj.name = strongSwordObj.name.Substring(0, strongSwordIndex);
        }
        strongShieldObj = Instantiate(strongShield, transform.position + new Vector3(-1, 1, 0), Quaternion.identity);
        strongShieldObj.AddComponent<ItemRotate>();
        //아이템 생성 후 아이템 게임 오브젝트의 (Clone) 없애기
        int strongShieldIndex = strongShieldObj.name.IndexOf("(Clone)");
        if (strongShieldIndex > 0)
        {
            strongShieldObj.name = strongShieldObj.name.Substring(0, strongShieldIndex);
        }
        gameObject.SetActive(false);
        canvasObj.SetActive(false);
        hpText.SetActive(false);

    }
    //몬스터의 체력바가 몬스터가 회전을 했을 때에 정면으로 고정하여 보여줌.
    private void Update()
    {
        //몬스터가 죽고나서 몬스터 오브젝트가 비활성화 되면 코루틴을 멈춘다.
        if (gameObject.activeSelf == false)
        {
            StopCoroutine("BossActiveFalse");
            StopCoroutine("BossOnHitColor");

        }
    }
}
