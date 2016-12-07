using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	public Slider YAngleS;
	public Slider YHeightS;
	public Text YAngleT;
	public Text YHeightT;
	Transform cam;
	void Awake()
	{
		cam = Camera.main.transform;
	}
	void Start () {
		YAngleS.value = cam.rotation.eulerAngles.x;
		YAngleT.text = cam.rotation.eulerAngles.x.ToString("N1");
		YHeightS.value = cam.position.y;
		YHeightT.text = cam.position.y.ToString("N1");
	}
	
	// Update is called once per frame
	void Update () {
		//Transform cam = Camera.main.transform;
		//cam.rotation = Quaternion.Euler( YAngleS.value, 0,0);
		//cam.position = new Vector3(
		//YHeightS.value = cam.position.y;

		YAngleT.text = cam.rotation.eulerAngles.x.ToString("N1");
		YHeightT.text = cam.position.y.ToString("N1");
	}

	public void ChangeAngle()
	{
		cam.rotation = Quaternion.Euler(YAngleS.value, 0, 0);
	}

	public void ChangeHeight()
	{
		cam.position = new Vector3(cam.position.x, YHeightS.value, cam.position.z);
	}
}
