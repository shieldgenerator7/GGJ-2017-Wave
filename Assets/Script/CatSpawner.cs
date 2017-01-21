using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    public float catSpawnDelay = 0.5f;//the minimum time between cat spawns (seconds)
    public GameObject catPrefab;//the prefab used to spawn cats

    private float nextCatSpawn = 0;//the soonest a cat can spawn
    private bool shouldSpawnCats = true;

    // Use this for initialization
    void Start()
    {

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
                CatController cc = Instantiate(catPrefab).GetComponent<CatController>();
                cc.init(Random.Range(1, 3), laneId);
            }
        }
    }

    public void spawnCats(bool spawn)
    {
        shouldSpawnCats = spawn;
    }
}
