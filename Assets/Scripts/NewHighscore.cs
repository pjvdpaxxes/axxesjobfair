using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewHighscore : MonoBehaviour {

	public static Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text> ();	
		setText ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setText() {
		if (PlayerPrefs.GetInt ("BrokeHighscore") == 1) {
			PlayerPrefs.SetInt ("BrokeHighscore", 0);
			scoreText.text = "New highscore!!!";
		} else if (PlayerPrefs.GetInt("Score") > 0) {
			scoreText.text = "Your score: " + PlayerPrefs.GetInt ("Score");
		}
	}
}
