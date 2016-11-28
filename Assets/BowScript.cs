using UnityEngine;
using System.Collections;

public class BowScript : MonoBehaviour {

	// Use this for initialization
	//public GameObject Arrow;
	public GameObject Bow;
	//Vector3 initPosArrow;
	//Vector3 endPosArrow;
	void Start()
	{
		//initPosArrow = Arrow.transform.localPosition;
		//endPosArrow = initPosArrow - new Vector3(0, 0, 3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReleaseArrow()
	{
		//Arrow.transform.localPosition = initPosArrow;
		//Arrow.
	}

	public void PullArrow()
	{
		//StartCoroutine(MoveBackArrow());
	}

	//IEnumerator MoveBackArrow()
	//{
	//	float dTime = 0;
	//	while (Arrow.transform.localPosition != endPosArrow)
	//	{
	//		dTime += Time.deltaTime;
	//		Arrow.transform.localPosition = Vector3.Lerp(initPosArrow, endPosArrow, dTime);

	//		yield return new WaitForSeconds(0.04f);
	//	}
	//}
}
