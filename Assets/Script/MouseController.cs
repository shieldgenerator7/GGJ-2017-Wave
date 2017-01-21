using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public GameObject toyPrefab;//the prefab for the toy this mouse spawns
    public int laneId;//the id of the lane this mouse occupies
    public float toySpawnDelay = 0.5f;//the minimum time between toy spawns (seconds)
    public string buttonName;//the name of the button that controls this mouse
    public GameObject tapArea;//the tap area that this mouse responds to

    private float nextToySpawn = 0;//the soonest this mouse can spawn a toy

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(transform.position.x, GameManager.getLanePositionY(laneId));
	}
	
	// Update is called once per frame
	void Update () {
        //Detect Inputs
        bool buttonInput = Input.GetButton(buttonName);
        bool mouseInput = false;
        if (Input.GetMouseButton(0))
        {
            Vector3 camPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (tapArea.GetComponent<BoxCollider2D>().OverlapPoint(camPoint))
            {
                mouseInput = true;
            }
        }
        bool touchInput = false;
        if (Input.touchCount > 0)
        {
            for (int i=0; i<Input.touchCount; i++)
            {
                Vector3 camPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                if (tapArea.GetComponent<BoxCollider2D>().OverlapPoint(camPoint))
                {
                    touchInput = true;
                    break;
                }
            }
        }
        //Spawn Toy
		if (buttonInput || mouseInput || touchInput)
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
