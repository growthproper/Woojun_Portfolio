using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class setItemBox : MonoBehaviour, IPointerClickHandler
{
    //공격력, 방어력 업데이트 관련 컴포넌트
    public List<setItemBoxSlot> setItemBoxList;

    GameObject abilityObj;
    Transform [] abilityObjChild;
    TextMeshProUGUI [] abilityText;

    public int swordBaseNum = 5;
    public int swordStrNum = 10;
    public int shieldBaseNum = 6;
    public int shieldStrNum = 11;
    void Awake()
    {
        //공격력, 방어력 Text 오브젝트 찾기
        abilityObj = GameObject.Find("AbilityText");
        abilityObjChild = new Transform[2];
        for(int i = 0; i < abilityObjChild.Length; i++)
        {
            abilityObjChild[i] = abilityObj.transform.GetChild(i).GetChild(0);
        }

        //TextMeshProUGUI 타입 변수에 저장
        abilityText = new TextMeshProUGUI[2];
        for(int i = 0; i < abilityText.Length; i++)
        {
            abilityText[i] = abilityObjChild[i].GetComponent<TextMeshProUGUI>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < setItemBoxList.Count; i++)
        {
            if (setItemBoxList[i].IsInRect(eventData.position) == true)
            {
                //클릭하면 게임오브젝트 비활성화
                setItemBoxList[i].setItemImg.gameObject.SetActive(false);
                
                //각각의 아이템을 클릭하면 공격력과 방어력 count 감소
                if (setItemBoxList[i].setItemImg.gameObject.name == "swordItem")
                {
                    int swordMinus = swordBaseNum - 5;
                    abilityText[0].text = swordMinus.ToString();
                }
                if (setItemBoxList[i].setItemImg.gameObject.name == "strongSword")
                {
                    int swordMinus = swordStrNum - 10;
                    abilityText[0].text = swordMinus.ToString();
                }
                if (setItemBoxList[i].setItemImg.gameObject.name == "shieldItem")
                {
                    int shieldMinus = shieldBaseNum - 6;
                    abilityText[1].text = shieldMinus.ToString();
                }
                if (setItemBoxList[i].setItemImg.gameObject.name == "strongShield")
                {
                    int shieldMinus = shieldStrNum - 11;
                    abilityText[1].text = shieldMinus.ToString();
                }
            }
        }
    }
    
    void Update()
    {
        //각각의 아이템이 setItemBoxSlot에 채워지면 공격력과 방어력 count 증가
        //swordItem
        if (setItemBoxList[0].setItemImg.gameObject.activeSelf == true)
        {
            abilityText[0].text = swordBaseNum.ToString();
        }
        //strongSword
        if (setItemBoxList[1].setItemImg.gameObject.activeSelf == true)
        {
            abilityText[0].text = swordStrNum.ToString();
        }
        //swordItem && strongSword
        if (setItemBoxList[0].setItemImg.gameObject.activeSelf == true && setItemBoxList[1].setItemImg.gameObject.activeSelf == true)
        {
            int swordSum = swordBaseNum + swordStrNum;
            abilityText[0].text = swordSum.ToString();
        }

        //sheildItem
        if (setItemBoxList[2].setItemImg.gameObject.activeSelf == true)
        {
            abilityText[1].text = swordBaseNum.ToString();
        }
        //strongSheild
        if (setItemBoxList[3].setItemImg.gameObject.activeSelf == true)
        {
            abilityText[1].text = swordStrNum.ToString();
        }
        //sheildItem && strongSheild
        if (setItemBoxList[2].setItemImg.gameObject.activeSelf == true && setItemBoxList[3].setItemImg.gameObject.activeSelf == true)
        {
            int shieldSum = shieldBaseNum + shieldStrNum;
            abilityText[1].text = shieldSum.ToString();
        }
    }
}
