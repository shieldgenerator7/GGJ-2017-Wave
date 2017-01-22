using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Level
{
    string sequence;//the sequence that tells which cats should spawn when
    int nextSpot = 0;//the spot of the next number to be retrieved

    //Loads a level sequence from the given filename
    public void loadLevel(string filename)
    {
        sequence = Resources.Load<TextAsset>(filename).text;
        nextSpot = 0;
    }

    public bool hasNextLane()
    {
        return nextSpot < sequence.Length;
    }

    public int getNextLane()
    {
        if (nextSpot == sequence.Length)
        {
            return -1;//no lane to give out
        }
        int nextLane = int.Parse(sequence.Substring(nextSpot,1));
        nextSpot++;
        return nextLane;
    }
}
