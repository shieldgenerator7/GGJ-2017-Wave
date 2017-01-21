using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantData : MonoBehaviour {

	//This tracks the saved scores and recent played song

	public int thisSong;
	public int thisScore;

	public int HighRandom;
	public int HighNyanCat;
	public int High5th;
	public bool firstPlay = true;

	// Use this for initialization
	void Start () {
		if(firstPlay){
			if (thisSong == null)
				thisSong = 0;
			
			if (HighRandom == null)
				HighRandom = 0;
			if (HighNyanCat == null)
				HighNyanCat = 0;
			if (High5th == null)
				High5th = 0;
			firstPlay = false;
		}
	}
	public int SwitchSong(){
		thisSong++;
		if (thisSong > 2)
			thisSong = 0;
		return thisSong;
	}

	public int ReturnSong(){
		return thisSong;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp (0)){
			Vector2 pos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitIt = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (pos), Vector2.zero);
			Debug.Log(hitIt.transform.gameObject.name);
			if (hitIt) {
				if (hitIt.transform.gameObject.name == "SplashScreen")
					SceneManager.LoadScene ("Title", LoadSceneMode.Single);

			}
		}
	}
	void Awake(){
		
		DontDestroyOnLoad (this);
	}
}
