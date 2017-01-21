using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float CEILING = 5;//the ceiling off which all lanes "hang off" in terms off finding their y coordinates

    public static bool gameInProgress = true;

    private static GameManager instance;
    private CatSpawner catSpawner;//the script that spawns cats

	// Use this for initialization
	void Start () {
        instance = this;
        catSpawner = GetComponent<CatSpawner>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!gameInProgress)
            {
                resetLevel();
            }
        }
    }

    public static void resetLevel()
    {
        gameInProgress = true;
        instance.catSpawner.spawnCats(true);
        //despawn all cats
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (go.tag == "Cat")
            {
                Destroy(go);
            }
        }
    }

    public static void levelFailed()
    {
        gameInProgress = false;
        instance.catSpawner.spawnCats(false);
    }

    //
    // Returns the y position of the lane with the given id
    // Ids range from 1 to 9. 1 is top, 9 is bottom
    //
    public static float getLanePositionY(int laneId)
    {
        return instance.CEILING - laneId;
    }
}
