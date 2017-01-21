using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyChecker : MonoBehaviour {

    public float initialY;

	// Use this for initialization
	void Start () {
	}

    public void init(Vector3 pos)
    {
        transform.position = pos;
        initialY = pos.y;
    }
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < initialY - 0.4f)
        {
            Destroy(gameObject);
        }
    }
}
