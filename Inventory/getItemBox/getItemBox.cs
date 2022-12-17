using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class getItemBox : MonoBehaviour, IPointerClickHandler
{
    //아이템을 습득하면 업데이트 되는 슬롯의 컴포넌트
    //getItemBoxSlotList 선언
    public List<getItemBoxSlot> getItemBoxSlotList;

    setItemBox setItemBox;
    setItemBoxSlot setItemBoxSlot;
    getAddItem getAddItemCount;
    public Sprite setItemBoxImg;
    int getItemBoxCount;
    GameObject getAddItemCountObj;
    string setItemEqualName;
    Color color;
    bool isCheck;
    bool isChecking;
    void Awake()
    {
        //SetItemBox
        GameObject setItemObj = GameObject.Find("SetItemBox");
        setItemBox = setItemObj.GetComponent<setItemBox>();
        getAddItemCountObj = GameObject.FindGameObjectWithTag("Player");
        
        for (int i = 0; i < setItemBox.setItemBoxList.Count; i++)
        {
            setItemBox.setItemBoxList[i].setItemImg.sprite = null;
            color = setItemBox.setItemBoxList[i].setItemImg.color;
            color.a = 0;
            setItemBox.setItemBoxList[i].setItemImg.color = color;
            setItemBox.setItemBoxList[i].isfilledImg = false;
        }
        isCheck = false;
    }
    //SetItemBox의 아이템이 채워져있는지 검사하는 함수, 채워져있으면 true 반환
    public bool checkFilledImg()
    {
        for (int i = 0; i < setItemBox.setItemBoxList.Count; i++)
        {
            if (setItemBox.setItemBoxList[i].setItemImg.gameObject.activeSelf == true)
            {
                return true;
            }
        }
        return false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        //GetItemBox의 슬롯을 클릭했을때
        for (int i = 0; i < getItemBoxSlotList.Count; i++)
        {            
            if (getItemBoxSlotList[i].IsInRect(eventData.position) == true && getItemBoxSlotList[i].emptyItem.gameObject.activeSelf == true)
            {
                setItemEqualName = getItemBoxSlotList[i].emptyItem.sprite.name;
                setItemBoxImg = Resources.Load<Sprite>(setItemEqualName);

                //setItemBox에서 게임 오브젝트가 활성화 되어 있는 것이 있다면 getItemBox의 아이템 count는 변화가 없음.
                //setItemBox 게임오브젝트 활성/비활성 검사
                for (int k = 0; k < setItemBox.setItemBoxList.Count; k++)
                {
                    if(setItemBox.setItemBoxList[k].setItemImg.gameObject.activeSelf == false && setItemEqualName == setItemBox.setItemBoxList[k].setItemImg.gameObject.name)
                    {
                        isCheck = false;
                    }
                    else if (setItemBox.setItemBoxList[k].setItemImg.gameObject.activeSelf == true && setItemEqualName == setItemBox.setItemBoxList[k].setItemImg.gameObject.name)
                    {
                        isCheck = true;
                    }                    
                   isChecking = isCheck;
                }
                
                //아이템을 클릭했을 때 아이템이 활성화 true, 게임 오브젝트 이름 정보와 같은 sprite를 setItemBoxSlot에 넣는다.

                string setItemObjName = getItemBoxSlotList[i].emptyItem.sprite.name;

                //getAddItem 컴포넌트의 count를 가져옴
                getAddItemCount = getAddItemCountObj.GetComponent<getAddItem>();
                
                //getItemBoxSlotList[i].itemCount.text가 0이 아니면 클릭할때마다 getItemBoxCount 감소
                if (getItemBoxSlotList[i].itemCount.text != "0" && isChecking == false) // setItemBox 활성/비활성 검사해서 false인 것만 count 감소
                {
                    getItemBoxCount = int.Parse(getItemBoxSlotList[i].itemCount.text);
                    getItemBoxCount--;
                    getItemBoxSlotList[i].itemCount.text = getItemBoxCount.ToString();
                    //getItemBoxCount가 0이 되면 getItemBoxSlotList의 카운트 텍스트도 0으로 바꿈
                    if (getItemBoxCount == 0)
                    {
                        getItemBoxSlotList[i].itemCount.text = "0";
                    }
                }
                //getItemBoxCount 0이 되면 카운트 숫자, 게임오브젝트, 아이템 게임오브젝트 name 변경 및 오브젝트 비활성화
                if (getItemBoxSlotList[i].itemCount.text == "0" )
                {
                    getItemBoxSlotList[i].itemCount.gameObject.SetActive(false);
                    getItemBoxSlotList[i].itemCount.gameObject.name = "EmptyCount";
                    getItemBoxSlotList[i].emptyItem.gameObject.SetActive(false);
                    getItemBoxSlotList[i].emptyItem.gameObject.name = "Empty";
                    getItemBoxSlotList[i].isFilledSlot = false;
                }                
                for (int j = 0; j < setItemBox.setItemBoxList.Count; j++)
                {

                    //클릭했을 때 SetItemBox로 아이템이 옮겨지도록 함.   
                    if (setItemEqualName == setItemBox.setItemBoxList[j].setItemImg.gameObject.name)
                    {
                        setItemBox.setItemBoxList[j].setItemImg.gameObject.SetActive(true);
                        color = setItemBox.setItemBoxList[j].setItemImg.color;
                        color.a = 1.0f;
                        setItemBox.setItemBoxList[j].setItemImg.color = color;
                        setItemBox.setItemBoxList[j].setItemImg.sprite = setItemBoxImg;
                        setItemBox.setItemBoxList[j].isfilledImg = true;
                        
                    }
                }
            }
        }        
    }
}
