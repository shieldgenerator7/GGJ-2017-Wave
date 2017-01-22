using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour {

    public float speed = 2;//the rate the cat moves across the floor towards the mouse holes
    public int laneId;//the lane that the cat will be in. This determines note of this cat
    public bool retreating = false;//whether the cat has been hit and is retreating
    public AudioClip mew;//the sound to play when hit

    public const int FINISH_LINE = -4;//the x coordinate of the finish line of the cats, where the mouse holes are
    public const int RETREAT_LINE = 7;//the x coordinate when the cat goes off the screen
    
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(transform.position.x, GameManager.getLanePositionY(laneId));
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
            GameManager.levelFailed();
        }
        else if (retreating && transform.position.x > RETREAT_LINE)
        {
            Destroy(gameObject);//kitty has successfully retreated
        }
	}

    public void takeHit()//kitty has toy, so it retreats
    {
        retreating = true;
        GetComponent<BoxCollider2D>().enabled = false;
        speed *= -2;
        NoteRecorder.recordNote(laneId);
        GameManager.checkLevelEnd();
        AudioSource mew = GetComponent<AudioSource>();
        float high = 2.0f;//the highest pitch on the scale
        float low = 0.5f;//the lowest pitch on the scale
        mew.pitch = (high+ low)-(((laneId-1)*(high-low)/8)+low);//convert (1 thru 9) to (0.5 thru 2)
        mew.Play();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Toy")
        {
            takeHit();
            Destroy(coll.gameObject);
        }
    }
}
