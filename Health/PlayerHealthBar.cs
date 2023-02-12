using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Hp; //플레이어 체력 텍스트
    [SerializeField]
    private Slider slider; //플레이어의 체력 게이지
   
    private float maxHP = 100;
    private float currentHP;
    private float indamage = 10;
    
    TextMeshProUGUI HpObj;
    PlayerHealthBar player;
    Slider sliderObj;

    Canvas canvasObj;
    private void Awake()
    {
        canvasObj = GetComponentInChildren<Canvas>();
        canvasObj.gameObject.name = "Canvas";        
        player = GetComponent<PlayerHealthBar>();
        sliderObj = GetComponentInChildren<Slider>();
        sliderObj.gameObject.name = "HealthSlider";
        slider = GetComponentInChildren<Slider>(sliderObj);
        player.Hp = HpObj;
        player.slider = sliderObj;
        currentHP = maxHP;        
    }
    private void Start()
    {
        if(canvasObj != null)
        {
            HpObj = GetComponentInChildren<TextMeshProUGUI>();
            HpObj.gameObject.name = "HPText";
        }
        player.Hp = HpObj;
    }
    public void OnClickEventAttack()
    {
        if (currentHP > 0)
        {
            currentHP -= indamage;
            currentHP = Mathf.Max(currentHP, 0);
            slider.value = currentHP / maxHP;
            Hp.text = $"{currentHP}/{maxHP}";
        }
    }
}
