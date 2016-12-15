using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailScript : MonoBehaviour {
	RectTransform rect;
	void Awake()
	{
		rect = GetComponent<RectTransform>();
	}

	void Start () {
		
	}
	public void ShowPanel()
	{
		rect.localPosition = new Vector3(0, 0, 0);
	}
	public void HidePanel()
	{
		rect.localPosition = new Vector3(0, 0, 1800);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
