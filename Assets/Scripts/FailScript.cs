using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailScript : MonoBehaviour {
	RectTransform rect;
	Vector3 origin;
	void Awake()
	{
		rect = GetComponent<RectTransform>();
		origin = rect.localPosition;
	}

	void Start () {
		
	}
	public void ShowPanel()
	{
		rect.localPosition = new Vector3(0, 0, 0);
	}
	public void HidePanel()
	{
		rect.localPosition = origin;
	}
}
