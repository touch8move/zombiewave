using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScript : MonoBehaviour {
	//GameManager gm;
	RectTransform rect;
	Vector3 origin;
	void Awake()
	{
		rect = GetComponent<RectTransform>();
		origin = rect.localPosition;
	}
	public void ShowPanel()
	{
		rect.localPosition = new Vector3(0, 0, 0);
	}
	public void HidePanel()
	{
		rect.localPosition = new Vector3(0, 0, 1800);
	}
}
