using UnityEngine;
using System.Collections;

public class MoveCoin : MonoBehaviour {
    public Vector2 velocity = new Vector2(-3, 0);
    private Rigidbody2D rigidbody2D;
    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = velocity;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
