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
	public int songSelect;

	public int HighRandom=0;
	public int HighNyanCat=0;
	public int High5th=0;


	void Start () {
		songSelect = 0;
	}
	
	void PlayClicked(){
		Debug.Log("PLAY");
		SceneManager.LoadScene ("Mousezart", LoadSceneMode.Single);
	}
	void ScoreClicked(){
		Debug.Log("SCORE");
	}
	void LevelClicked(){
		Debug.Log("LEVEL SWAP");
		//random -> Nyan -> 5th -> random

		if (ButtRandom.activeSelf == true) {
			ButtRandom.SetActive (false);
			ButtNyanCat.SetActive (true);
			Butt5th.SetActive (false);
			songSelect = 1;
		}
		else if (ButtNyanCat.activeSelf == true ){
			ButtRandom.SetActive (false);
			ButtNyanCat.SetActive (false);
			Butt5th.SetActive (true);
			songSelect = 2;
		}
		else if (Butt5th.activeSelf == true){
			ButtRandom.SetActive (true);
			ButtNyanCat.SetActive (false);
			Butt5th.SetActive (false);
			songSelect = 0;
		}
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
			}

		}
	}
}
