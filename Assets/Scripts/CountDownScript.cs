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
	public void CountDownStart(int wave)
	{
		TimerText.text = "Wave " + wave;
		panel.localPosition = new Vector3(0, 0, 0);
		CurTime = 4;
		StartCoroutine(CountDown());
	}
	IEnumerator CountDown()
	{
		yield return new WaitForSeconds(2);
		tmpCurTime = CurTime;
		float tic = 1f;
		while (tmpCurTime > 1)
		{
			tmpCurTime -= tic;
			//tmpCurTime = CurTime;
			TimerText.text = tmpCurTime.ToString("N0");
			yield return new WaitForSeconds(tic);

		}
		TimerText.text = "GO";
		Invoke("RemovePanel", 1);
	}

	void RemovePanel()
	{
		panel.position = new Vector3(0, 0, 3600);
		//StartGame
	}
}
