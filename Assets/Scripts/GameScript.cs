using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {


	public Text point;
	public Button StartButton;
	public Slider LoadBar;
	// Use this for initialization
	void Awake()
	{
		LoadBar.gameObject.SetActive(false);
	}
	void Start()
	{
		int score = PlayerPrefs.GetInt("HighScore");
		point.text = score.ToString("N0");
		//LoadBar.enabled = false;

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
				//SceneManager.LoadScene(1);
				StartCoroutine(loadAsync());
				//Asy
				break;
		}
	}

	private IEnumerator loadAsync()
	{
		LoadBar.gameObject.SetActive(true);
		AsyncOperation operation = SceneManager.LoadSceneAsync(1);
		while (!operation.isDone)
		{
			yield return operation.isDone;
			LoadBar.value = operation.progress;
			Debug.Log("loading progress: " + operation.progress);
		}
		LoadBar.value = 1;
		yield return new WaitForSeconds(0.5f);
		Debug.Log("load done");
	}
}
