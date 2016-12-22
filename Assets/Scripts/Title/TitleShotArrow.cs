using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleShotArrow : MonoBehaviour {
	public GameObject Arrow;
	AudioSource audio;
	//public GameObject ShotPoint;
	public float power;

	void Awake()
	{
		audio = GetComponent<AudioSource>();
	}
	void Update()
	{
		for (int i = 0; i < Input.touches.Length; i++)
		{
			HandleTouch(Input.touches[i].fingerId, Input.touches[i].position, Input.touches[i].phase);
		}

		if (Input.touchCount == 0)
		{
			if (Input.GetMouseButtonDown(0))
			{
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
				ShotArrow(0, 0);
				break;
		}
	}

	public void ShotArrow(float yAngle, float xAngle)
	{
		//GameObject arrow = Instantiate(Arrow);
		//GameObject _startPoint = Instantiate(ShotPoint, ShotPoint.transform.position, Quaternion.Euler(360 - xAngle, yAngle + 180, 0.0f));
		//GameObject _startPoint = Instantiate(ShotPoint, ShotPoint.transform.position, Quaternion.identity);
		//Arrow.transform.SetParent(ShotPoint.transform);
		//Arrow.transform.localPosition = Vector3.zero;
		//Arrow.transform.localRotation = Quaternion.Euler(0, 180, 0);
		Arrow.GetComponent<TitleArrow>().Shot(power, 0);
		audio.Play();
		StartCoroutine(loadAsync());
	}

	private IEnumerator loadAsync()
	{
		yield return new WaitForSeconds(1f);
		AsyncOperation operation = SceneManager.LoadSceneAsync(1);
		while (!operation.isDone)
		{
			yield return operation.isDone;
			Debug.Log("loading progress: " + operation.progress);
		}
		yield return new WaitForSeconds(1f);
		Debug.Log("load done");
	}
}
