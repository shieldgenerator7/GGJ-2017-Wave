using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour {

    public float speed = 2;//the rate the cat moves across the floor towards the mouse holes
    public int laneId;//the lane that the cat will be in. This determines note of this cat

    public const int FINISH_LINE = -4;//the x coordinate of the finish line of the cats, where the mouse holes are
    
	// Use this for initialization
	void Start () {
		
	}

    public void init(float speed, int laneId)
    {
        this.speed = speed;
        this.laneId = laneId;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(-speed*Time.deltaTime, 0);//move kitty
        if (transform.position.x < FINISH_LINE)
        {
            //GameManager.levelFailed();
        }
	}
}
