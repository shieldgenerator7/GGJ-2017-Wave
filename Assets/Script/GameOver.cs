using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp (0)){
			Vector2 pos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitIt = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (pos), Vector2.zero);
			Debug.Log(hitIt.transform.gameObject.name);
			if (hitIt) {
				if (hitIt.transform.gameObject.name == "GameOver")
					SceneManager.LoadScene ("Title", LoadSceneMode.Single);

			}
		}
	}
}
