using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	public Slider YAngleS;
	public Slider YHeightS;
	public Slider FieldOfViewS;
	public Slider ZS;
	public Slider XS;
	public Text YAngleT;
	public Text YHeightT;
	public Text FieldOfViewT;
	public Text ZT;
	public Text XT;

	Transform cam;
	void Awake()
	{
		cam = Camera.main.transform;
	}
	void Start () {
		YAngleS.value = cam.rotation.eulerAngles.x;
		YHeightS.value = cam.position.y;
		FieldOfViewS.value = Camera.main.fieldOfView;
		ZS.value = cam.position.z;
		XS.value = cam.position.x;

		YAngleT.text = cam.rotation.eulerAngles.x.ToString("N1");
		YHeightT.text = cam.position.y.ToString("N1");
		FieldOfViewT.text = Camera.main.fieldOfView.ToString("N0");
		ZT.text = cam.position.z.ToString("N1");
		XT.text = cam.position.x.ToString("N1");
	}
	
	// Update is called once per frame
	void Update () {
		YAngleT.text = cam.rotation.eulerAngles.x.ToString("N1");
		YHeightT.text = cam.position.y.ToString("N1");
		FieldOfViewT.text = Camera.main.fieldOfView.ToString("N0");
		ZT.text = cam.position.z.ToString("N1");
		XT.text = cam.position.x.ToString("N1");
	}

	public void ChangeAngle()
	{
		cam.rotation = Quaternion.Euler(YAngleS.value, 0, 0);
	}

	public void ChangeHeight()
	{
		cam.position = new Vector3(cam.position.x, YHeightS.value, cam.position.z);
	}

	public void ChangeFieldOfView()
	{
		Camera.main.fieldOfView = FieldOfViewS.value;
	}

	public void ChangeZ()
	{
		cam.position = new Vector3(cam.position.x, cam.position.y, ZS.value);
	}

	public void ChangeX()
	{
		cam.position = new Vector3(XS.value, cam.position.y, cam.position.z);
	}
}
