using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToySpawner : MonoBehaviour
{
    public Vector2 toySpawnVelocity = new Vector2(1000, 100);//the velocity at which the toys launch
    //Toy Prefabs
    public GameObject toyPrefab;
    public Sprite toy_bear;
    public Sprite toy_tuna;
    public Sprite toy_duck;
    public Sprite toy_milk;
    private List<Sprite> toySpriteList;
    private static int TOY_AMOUNT = 4;

    private static ToySpawner instance;

    // Use this for initialization
    void Start () {
        instance = this;
        toySpriteList = new List<Sprite>();
        toySpriteList.Add(toy_bear);
        toySpriteList.Add(toy_tuna);
        toySpriteList.Add(toy_duck);
        toySpriteList.Add(toy_milk);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void spawnToy(Vector3 startPos)
    {
        GameObject newToy = Instantiate(instance.toyPrefab);
        ToyChecker tc = newToy.GetComponent<ToyChecker>();
        tc.init(startPos);
        tc.GetComponent<Rigidbody2D>().AddForce(instance.toySpawnVelocity);
        int randSprite = Random.Range(0, TOY_AMOUNT - 1);
        newToy.GetComponent<SpriteRenderer>().sprite = instance.toySpriteList[randSprite];
    }
}
