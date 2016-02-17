using UnityEngine;
using System.Collections;

public class UpdateScore : MonoBehaviour {
    public static GUIText scoreText;
    public static int score;

    // Use this for initialization
    void Start () {
        scoreText = GetComponent<GUIText>();
        score = 0;
        PrintScore();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void PrintScore()
    {
        scoreText.text = "Distance : " + score;
        score++;
        Invoke("PrintScore", 0.5f);
    }
}
