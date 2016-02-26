using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour {
    public static Text scoreText;
    public static int score;

    // Use this for initialization
    void Start () {
		scoreText = GetComponent<Text>();
        score = 0;
        PrintScore();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void PrintScore()
    {
        scoreText.text = "Score : " + score;
        score++;
        Invoke("PrintScore", 0.5f);
    }
}
