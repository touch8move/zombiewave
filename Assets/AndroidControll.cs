using UnityEngine;
using System.Collections;

public class AndroidControll : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
}