using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScript : MonoBehaviour
{

	public Text point;
	//public Button restart;
	Animator ani;
	// Use this for initialization
	void Awake()
	{
		ani = GetComponent<Animator>();
		//ani.Stop();
	}
	void Start()
	{

	}

	public void Show()
	{
		ani.SetTrigger("GameOver");
		//ani.Play("GameOver");
	}
	public void SetPoint()
	{
		//ani.Stop();
		StartCoroutine(IncreasePoint(FindObjectOfType<Controller>().Point));
	}
	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator IncreasePoint(int value)
	{
		int current = 0;
		int step = value / 20;
		while (current < value)
		{
			current += step;
			point.text = current.ToString("N0");
			yield return new WaitForSeconds(0.02f);
		}
		point.text = value.ToString("N0");
	}
}