using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceItem : MonoBehaviour {
    public int ScrollViewItemIndex = 0;

    Vector3 _targetPosition = Vector3.one; //変換先の位置情報
    Vector3 _targetScale = Vector3.one;//変換先のサイズ情報

    Transform m_Transform;
    Image m_Image;

    private int m_index = 1;
        
    public void Init(int cardIndex = 1)
    {
        m_index = cardIndex;

        m_Transform = this.transform;
        m_Image = GetComponent<Image>();
        m_Image.sprite = Resources.Load<Sprite>(string.Format( "/Card_Pack1_[0]", cardIndex % 6 + 1));

    }


    public void UpdataScrollViewitems(float x,float y,float scale)
    {
        _targetPosition.x = x;
        _targetPosition.y = y;
        _targetScale.x = _targetScale.y = scale;

        m_Transform.localPosition = _targetPosition;
        m_Transform.localScale = _targetScale;
    }

   public void SetSelectColor(bool isCenter)
    {
        if (m_Image != null)
        {
            m_Image = GetComponent<Image>();
        }

        if (isCenter)
        {
            m_Image.color = Color.white;

        }
        else
        {
            m_Image.color = Color.gray;

        }
    }
}
