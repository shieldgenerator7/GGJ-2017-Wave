using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInput : MonoBehaviour {

	public int VolBoost = 100;
	public float vol;
	public int pitch;
	public int time;
	public string mic;
	AudioSource audioBit;
	public float[] sample = new float[20];




	// Use this for initialization
	public void Start () {
		
		audioBit = GetComponent<AudioSource>();
		mic = Microphone.devices[0];
		audioBit.clip = Microphone.Start(mic, true, 3600,44100);

		audioBit.loop = true;
	//	audioBit.mute = true; //avoid speaker feedback

		while (!(Microphone.GetPosition(null)>0)){
			audioBit.Play();

		}

	}

	// Update is called once per frame
	void Update ()
	{
		audioBit = GetComponent<AudioSource> ();
		time = Microphone.GetPosition (mic);
		vol = AverageVol()*VolBoost;
		pitch = Mathf.RoundToInt(vol);


	}	
	float AverageVol(){
		int size = 200;
		float snip = 0f;
		GetComponent<AudioSource>().GetOutputData (sample, 0);
		foreach (float i in sample) {
			
			snip += Mathf.Abs (i);
		}
		return snip / size;
	}

}


