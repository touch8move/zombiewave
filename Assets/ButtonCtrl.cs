using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.

public class ButtonCtrl : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
	
	public void OnPointerDown(PointerEventData eventData)
	{
		//Debug.Log("Pressed");
		FindObjectOfType<Controller>().IncreasePower();
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		//Debug.Log("Released");
		FindObjectOfType<Controller>().releasePower();
	}
}
