using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour {

	public static Text highscoreText;

	// Use this for initialization
	void Start () {
		highscoreText = GetComponent<Text>();
		LoadData ();
		highscoreText.text = "Highscore: " + PlayerPrefs.GetInt ("Highscore");
	}

	public void SaveData() {
		BinaryFormatter BinForm = new BinaryFormatter(); 
		FileStream file;

		if (File.Exists (Application.persistentDataPath + "/gameInfo.dat")) {
			file = File.Open (Application.persistentDataPath + "/gameInfo.dat", FileMode.Open); // if the file already exists it opens that file
		}
		else {
			file = File.Create(Application.persistentDataPath + "/gameInfo.dat"); //creates a file
		}

		int topscore = 100;
		BinForm.Serialize (file, topscore);
		file.Close();
	}

	public void LoadData() {
		if (File.Exists (Application.persistentDataPath + "/gameInfo.dat")) { 
			BinaryFormatter BinForm = new BinaryFormatter(); //creates a new variabe called "BinForm" that stores a "binary formatter" in charge of writing files to binary
			FileStream file = File.Open (Application.persistentDataPath + "/gameInfo.dat", FileMode.Open); // if the file already exists it opens that file
			int data = (int)BinForm.Deserialize(file); // deserialized the file and casts it to something we can understand (gamedata)binForm
			file.Close(); // closes file
			PlayerPrefs.SetInt("Highscore", data);
		}
	}
}
