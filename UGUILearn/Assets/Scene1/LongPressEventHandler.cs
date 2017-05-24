using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongPressEventHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler,IPointerClickHandler {

    //長押しイベントをInspectorから外部に渡せるようにするためのUnityEvent
    [Serializable]
    public class LongPressEvent : UnityEvent { };
    public LongPressEvent onLongPress = new LongPressEvent();

    public float ThresHold = 0.6f;//長押しと判定するまでの時間
    public float TapGap = 1f;//長押し中にどの程度指のズレを許容するか

    private float m_PressTime = 0;//押されている時間
    private bool m_Cancled = false;//長押しキャンセルフラグ	
    private Vector3 m_StartTapPosition = Vector3.zero;//押し始めた座標



    //オブジェクト上でクリック(押して、離す)されたとき呼ばれる
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnCLick");
        if (m_PressTime > ThresHold)
        {
            return;

        }


        if (IsMove())
        {
            m_Cancled = true;
            return;
        }
        m_Cancled = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_StartTapPosition = this.transform.position;
        StartCoroutine(WatchTap());

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Cancled = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_Cancled = true;
    }
    
    //指が動いたか
    private bool IsMove()
    {
        if (Mathf.Abs(this.transform.position.x - m_StartTapPosition.x) > TapGap)
        {
            return true;
        }

        if (Mathf.Abs(this.transform.position.y - m_StartTapPosition.y) > TapGap)
        {
            return true;
        }
        return false;
    }
    //長押しの監視
    private IEnumerator WatchTap()
    {
        m_PressTime = 0;
        m_Cancled = false;
        while (true)
        {
            m_PressTime += Time.deltaTime;//押されている時間を更新する

            if (m_Cancled)//キャンセルや
            {
                break;
            }
            if (IsMove())//指が動いたら終了
            {
                break;
            }
            //閾値を超えたらLongPressイベントを発行
            if (m_PressTime >= ThresHold)
            {
                if (onLongPress != null)
                {
                    onLongPress.Invoke();
                }
                break;
            }
            yield return 0;
        }
    }
}
