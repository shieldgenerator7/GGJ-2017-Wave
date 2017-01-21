using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public GameObject toyPrefab;//the prefab for the toy this mouse spawns
    public int laneId;//the id of the lane this mouse occupies

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("NoteE"))
        {
            //transform.position += new Vector3(1, 0);
            GameObject newToy = Instantiate(toyPrefab);
            ToyChecker tc = newToy.GetComponent<ToyChecker>();
            tc.init(transform.position);
            tc.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 100));
        }
	}
}
