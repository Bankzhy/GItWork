using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum DragPosition{
	Left,
	Right,
	Up,
	Down,
}

public class CDragOnCard : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {
	public bool isVertical=false;
	public bool DragOnSuface=true;

	private DragPosition m_DragPosition=DragPosition.Left;
	private RectTransform m_DraggingPlane;
	private bool isSelf=false;

	public void OnBeginDrag(PointerEventData eventData){
		Vector2 touchDeltaPosition = Vector2.zero;
#if UNITY_EDITOR
		float delta_x=Input.GetAxis("Mouse X");
		float delta_y=Input.GetAxis("Mouse Y");
		touchDeltaPosition=new Vector2(delta_x,delta_y);
#elif UNITY_ANDROID||UNITY_IPHONE
		touchDeltaPosition=Input.GetTouch(0).deltaPosition;
#endif
		if (isVertical) {
			if (touchDeltaPosition.y > 0) {
				Debug.Log ("UpDrag");
				m_DragPosition = DragPosition.Up;
			} else {
				Debug.Log ("DownDrag");
				m_DragPosition = DragPosition.Down;
			}

			if (Mathf.Abs (touchDeltaPosition.x) > Mathf.Abs (touchDeltaPosition.y)) {
				isSelf = true;
				var canvas = FindInParents<Canvas> (gameObject);
				if (canvas == null)
					return;
				if (DragOnSuface)
					m_DraggingPlane = transform as RectTransform;//?
				else
					m_DraggingPane = canvas.transform as RectTransform;//?
			} else {
				isSelf = false;

			}

		} else {

		}
	}

	public void OnDrag(PointerEventData eventData){

	}

	public void OnEndDrag(PointerEventData eventData){

	}

	public static T FindInParents<T>(GameObject go)where T:Component{ 
		if (go == null)
			return null;
		var comp = go.GetComponent<T> ();
		if (comp != null) {
			return comp;
		}
		Transform t = go.transform.parent;
		if (t != null && comp == null) {
			comp = t.GetComponent<T> ();
			t = t.parent;
		}
		return comp;
	}
}
