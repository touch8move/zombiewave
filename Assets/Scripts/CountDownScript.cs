using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDownScript : MonoBehaviour {

	// Use this for initialization
	float CurTime;
	Text TimerText;
	RectTransform panel;
	float tmpCurTime;
	void Awake()
	{
		panel = GetComponent<RectTransform>();
		TimerText = GetComponentInChildren<Text>();
	}
	void Start () {
		
	}
	public void CountDownStart()
	{
		panel.localPosition = new Vector3(0, 0, 0);
		CurTime = 4;
		StartCoroutine(CountDown());
	}
	IEnumerator CountDown()
	{
		tmpCurTime = CurTime;
		float tic = 1f;
		while (CurTime > 1)
		{
			CurTime -= tic;
			tmpCurTime = CurTime;
			TimerText.text = CurTime.ToString("N0");
			yield return new WaitForSeconds(tic);

		}
		TimerText.text = "GO";
		Invoke("RemovePanel", 1);
	}

	void RemovePanel()
	{
		panel.position = new Vector3(0, 0, 1800);
		//StartGame
	}
}
