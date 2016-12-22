﻿using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{

	public Text FpsLabel;
	//public Text InputLabel;
	public InputCtrl inputctrl;
	public float updateInterval = 0.5F;

	private float accum = 0; // FPS accumulated over the interval
	private int frames = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval

	void Awake()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}

	void Start()
	{
		timeleft = updateInterval;
	}

	void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		++frames;
		if (timeleft <= 0.0)
		{
			float fps = accum / frames;
			string format = System.String.Format("{0:F2} FPS", fps);

			FpsLabel.text = format;
			//InputLabel.text = inputctrl.TouchPoint.ToString();
			timeleft = updateInterval;
			accum = 0.0F;
			frames = 0;
		}
	}
}
