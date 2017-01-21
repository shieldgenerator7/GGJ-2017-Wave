using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleControl : MonoBehaviour {

	// Use this for initialization

	public GameObject HighScore;
	public GameObject ButtRandom;
	public GameObject ButtNyanCat;
	public GameObject Butt5th;
	public int songSelect = 0;

	public PersistantData Keeping;


	void Start () {
		Keeping = Object.FindObjectOfType<PersistantData>();
		songSelect = Keeping.ReturnSong ();
		UpdateSong();
	}

	void UpdateSong(){
		ButtRandom.SetActive (false);
		ButtNyanCat.SetActive (false);
		Butt5th.SetActive (false);

		//random -> Nyan -> 5th -> random
		if (songSelect == 0)
			ButtRandom.SetActive (true);
		if (songSelect == 1)
			ButtNyanCat.SetActive (true);
		if (songSelect == 2)
			Butt5th.SetActive (true);
	}
	
	void PlayClicked(){
		//Debug.Log("PLAY");
		SceneManager.LoadScene ("Mousezart", LoadSceneMode.Single);
	}
	void ScoreClicked(){
		//Debug.Log("SCORE");
		SceneManager.LoadScene ("HighScore", LoadSceneMode.Single);
	}
	void LevelClicked(){
		//Debug.Log("LEVEL SWAP");


		songSelect = Keeping.SwitchSong ();
		Debug.Log (songSelect);
		UpdateSong();
	}


	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp (0)){
			Vector2 pos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitIt = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (pos), Vector2.zero);
			Debug.Log(hitIt.transform.gameObject.name);
			if (hitIt) {
				if (hitIt.transform.gameObject.name == "Play")
					PlayClicked ();
				else if (hitIt.transform.gameObject.name == "Score")
					ScoreClicked ();
				else if (hitIt.transform.gameObject.name == "Level")
					LevelClicked ();
				else if(hitIt.transform.gameObject.name == "Exit")
					Application.Quit();
			}

		}
	}
}
