using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    public float catSpawnDelay = 0.5f;//the minimum time between cat spawns (seconds)
    public GameObject catPrefab;//the prefab used to spawn cats

    private float nextCatSpawn = 0;//the soonest a cat can spawn
    private bool shouldSpawnCats = true;

    private List<string> colorOptions = new List<string>();

    // Use this for initialization
    void Start()
    {
        colorOptions.Add("#FFB37C");
        colorOptions.Add("#6D6D6D");
        colorOptions.Add("#FCFCFC");
        colorOptions.Add("#FFB249");
        colorOptions.Add("#6D6D86");
        colorOptions.Add("#F7F7DE");
        colorOptions.Add("#FFECB5");
        colorOptions.Add("#B74900");
        colorOptions.Add("#A0A0A0");
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSpawnCats)
        {
            if (nextCatSpawn < Time.time)
            {
                nextCatSpawn = Time.time + catSpawnDelay;
                //int randLane = Random.Range(1, 9);
                int laneId = GameManager.level.getNextLane();
                if (laneId < 1 || laneId > 9)  
                {
                    if (laneId != 0)
                    {
                        Debug.Log("Invalid laneId: " + laneId);//0 is valid (end of level), but anything else is invalid
                    }
                    spawnCats(false);
                    return;
                }
                GameObject newCat = Instantiate(catPrefab);
                CatController cc = newCat.GetComponent<CatController>();
                cc.init(1.5f, laneId);// Random.Range(1, 3), laneId);//
                int randomColor = Random.Range(0, colorOptions.Count);
                Color color = new Color();
                ColorUtility.TryParseHtmlString(colorOptions[randomColor], out color);
                newCat.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    public void spawnCats(bool spawn)
    {
        shouldSpawnCats = spawn;
    }
}
