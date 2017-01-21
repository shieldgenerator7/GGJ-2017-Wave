using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour {

    public float speed = 2;//the rate the cat moves across the floor towards the mouse holes
    public int laneId;//the lane that the cat will be in. This determines note of this cat
    public bool retreating = false;//whether the cat has been hit and is retreating

    public const int FINISH_LINE = -4;//the x coordinate of the finish line of the cats, where the mouse holes are
    public const int RETREAT_LINE = 7;//the x coordinate when the cat goes off the screen
    
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
        else if (retreating && transform.position.x > RETREAT_LINE)
        {
            Destroy(gameObject);//kitty has successfully retreated
        }
	}

    public void takeHit()//kitty has toy, so it retreats
    {
        retreating = true;
        speed *= -2;
    }
}
