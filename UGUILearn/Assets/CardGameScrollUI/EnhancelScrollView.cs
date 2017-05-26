using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public  class EnhancelScrollView : MonoBehaviour {
    public AnimationCurve PositionCurve;//模拟位置曲线 ？
    public AnimationCurve ScaleCurve;//模拟缩放曲线 ？

	public float PosCurveFactor=500f;
	public float yPositionValue=46f;

    public List<EnhanceItem> ScrollViewItems = new List<EnhanceItem>();//保存每一张卡牌的数据
    public List<Image> ImageTargets = new List<Image>();//保存每一张卡牌的图片 ？为什么有必要保存
    public float d_Factor=0.2f;
	public float Duration=0.2f;//スワイプ可能な間隔
	public float HorizontalValue=0f;
	public float HorizontalTargetValue=0.1f;



	private float OriginHorizontalValue=0.1f;
    private float[] m_MoveHorizontalValues;//
    private float[] m_DHorizontalValues;//
	private float m_CurrentDuration;
	private bool isInit=false;
	private EnhanceItem m_Centeritem;
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


	void Update()
	{
		if (!isInit) {
			return;
		}
		m_CurrentDuration += Time.deltaTime;
		SortDepth ();
		if (m_CurrentDuration > Duration) {
			m_CurrentDuration = Duration;

			if (m_Centeritem == null) {
				var obj = transform.GetChild (transform.childCount - 1);//?
				if (obj != null) {
					m_Centeritem = obj.GetComponent<EnhanceItem> ();

				}
				if (m_Centeritem != null) {
					m_Centeritem.SetSelectColor (true);
				}

			} else {
				m_Centeritem.SetSelectColor (true);
			}


		}


		float percent = m_CurrentDuration / Duration;
		HorizontalValue = Mathf.Lerp (OriginHorizontalValue, HorizontalTargetValue, percent);
		UpdateEnhanceScrollView (HorizontalValue);




	}

	public void UpdateEnhanceScrollView(float fValue){
		for (int i = 0; i < ScrollViewItems.Count; i++) {
			EnhanceItem itemScript = ScrollViewItems [i];
			float xValue = GetXValue (fValue,m_DHorizontalValues[itemScript.ScrollViewItemIndex]);
			float scaleValue = GetScaleValue (fValue, m_DHorizontalValues [itemScript.ScrollViewItemIndex]);
			itemScript.UpdataScrollViewitems (xValue,yPositionValue,scaleValue);
		}
	}

	private float GetXValue(float sliderValue,float added){
		float evaluteValue = PositionCurve.Evaluate (sliderValue + added) * PosCurveFactor;//(sliderValue + added)指定された区間のカーブ値を評価する
		return evaluteValue;
	}

	private float GetScaleValue(float sliderValue,float add){
		float scaleValue = ScaleCurve.Evaluate (sliderValue + add);
		return scaleValue;
	}

	public void SortDepth(){
		ImageTargets.Sort (new CompareDepthMethod ());//IComparerインタフェースを使って、カードをソートする
		for(int i=0;i<ImageTargets.Count;i++){
			ImageTargets [i].transform.SetSiblingIndex (i);//エディターにおいての並び順を左から右へとソートする
		}



	}

	public class CompareDepthMethod:IComparer<Image>{
		public int Compare(Image left, Image right){
			if (left.transform.position.x > right.transform.position.x) {
				return 1;
			} else if (left.transform.position.x < right.transform.position.x) {
				return -1;
			} else {
				return 0;
			}
		}
	}



}

