using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    Text text;
    void Awake ()
    {
        text = GetComponent <Text> ();
    }

	public void SetScore(int score)
	{
		text.text = score.ToString("N0");
	}
}
