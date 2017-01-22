using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTongueController : MonoBehaviour {

    private static CatTongueController instance;
    
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void setPosition(int laneId)
    {
        float high = -1.7f;//the highest pitch on the scale
        float low = -2.23f;//the lowest pitch on the scale
        int bottomLane = 9;
        int topLane = 1;
        float newY = (high + low) - (((laneId - topLane) * (high - low) / (bottomLane - topLane)) + low);
        instance.transform.position = new Vector3(instance.transform.position.x, newY);
    }
}
