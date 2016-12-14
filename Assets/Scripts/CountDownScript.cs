using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDownScript : MonoBehaviour {

	// Use this for initialization
	float CurTime;
	Text TimerText;
	RectTransform panel;
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
		CurTime = 3;
		StartCoroutine(CountDown());
	}
	IEnumerator CountDown()
	{
		while (CurTime > 0)
		{
			CurTime -= Time.deltaTime;
			TimerText.text = CurTime.ToString("N0");
			yield return new WaitForSeconds(0.02f);
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
