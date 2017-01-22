using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour {

    public float CEILING = 5.0f;//the ceiling off which all lanes "hang off" in terms off finding their y coordinates

    public static bool gameInProgress = true;
    public static bool betweenLevels = false;
    public static bool gameWon = false;//set to true after all levels have been completed
    public string levelListFileName = "levellist";
    public string levelDirectory = "Levels/";

    private static GameManager instance;
    private CatSpawner catSpawner;//the script that spawns cats
    private static List<string> levelFileNames = new List<string>();//the names of the files that contain a level
    private static int currentLevelIndex = 0;//the index in the level list of the current level
    public static Level level;//the only level needed, because it can be reused
    private static float earliestNextLevelTrigger;//the earliest time the song playback can be skipped (to keep from accidentially skipping the playback)

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        Input.simulateMouseWithTouches = false;
        catSpawner = GetComponent<CatSpawner>();
        level = new Level();
        //Load level names
        TextAsset file = Resources.Load<TextAsset>(levelDirectory+ levelListFileName);
        foreach (string line in file.text.Split('\n'))
        {
            levelFileNames.Add(levelDirectory + line.Trim());
        }
        loadLevel(currentLevelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (!gameInProgress)
        {
            bool keyInput = Input.GetKeyDown(KeyCode.Return);
            bool mouseInput = Input.GetMouseButtonUp(0);
            bool touchInput = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
            if (keyInput || mouseInput || touchInput)
            {
                if (betweenLevels) 
                {
                    if (Time.time > earliestNextLevelTrigger) {
                        SceneManager.UnloadSceneAsync("LevelEnd");
                        NoteRecorder.reset();
                        NotePlayer.playback(false);
                        if (currentLevelIndex < levelFileNames.Count - 1)
                        {//another level to continue
                            gameInProgress = true;
                            loadLevel(currentLevelIndex + 1);
                            instance.catSpawner.spawnCats(true);
                        }
                        else
                        {//all levels done, game won!
                            gameWon = true;
                            betweenLevels = false;
                            SceneManager.LoadScene("GameWon", LoadSceneMode.Additive);
                        }
                    }
                }
                else if (gameWon)
                {//proceed to start screen
                    SceneManager.LoadScene("Title", LoadSceneMode.Single);
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
        NotePlayer.playback(true);
        earliestNextLevelTrigger = Time.time + 2.0f;//put a delay on how long until you can go to next level (seconds)
    }

    public static void levelFailed()
    {
		Monetizr.Instance.ShowProductWithID("9926388170");
        
        gameInProgress = false;
        instance.catSpawner.spawnCats(false);
		SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
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
