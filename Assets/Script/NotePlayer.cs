using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePlayer : MonoBehaviour {
    //this class plays back the notes recorded by the NoteRecorder
    //even though these methods are static, in order to work, 
    //they still require an instance of this script and an AudioSource to be added to an object (preferably the GameManager)

    public static bool playing = false;
    public static float startTime;//the time the playback was started (actually the difference between that and the time of the first note's recording
    public static int currentIndex = 0;//the current note that's being played

    private AudioSource mew;

    // Use this for initialization
    void Start () {
        mew = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playing)
        {
            Note note = NoteRecorder.notes[currentIndex];
            if (note.time + startTime < Time.time)//if it's time to play this note
            {
                mew.pitch = CatController.getPitch(note.laneId);
                mew.Play();
                currentIndex++;
                if (currentIndex >= NoteRecorder.notes.Count)
                {
                    playback(false);
                }
            }
        }		
	}

    public static void playback(bool play)
    {
        playing = play;
        if (play)
        {
            currentIndex = 0;
            startTime = Time.time - NoteRecorder.notes[0].time;
            startTime += 1.0f;//add a delay (seconds)
        }
    }
}
