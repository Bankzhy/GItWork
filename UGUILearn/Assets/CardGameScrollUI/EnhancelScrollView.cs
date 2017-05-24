using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public  class EnhancelScrollView : MonoBehaviour {
    public AnimationCurve PositionCurve;//模拟位置曲线 ？
    public AnimationCurve ScaleCurve;//模拟缩放曲线 ？
    public List<EnhanceItem> ScrollViewItems = new List<EnhanceItem>();//保存每一张卡牌的数据
    public List<Image> ImageTargets = new List<Image>();//保存每一张卡牌的图片 ？为什么有必要保存
    public float d_Factor=0.2f;


    private float[] m_MoveHorizontalValues;//
    private float[] m_DHorizontalValues;//


    public void Init()
    {
        if (m_MoveHorizontalValues == null)
        {
            m_MoveHorizontalValues = new float[ScrollViewItems.Count];

        }

        if (m_DHorizontalValues == null)
        {
            m_DHorizontalValues = new float[ScrollViewItems.Count];
        }

        int centerIndex = ScrollViewItems.Count / 2;//卡牌数为5的时候 值为2
        for (int i = 0; i < ScrollViewItems.Count; i++)
        {
            ScrollViewItems[i].ScrollViewItemIndex = i;
            Image tempImage = ScrollViewItems[i].GetComponent<Image>();
            ImageTargets.Add(tempImage);

            m_DHorizontalValues[i] = d_Factor * (centerIndex - i);//m_DHorizontalValues最后的代入值为[0.4, 0.2, 0，-0.2，-0.4]
            m_DHorizontalValues[centerIndex] = 0.0f;//防御代码：确保中间值为0

            m_MoveHorizontalValues[i] = 0.5f - m_DHorizontalValues[i];//m_MoveHorizontalValues最终代入值    [0.1, 0.3, 0.5, 0.7, 0.9]
            ScrollViewItems[i].SetSelectColor(false);//すべてのカードの色を灰色

        }
    }





}

