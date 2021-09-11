using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GetTouch : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    //[SerializeField]
    public GameObject Right_Touch_Obj;
    public float x,y,z;
    public Text PrintData;
    public void OnBeginDrag(PointerEventData eventData)
    {
        x=eventData.delta.x;
        y=eventData.delta.y;
        Debug.Log("X="+x+" Y="+y);
        SetString();
    }
    public void OnDrag(PointerEventData eventData)
    {}/*
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
    void SetString()
    {
        string str;
        str="  X_"; str+=x;
        str+="\n  Y_"; str+=y;
        str+="\n  Z_"; str+=z;
        PrintData.text=str;
    }
}
