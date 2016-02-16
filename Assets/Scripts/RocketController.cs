using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {

	public float rocketForce = 75.0f;
	private ParticleSystem ps;

	void Start () {
		ps = (ParticleSystem) gameObject.transform.GetChild (0).gameObject.GetComponent<ParticleSystem>();
		//ps = r.GetComponent<ParticleSystem> ();
	}

	void Update () {
	
	}

	void FixedUpdate () 
	{
		rocketMovement ();
		
		// Rotate rocket
		Vector3 vel = GetComponent<Rigidbody2D>().velocity;
		if (vel.y < 0)
			rotateDown ();

		// Make sure rocket doesn't rotate incorrectly
		checkRotation();

		// Make sure rocket doesn't go backwards
		checkVelocity();
	}

	void rocketMovement() {
		// Make rocket fly when LMB is pressed
		bool rocketActive = false;
		rocketActive = Input.GetButton("Fire1");
		/*
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				rocketActive = true;
			} else if((Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)) {
				rocketActive = false;
			}
		}*/

		if (rocketActive) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, rocketForce));
			rotateUp ();
			activateAfterburner (true);
		} else {
			activateAfterburner (false);
		}
	}

	void activateAfterburner(bool activate) {
		if (activate) {
			ps.Play ();
		} else {
			ps.Stop ();
		}
	}

	void checkVelocity() {
		Vector3 vel = GetComponent<Rigidbody2D>().velocity;
		vel.x = 0;
		GetComponent<Rigidbody2D> ().velocity = vel;
	}

	void checkRotation() {
		var rotationVector = transform.rotation.eulerAngles;
		if (rotationVector.z > 310) {
			rotationVector.z = 310;
		} else if (rotationVector.z < 230) {
			rotationVector.z = 230;
		}
		transform.rotation = Quaternion.Euler (rotationVector);
	}

	void rotateUp() {
		var rotationVector = transform.rotation.eulerAngles;
		print (rotationVector.z); // Delete afterwards

		if (rotationVector.z > 310)
			return;
		
		rotationVector.z += 5;
		transform.rotation = Quaternion.Euler (rotationVector);
	}

	void rotateDown() {
		var rotationVector = transform.rotation.eulerAngles;

		if (rotationVector.z < 230)
			return;

		rotationVector.z -= 1;
		transform.rotation = Quaternion.Euler (rotationVector);
	}
}
