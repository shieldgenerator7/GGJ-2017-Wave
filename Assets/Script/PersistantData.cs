using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour {


	public string song;
	public string score;
//	GameObject playSong = new GameObject[3];
//	GameObject songscore = new GameObject[1];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Awake(){

		DontDestroyOnLoad (this);
	}
}
