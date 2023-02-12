using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
public class PlayerLife : LifeHealth
{  
    //플레이어의 체력 컴포넌트
    public TextMeshProUGUI Hp; //플레이어 체력 텍스트
    public TextMeshProUGUI sheildText; //방어력
    public float shieldNumber; //방어력 능력치

    public Slider healthSlider; //플레이어의 체력 게이지
    
    private Character_Animator playerController; //플레이어 콘트롤러 클래스
    private SkinnedMeshRenderer meshRenderer; // 플레이어의 스킨드메시 렌더러 컴포넌트
    private Color originColor; // 컬러

    //효과
    public GameObject HitBossMonExplosion;
    public Transform HitBossMonExplosionPos;

    //효과음
    public AudioClip getHitClip;
    public AudioSource getHit;

    //아이템
    public getItemBox portion;
    public string portionCountText;
    public Button healthBtn;
    public TextMeshProUGUI healthBtnCount;
    public Transform healthRestoreCountObj;
    public GameObject getItemBoxObj;
    public GameObject healthBtnObj;
    public int portionCount;

    //오디오
    public AudioClip reHealthClip;
    public AudioSource reHealth;
    private void Awake()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originColor = meshRenderer.material.color;
        playerController = GetComponent<Character_Animator>();

        HitBossMonExplosion = Resources.Load<GameObject>("Effect/BossHitExplosion");
        HitBossMonExplosionPos = transform.GetChild(7);

        Transform sheildTexObj = GameObject.Find("Canvas/Inventory/AbilityText").transform.GetChild(1).GetChild(0);
        sheildText = sheildTexObj.GetComponent<TextMeshProUGUI>();

        getHit = GetComponent<AudioSource>();
        getHitClip = Resources.Load<AudioClip>("Audio/hit");
        getHit.clip = getHitClip;

        getItemBoxObj = GameObject.Find("Canvas/Inventory/GetItemBox");
        healthBtnObj = GameObject.Find("Canvas/HealthRestore");
        healthRestoreCountObj = healthBtnObj.transform.GetChild(0);
        healthBtn = healthBtnObj.GetComponent<Button>();
        portion = getItemBoxObj.GetComponent<getItemBox>();
        healthBtnCount = healthRestoreCountObj.GetComponent<TextMeshProUGUI>();
        portionCount = 0;

