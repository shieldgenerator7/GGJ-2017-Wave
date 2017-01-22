using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour {

    public float CEILING = 5;//the ceiling off which all lanes "hang off" in terms off finding their y coordinates

    public static bool gameInProgress = true;
    public static bool betweenLevels = false;
    public string levelListFileName = "levellist.txt";
    public string levelDirectory = "Assets/Levels/";

    private static GameManager instance;
    private CatSpawner catSpawner;//the script that spawns cats
    private static List<string> levelFileNames = new List<string>();//the names of the files that contain a level
    private static int currentLevelIndex = 0;//the index in the level list of the current level
    public static Level level;//the only level needed, because it can be reused

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        catSpawner = GetComponent<CatSpawner>();
        level = new Level();
        //Load level names
        System.IO.StreamReader file = new System.IO.StreamReader(levelDirectory+levelListFileName);//2017-01-21: copied from https://msdn.microsoft.com/en-us/library/aa287535(v=vs.71).aspx
        string line;
        while ((line = file.ReadLine()) != null)
        {
            levelFileNames.Add(levelDirectory+line);
        }
        file.Close();
        loadLevel(currentLevelIndex);
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
                if (betweenLevels)
                {
                    SceneManager.UnloadSceneAsync("LevelEnd");
                    gameInProgress = true;
                    loadLevel(currentLevelIndex + 1);
                    instance.catSpawner.spawnCats(true);
                }
                else {
                    resetLevel();
                }
            }
        }
    }

    public static void loadLevel(int index)
    {
        currentLevelIndex = index;
        level.loadLevel(levelFileNames[index]);
    }

    public static void resetLevel()
    {
        SceneManager.LoadScene("Mousezart", LoadSceneMode.Single);
        loadLevel(currentLevelIndex);
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

    public static void checkLevelEnd()
    {
        if (level.hasNextLane())
        {
            return;//the level's not over yet
        }
        bool end = true;
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (go.tag == "Cat")
            {
                CatController cc = go.GetComponent<CatController>();
                if (!cc.retreating)
                {
                    end = false;
                }
            }
        }
        if (end)
        {
            levelWon();
        }
    }

    public static void levelWon()
    {
        gameInProgress = false;
        betweenLevels = true;
        SceneManager.LoadScene("LevelEnd", LoadSceneMode.Additive);
    }

    public static void levelFailed()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
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
