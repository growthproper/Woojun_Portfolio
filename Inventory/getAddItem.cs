using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class getAddItem : MonoBehaviour
{
    //아이템 습득하면 인벤토리 업데이트하는 컴포넌트
    public getItemBox getItemBox;   
    public Sprite getItemSprite;    
    public int getItemCount;
    string getItemName;
    public List<getItemBoxSlot> falseSlot;
    public List<getItemBoxSlot> addfalseSlot;

    public AudioClip getItemClip;
    public AudioSource getItem;
    void Awake()
    {
        GameObject findGetItemBoxObj = GameObject.Find("Canvas/Inventory/GetItemBox");
        GameObject inventoryObj = GameObject.Find("Canvas/Inventory");
        inventoryObj.SetActive(false);
        //GetItemBox
        getItemBox = findGetItemBoxObj.GetComponent<getItemBox>();

        //아이템 효과음
        getItem = GetComponent<AudioSource>();
        getItemClip = Resources.Load<AudioClip>("Audio/getItem");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            getItem.clip = getItemClip;
            getItem.Play();

            //게임오브젝트 name을 저장
            getItemName = other.gameObject.name;

            //자신의 게임오브젝트 name과 같은 name의 sprite를 Resources폴더에서 찾아서 저장
            getItemSprite = Resources.Load<Sprite>(getItemName);
           
            //현재 인덱스까지 검사
            for (int j = 0; j <= getItemBoxSlot.currentIndex; j++)
            {
                //현재 인덱스 까지 비어있는 슬롯이 있다면 비어있는 슬롯부터 추가하면서 count 1로 변경
                if (getItemBox.getItemBoxSlotList[j].isFilledSlot == false && getItemBox.getItemBoxSlotList[j].emptyItem.gameObject.name == "Empty")
                {
                    falseSlot = new List<getItemBoxSlot>();
                    falseSlot.Add(getItemBox.getItemBoxSlotList[j]);
                    falseSlot[0].emptyItem.gameObject.SetActive(true);
                    falseSlot[0].emptyItem.gameObject.name = getItemName;
                    falseSlot[0].emptyItem.sprite = getItemSprite;
                    falseSlot[0].itemCount.gameObject.SetActive(true);
                    falseSlot[0].itemCount.gameObject.name = getItemName + "_Count";
                    falseSlot[0].isFilledSlot = true;
                    falseSlot[0].itemCount.text = "1";
                    getItemBoxSlot.currentIndex++;
                    break;
                }                 
                //현재 인덱스 까지 trigger한 아이템의 이름이 슬롯의 아이템 이름과 같다면 count만 1 증가
                else if (getItemBox.getItemBoxSlotList[j].emptyItem.gameObject.name == getItemName)
                {
                    //getItemBox.getItemBoxSlotList[getItemBoxSlot.currentIndex].emptyItem.sprite = null;
                    getItemCount = int.Parse(getItemBox.getItemBoxSlotList[j].itemCount.text);
                    getItemCount++;
                    getItemBox.getItemBoxSlotList[j].itemCount.text = getItemCount.ToString();
                    getItemBox.getItemBoxSlotList[j].isFilledSlot = true;                    
                    break;
                }
            }
        }
        //trigger한 아이템은 비활성화
        other.gameObject.SetActive(false);
    }
}
