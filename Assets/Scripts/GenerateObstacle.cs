using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GenerateObstacle : MonoBehaviour {
    public GameObject obstales;
    public bool gen = true;
    public float seconds = 0;
    public int updateCounter = 0;
    public int maxRange = 170;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (gen)
        {
            gen = false;

            Invoke("GenerateNewObstacle", 2);
        }
        else
            updateCounter++;
        if (updateCounter >= maxRange)
        {
            gen = true;
            updateCounter = 0;
        }
    }

    void GenerateNewObstacle()
    {
        //UpdateScore.score++;
        //UpdateScore.PrintScore();
        float y = Random.Range(-2.73f, 2.93f);
        float x = 14.86f;
        Instantiate(obstales, new Vector3(x, y), Quaternion.identity);
    }
}
