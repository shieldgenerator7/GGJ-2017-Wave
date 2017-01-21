using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public GameObject toyPrefab;//the prefab for the toy this mouse spawns
    public int laneId;//the id of the lane this mouse occupies
    public float toySpawnDelay = 0.5f;//the minimum time between toy spawns (seconds)

    private float nextToySpawn = 0;//the soonest this mouse can spawn a toy

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("NoteE"))
        {
            if (nextToySpawn < Time.time)
            {
                nextToySpawn = Time.time + toySpawnDelay;
                GameObject newToy = Instantiate(toyPrefab);
                ToyChecker tc = newToy.GetComponent<ToyChecker>();
                tc.init(transform.position);
                tc.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 100));
            }
        }
	}
}
