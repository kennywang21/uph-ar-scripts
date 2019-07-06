using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {
	private Image bgImg;
	private Image joystickImg;

	public Vector3 InputDirection { get; set; }

	private void Start() {
		bgImg = GetComponent<Image> ();
		joystickImg = transform.GetChild(0).GetComponent<Image> ();
	}

	public virtual void OnDrag(PointerEventData ped) {
		Vector2 pos = Vector2.zero;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(bgImg.rectTransform,
			    ped.position,
			    ped.pressEventCamera,
			    out pos)) 
		{
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
			float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

			InputDirection = new Vector3 (x, 0f, y);

			if (InputDirection.magnitude > 1f) {
				
				InputDirection = InputDirection.normalized;	
			}
				
			joystickImg.rectTransform.anchoredPosition = 
				new Vector2 (InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3)
					, InputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3));
		}
	}

	public virtual void OnPointerDown(PointerEventData ped) {
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped) {
		InputDirection = Vector3.zero;
		joystickImg.rectTransform.anchoredPosition = Vector3.zero;
	}
}
