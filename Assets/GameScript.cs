using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {


	public Text point;
	public Button StartButton;
	// Use this for initialization
	void Start()
	{
		int score = PlayerPrefs.GetInt("HighScore");
		point.text = score.ToString("N0");
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
				SceneManager.LoadScene(1);
				break;
		}
	}
}
