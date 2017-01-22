using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToySpawner : MonoBehaviour
{
    public Vector2 toySpawnVelocity = new Vector2(1000, 100);//the velocity at which the toys launch
    //Toy Prefabs
    public GameObject toyPrefab;
    public Sprite toy_bear;
    public Sprite toy_bunny;
    public Sprite toy_butterfly;
    public Sprite toy_duck;
    public Sprite toy_fish;
    public Sprite toy_milk;
    public Sprite toy_mouse;
    public Sprite toy_tuna;
    public Sprite toy_yarn;
    private List<Sprite> toySpriteList;
    private static int TOY_AMOUNT = 9;
    private static List<string> colorOptions = new List<string>();

    private static ToySpawner instance;

    // Use this for initialization
    void Start () {
        instance = this;
        toySpriteList = new List<Sprite>();
        //Toy Options
        toySpriteList.Add(toy_bear);
        toySpriteList.Add(toy_bunny);
        toySpriteList.Add(toy_butterfly);
        toySpriteList.Add(toy_duck);
        toySpriteList.Add(toy_fish);
        toySpriteList.Add(toy_milk);
        toySpriteList.Add(toy_mouse);
        toySpriteList.Add(toy_tuna);
        toySpriteList.Add(toy_yarn);
        //Color Options
        colorOptions.Add("#FFEB56");
        colorOptions.Add("#FF7700");
        colorOptions.Add("#FCFCFC");
        colorOptions.Add("#674BCC");
        colorOptions.Add("#FF6060");
        colorOptions.Add("#D88F3C");
        colorOptions.Add("#BCBCBC");
        colorOptions.Add("#00A2FF");
        colorOptions.Add("#00A010");
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
        int randSprite = Random.Range(0, TOY_AMOUNT);
        newToy.GetComponent<SpriteRenderer>().sprite = instance.toySpriteList[randSprite];
        //Random Color
        //int randomColor = Random.Range(0, colorOptions.Count);
        //Color color = new Color();
        //ColorUtility.TryParseHtmlString(colorOptions[randomColor], out color);
        //newToy.GetComponent<SpriteRenderer>().color = color;
    }
}