        //아이템 습득했을 때 효과음
        reHealth = GetComponent<AudioSource>();
        reHealthClip = Resources.Load<AudioClip>("Audio/hpRehealth");
    }
    void Start()
    {
        //버튼 누를 때 함수 호출
        healthBtn.onClick.AddListener(AddHealth);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        health = startingHealth;        
        //플레이어 조작을 받는 컴포넌트 활성화
        playerController.enabled = true;
    }    
    // 데미지 처리
    public override void DamagePlayer(float damage)
    {
        // LifeHealth의 DamagePlayer() 실행(데미지 적용)
        base.DamagePlayer(damage);

        //히트 효과음
        getHit.clip = getHitClip;
        getHit.Play();

        shieldNumber = float.Parse(sheildText.text);
        //갱신된 체력을 체력 슬라이더에 반영
        if (health > 0)
        {           
            health -= (damage - shieldNumber);            
            health = Mathf.Max(health, 0); // health와 0 중에 더 큰값을 반환
            healthSlider.value = health / startingHealth;
            Hp.text = $"{health}/{startingHealth}"; // health 게이지가 감소할 때마다 텍스트도 감소
            if (health > startingHealth)
            {
                health = startingHealth;
                Hp.text = $"{health}/{startingHealth}";
            }
        }
        Debug.Log("플레이어의 체력이 damage" + damage + "만큼 감소했습니다.");
        Debug.Log("플레이어의 체력이 shieldNumber" + shieldNumber + "만큼 증가했습니다.");
        //타격을 받을 때마다 코루틴 시간설정 간격으로 색상 변경
        StartCoroutine("PlayerOnHitColor");
    }
    public override void DamageStrPlayer(float damage)
    {
        // LifeHealth의 DamagePlayer() 실행(데미지 적용)
        base.DamageStrPlayer(damage);

        //히트 효과음
        getHit.clip = getHitClip;
        getHit.Play();
        //데미지 입을 때 Effect 생성
        Instantiate(HitBossMonExplosion, HitBossMonExplosionPos.position, Quaternion.identity);
        shieldNumber = float.Parse(sheildText.text);
        Debug.Log("플레이어의 체력이 " + damage + "만큼 감소했습니다.");
        //갱신된 체력을 체력 슬라이더에 반영
        if (health > 0)
        {
            health -= (damage - shieldNumber);
            health = Mathf.Max(health, 0); // health와 0 중에 더 큰값을 반환
            healthSlider.value = health / startingHealth;
            Hp.text = $"{health}/{startingHealth}"; // health 게이지가 감소할 때마다 텍스트도 감소
            if (health > startingHealth)
            {
                health = startingHealth;
                Hp.text = $"{health}/{startingHealth}";
            }
        }
        //타격을 받을 때마다 코루틴 시간설정 간격으로 색상 변경
        StartCoroutine("PlayerOnHitColor");
    }
    //플레이어가 타격 받았을 때 색상 변경되는 시간 간격
    private IEnumerator PlayerOnHitColor()
    {
        //메시의 색을 빨간색으로 변경한 후 0.1초 후에 원래 색상으로 변경
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.color = originColor;
    }
    //플레이어가 죽고나서 비활성화 되는 시간 간격
    private IEnumerator PlayerSetActiveFalse()
    {
        DiePlayer();
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false); //죽고 나서 5초후에 플레이어 오브젝트를 비활성화
    }
    // 사망 처리
    public override void DiePlayer()
    {
        // LifeHealth의 DiePlayer() 실행(사망 적용)
        base.DiePlayer();
        //체력 슬라이더 비활성화
        //healthSlider.gameObject.SetActive(false);

        //애니메이터의 PlayerDie 트리거를 발동시켜 사망 애니메이션 재생
        Character_Animator.aniPlayer.SetTrigger("PlayerDie");
        StartCoroutine("PlayerSetActiveFalse");

        //플레이어 조작을 받는 컴포넌트 비활성화
        playerController.enabled = false;
    }
    //체력 아이템 버튼 눌렀을 때의 반응
    public void AddHealth()
    {
        for (int i = 0; i < portion.getItemBoxSlotList.Count; i++)
        {
            if (portion.getItemBoxSlotList[i].itemCount.text != "0" && portion.getItemBoxSlotList[i].emptyItem.gameObject.name == "portionItem")
            {
                reHealth.clip = reHealthClip;
                reHealth.Play();
                portionCount = int.Parse(portion.getItemBoxSlotList[i].itemCount.text);
                portionCount--;
                health += 20;
                if (health >= startingHealth)
                {
                    health = startingHealth;
                }
                Hp.text = $"{health}/{startingHealth}";
                healthSlider.value = health / startingHealth;
                portion.getItemBoxSlotList[i].itemCount.text = portionCount.ToString();

                if (portion.getItemBoxSlotList[i].itemCount.text == "0")
                {
                    healthBtnCount.text = portion.getItemBoxSlotList[i].itemCount.text;
                    portion.getItemBoxSlotList[i].emptyItem.gameObject.name = "Empty";
                    portion.getItemBoxSlotList[i].itemCount.gameObject.name = "EmptyCount";
                    portion.getItemBoxSlotList[i].emptyItem.gameObject.SetActive(false);
                    portion.getItemBoxSlotList[i].itemCount.gameObject.SetActive(false);
                    portion.getItemBoxSlotList[i].isFilledSlot = false;
                    healthBtnCount.text = "0";
                }
            } 
            healthBtnCount.text = portion.getItemBoxSlotList[i].itemCount.text;
        }
    }  
    void Update()
    {
        for (int i = 0; i < portion.getItemBoxSlotList.Count; i++)
        {
            if (portion.getItemBoxSlotList[i].itemCount.gameObject.name == "portionItem_Count")
            {
                portionCount = int.Parse(portion.getItemBoxSlotList[i].itemCount.text);               
            }
        }
        healthBtnCount.text = portionCount.ToString();
    }
}
