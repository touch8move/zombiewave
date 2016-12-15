using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScript : MonoBehaviour {
	GameManager gm;
	RectTransform rect;
	void Awake()
	{
		gm = FindObjectOfType<GameManager>();
		rect = GetComponent<RectTransform>();
	}
	public void ShowPanel()
	{
		rect.localPosition = new Vector3(0, 0, 0);
	}
	public void HidePanel()
	{
		rect.localPosition = new Vector3(0, 0, 1800);
	}
	void Update()
	{
		foreach (Touch touch in Input.touches)
		{
			HandleTouch(touch.fingerId, (touch.position), touch.phase);
		}

		// Simulate touch events from mouse events
		if (Input.touchCount == 0)
		{
			if (Input.GetMouseButtonDown(0))
			{
				//Debug.Log("MousePoint:" + (Input.mousePosition));
				HandleTouch(10, (Input.mousePosition), TouchPhase.Began);
			}
			if (Input.GetMouseButton(0))
			{
				HandleTouch(10, (Input.mousePosition), TouchPhase.Moved);
			}
			if (Input.GetMouseButtonUp(0))
			{
				HandleTouch(10, (Input.mousePosition), TouchPhase.Ended);
			}
		}
	}

	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase)
	{
		switch (touchPhase)
		{
			case TouchPhase.Began:
				// TODO
				break;
			case TouchPhase.Moved:
				// TODO
				break;
			case TouchPhase.Ended:
				// TODO
				gm.StartGame();
				break;
		}
	}
}
