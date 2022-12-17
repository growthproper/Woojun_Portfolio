using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MonsterLife : LifeHealth
{
    //몬스터 체력 컴포넌트
    private Animator animator;
    private Monster_Animator enemyController;
    private SkinnedMeshRenderer meshRenderer;
    private Color originColor;
    public Slider monsterLifeSlider; //몬스터의 체력 게이지

    public TextMeshProUGUI attackText; //방어력
    public float attackNumber; //방어력 능력치
    public Transform attack;

    //캔버스
    public GameObject canvasObj;
    public GameObject inventoryCanvasObj;
    public Transform inventoryObj;

    //이펙트
    public GameObject HitExplosion;
    public Transform HitExplosionPos;

    //오디오
    public AudioClip dieClip;
    public AudioSource dieMonster;

    //아이템
    public GameObject swordItem;
    public GameObject portionItem;
    public GameObject shieldItem;
    GameObject swordObj;
    GameObject portionObj;
    GameObject shieldObj;
    private void Awake()
    {   
        animator = transform.GetComponent<Animator>();
        meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        originColor = meshRenderer.material.color;
        enemyController = transform.GetComponent<Monster_Animator>();

        //Boom 효과
        HitExplosion = Resources.Load<GameObject>("Effect/HitExplosion");
        HitExplosionPos = transform.GetChild(5);

        //플레이어가 공격력을 가지고 있을 때
        inventoryCanvasObj = GameObject.FindGameObjectWithTag("Canvas");
        inventoryObj = inventoryCanvasObj.transform.Find("Inventory/AbilityText/attackText");
        attack = inventoryObj.transform.GetChild(0);
        attackText = attack.GetComponent<TextMeshProUGUI>();

        //몬스터 Die 효과음
        dieMonster = GetComponent<AudioSource>();
        dieClip = Resources.Load<AudioClip>("Audio/monsterDeath");

        //아이템
        swordItem = Resources.Load<GameObject>("Item/swordItem");
        portionItem = Resources.Load<GameObject>("Item/portionItem");
        shieldItem = Resources.Load<GameObject>("Item/shieldItem");
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        canvasObj = transform.Find("Health").gameObject;
        monsterLifeSlider = canvasObj.GetComponentInChildren<Slider>();

        startingHealth = 100f;
        
        health = startingHealth;
        //플레이어 조작을 받는 컴포넌트 활성화
        enemyController.enabled = true;
    }
    //부활 기능
    public override void Rebirth(float newbirth)
    {
        base.Rebirth(newbirth);
    }
    //몬스터의 체력 감소 함수 및 감소시 메시 컬러 빨간색 변경
    public override void DamageMonster(float damage)
    {
        base.DamageMonster(damage);

        attackNumber = float.Parse(attackText.text);
        //체력이 감소되거나 피격 애니메이션이 재생되는 등의 코드를 작성
        if (health > 0)
        {
            health -= (damage + attackNumber);
            health = Mathf.Max(health, 0);
            monsterLifeSlider.value = health / startingHealth;
        }
        Debug.Log("몬스터의 체력이 damage" + damage + "만큼 감소했습니다.");
        Debug.Log("몬스터의 체력이 attackNumber " + attackNumber + "만큼 감소했습니다.");
        // 색상 변경
        StartCoroutine("EnemyOnHitColor");
    }
    public override void DamageStrMonster(float damage)
    { 
        base.DamageMonster(damage);
        Instantiate(HitExplosion, HitExplosionPos.position, Quaternion.identity);

        attackNumber = float.Parse(attackText.text);
        //체력이 감소
        if (health > 0)
        {
            health -= (damage + attackNumber);
            health = Mathf.Max(health, 0);
            monsterLifeSlider.value = health / startingHealth;
        }
        Debug.Log("몬스터의 체력이 " + damage + "만큼 감소했습니다.");
        Debug.Log("몬스터의 체력이 attackNumber " + attackNumber + "만큼 감소했습니다.");
        // 색상 변경
        StartCoroutine("EnemyOnHitColor");
    }
    public override void DieMonster()
    {
        // LifeHealth의 DieMonster() 실행(사망 적용)
        base.DieMonster();
     
        //애니메이터의 DiePlayer 트리거를 발동시켜 사망 애니메이션 재생
        animator.SetTrigger("MonsterDie");

        //사망 효과음        
        dieMonster.clip = dieClip;
        dieMonster.Play();

        StartCoroutine("MonActiveFalse");

        //플레이어 조작을 받는 컴포넌트 비활성화
        enemyController.enabled = false;
    }
    //색상변경 시간 간격 코루틴
    private IEnumerator EnemyOnHitColor()
    {
        //색을 빨간색으로 변경한 후 0.1초 후에 원래 색상으로 변경
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.color = originColor;
    }
    //몬스터가 죽고나서 5초후에 비활성
    private IEnumerator MonActiveFalse()
    {
        //DieMonster();
        yield return new WaitForSeconds(5f);
        
        //몬스터 죽으면 비활성 후 아이템 생성
        swordObj = Instantiate(swordItem, transform.position + new Vector3(1, 1, 0), Quaternion.identity);
        swordObj.AddComponent<ItemRotate>();
        //아이템 생성 후 아이템 게임 오브젝트의 (Clone) 없애기
        int swordIndex = swordObj.name.IndexOf("(Clone)");
        if (swordIndex > 0)
        {
            swordObj.name = swordObj.name.Substring(0, swordIndex);
        }
        portionObj = Instantiate(portionItem, transform.position + new Vector3(-1, 1, 0), Quaternion.identity);
        portionObj.AddComponent<ItemRotate>();
        //아이템 생성 후 아이템 게임 오브젝트의 (Clone) 없애기
        int portionIndex = portionObj.name.IndexOf("(Clone)");
        if (portionIndex > 0)
        {
            portionObj.name = portionObj.name.Substring(0, portionIndex);
        }
        //아이템 생성 후 아이템 게임 오브젝트의 (Clone) 없애기
        shieldObj = Instantiate(shieldItem, transform.position + new Vector3(-1.5f, 1, 0), Quaternion.identity);
        shieldObj.AddComponent<ItemRotate>();
        int shieldIndex = shieldObj.name.IndexOf("(Clone)");
        if (shieldIndex > 0)
        {
            shieldObj.name = shieldObj.name.Substring(0, shieldIndex);
        }
        gameObject.SetActive(false);
    }
    //몬스터의 체력바가 몬스터가 회전을 했을 때에 정면으로 고정하여 보여줌.
    private void Update()
    {
        //몬스터 체력바가 활성화 되어 있다면 체력바가 카메라를 바라보게 처리      
        canvasObj.transform.forward = Camera.main.transform.forward;
              
        //몬스터가 죽고나서 몬스터 오브젝트가 비활성화 되면 코루틴을 멈춘다.
        if (gameObject.activeSelf == false)
        {
            StopCoroutine("MonActiveFalse");
            StopCoroutine("EnemyOnHitColor");            
        }
    }
}
