using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteRecorder : MonoBehaviour {
    //this script records which cat gets hit, when

    public static List<Note> notes = new List<Note>();

	// Use this for initialization
	void Start () {
		
	}

    public static void recordNote(int laneId)
    {
        notes.Add(new Note(laneId, Time.time));
    }
}

public class Note
{
    public int laneId;//which lane the note is in (which note it is)
    public float time;//what time the note was pressed

    public Note(int laneId, float time)
    {
        this.laneId = laneId;
        this.time = time;
    }
}
