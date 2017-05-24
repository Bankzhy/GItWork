using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UGUITest : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerEnterHandler,IPointerDownHandler,IPointerUpHandler {
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(1);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(2);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(3);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(4);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(5);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(6);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
