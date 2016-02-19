using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class RocketController : MonoBehaviour {

	public float rocketForce = 75.0f;
	private ParticleSystem ps;
	public GameObject explosion;
    public AudioClip burnSound;
    public AudioClip coinSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
	private bool dead = false;

	void Start () {
		ps = (ParticleSystem) gameObject.transform.GetChild (0).gameObject.GetComponent<ParticleSystem>();
        //ps = r.GetComponent<ParticleSystem> ();
        source = GetComponent<AudioSource>();
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
		if (dead)
			return;

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
            source.PlayOneShot(burnSound, 1F);
		} else {
            source.Stop();
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
		//print (rotationVector.z); // Delete afterwards

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

    // Die by collision
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.tag.Equals("Ceiling") && !other.gameObject.tag.Equals("Coin")) {
            StartCoroutine(Die());
        }
        else if(other.gameObject.tag.Equals("Coin"))
        {
            //source.clip = coinSound;
            //source.Play();
            Destroy(other.gameObject);
            UpdateScore.score += 10;
        }
    }

	IEnumerator Die()
    {
		if (dead)
			yield break;

		dead = true;
		Vector3 pos = transform.position;
		GameObject exp_instance = (GameObject) Instantiate(explosion, pos, Quaternion.identity);
        source.Stop();
		if (UpdateScore.score > PlayerPrefs.GetInt ("Highscore")) {
			PlayerPrefs.SetInt ("Highscore", UpdateScore.score);
			PlayerPrefs.SetInt ("BrokeHighscore", 1);
			SaveHighscore ();
		}
		yield return new WaitForSeconds(2.3f);
		Destroy (exp_instance);
		SceneManager.LoadScene ("StartMenu");
    }

	public void SaveHighscore() {
		BinaryFormatter BinForm = new BinaryFormatter(); 
		FileStream file;

		if (File.Exists (Application.persistentDataPath + "/gameInfo.dat")) {
			file = File.Open (Application.persistentDataPath + "/gameInfo.dat", FileMode.Open); // if the file already exists it opens that file
		}
		else {
			file = File.Create(Application.persistentDataPath + "/gameInfo.dat"); //creates a file
		}

		BinForm.Serialize (file, UpdateScore.score);
		file.Close();
	}
}
