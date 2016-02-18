using UnityEngine;
using System.Collections;

public class GenerateCoin : MonoBehaviour {
    public Sprite[] coinSprites;
    public GameObject coinPrefab;

    // Use this for initialization
    void Start () {
        int timeRandomizer = Random.Range(10, 30);
        Invoke("MakeCoin", timeRandomizer);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void MakeCoin() {
        // Random coin
        int coinRandomizer = Random.Range(0, 6);

        GameObject Clone;
        float y = Random.Range(-2.73f, 2.93f);
        float x = 14.86f;

        Clone = (Instantiate(coinPrefab, new Vector3(x, y), Quaternion.identity)) as GameObject;

        Clone.GetComponent<SpriteRenderer>().sprite = coinSprites[coinRandomizer];

        float timeRandomizer = Random.Range(10f, 30f);

        //if (timeRandomizer%2 == 0)
        //{
        //    timeRandomizer++;
        //}
        Invoke("MakeCoin", timeRandomizer);
    }
}
