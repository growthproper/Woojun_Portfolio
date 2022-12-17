using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class setItemBoxSlot : MonoBehaviour
{
    //공격력, 방어력 각 슬롯의 크기를 get하는 컴포넌트
    public Image setItemBoxSlotImg;
    public Image setItemImg;
    public bool isfilledImg;
    
    float xMin;
    public float XMIN
    {
        get
        {
            xMin = transform.position.x - setItemBoxSlotImg.rectTransform.rect.width * 0.5f;
            return xMin;
        }
    }
    float xMax;
    public float XMAX
    {
        get
        {
            xMax = transform.position.x + setItemBoxSlotImg.rectTransform.rect.width * 0.5f;
            return xMax;
        }
    }
    float yMin;
    public float YMIN
    {
        get
        {
            yMin = transform.position.y - setItemBoxSlotImg.rectTransform.rect.height * 0.5f;
            return yMin;
        }
    }
    float yMax;
    public float YMAX
    {
        get
        {
            yMax = transform.position.y + setItemBoxSlotImg.rectTransform.rect.height * 0.5f;
            return yMax;
        }
    }
    void Start()
    {
        xMin = transform.position.x - setItemBoxSlotImg.rectTransform.rect.width * 0.5f;
        xMax = transform.position.x + setItemBoxSlotImg.rectTransform.rect.width * 0.5f;
        yMin = transform.position.y - setItemBoxSlotImg.rectTransform.rect.height * 0.5f;
        yMax = transform.position.y + setItemBoxSlotImg.rectTransform.rect.height * 0.5f;
    }
    public bool IsInRect(Vector2 pos)
    {
        if (pos.x >= XMIN && pos.x <= XMAX && pos.y >= YMIN && pos.y <= YMAX)
        {
            return true;
        }
        return false;
    }
    

}
