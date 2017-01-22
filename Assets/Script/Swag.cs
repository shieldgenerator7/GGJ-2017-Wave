using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Swag : MonoBehaviour {

	public Monetizr theMonetizr; 

	// Use this for initialization
	void Start () {
		theMonetizr = Object.FindObjectOfType<Monetizr>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp (0)){
			Vector2 pos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitIt = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (pos), Vector2.zero);
			Debug.Log(hitIt.transform.gameObject.name);
			if (hitIt) {
				if (hitIt.transform.gameObject.name == "Swag")
					Application.OpenURL ("http://instagramprinted.myshopify.com/products/copy-of-ggj-t-shirt?variant=36586019722");

			}
		}
	}
}
