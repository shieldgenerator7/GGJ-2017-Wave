using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInput : MonoBehaviour {

	public int VolBoost;
	public float vol;
	public int pitch;
	public int time;
	public string mic;
	AudioSource audioBit;
	public int sampleSize;
	public float[] sample;
	public MouseController Mouser;
	public GameObject mouseNote;
	public int range;


	// Use this for initialization
	public void Start () {
		VolBoost = 8000;
		sampleSize = 100;
		float[] sample = new float[sampleSize];
		audioBit = GetComponent<AudioSource>();
		mic = Microphone.devices[0];
		if(Microphone.IsRecording(mic)==true){
			Microphone.End(mic);
		}
		audioBit.clip = Microphone.Start(mic, true, 3600,44100);
		audioBit.loop = true;
	//	audioBit.mute = true; //avoid speaker feedback
		while (!(Microphone.GetPosition(null)>0)){
			audioBit.Play();
		}

		InvokeRepeating ("AudioFire", .5f, .1f);
	}

	// Update is called once per frame
	void Update ()
	{
		


	}	
	float AverageVol(){
		float snip = 0f;
		GetComponent<AudioSource>().GetOutputData (sample, 0);
		foreach (float i in sample) {
			snip += Mathf.Abs (i);
		}
		return snip / sampleSize;
	}

	void AudioFire(){
		audioBit = GetComponent<AudioSource> ();
		time = Microphone.GetPosition (mic);
		vol = AverageVol()*VolBoost;
		pitch = Mathf.RoundToInt(vol);


		if ((1 < pitch & pitch < 40 )||(pitch == 40)) {
			range = 1;
		} else if ((40 < pitch & pitch < 80 )||(pitch == 80)) {
			range = 2;
		} else if ((80 < pitch & pitch < 120 )||(pitch == 120)) {
			range = 3;
		} else if ((120 < pitch & pitch < 160 )||(pitch == 160)) {
			range = 4;
		} else if ((160 < pitch & pitch < 200 )||(pitch == 200)) {
			range = 5;
		} else if ((200 < pitch & pitch < 240 )||(pitch == 240)) {
			range = 6;
		} else if ((240 < pitch & pitch < 280 )||(pitch == 280)) {
			range = 7;
		} else if ((280 < pitch & pitch < 320 )||(pitch == 320)) {
			range = 8;
		} else if ((320 < pitch & pitch < 360 )||(pitch == 360)) {
			range = 9;
		}
		switch (range) {
		case 9:
			mouseNote = GameObject.Find ("MouseE");
			mouseNote.GetComponent<MouseController> ().SpawnToy ();
			break;
		case 8:
			mouseNote = GameObject.Find ("MouseF");
			mouseNote.GetComponent<MouseController> ().SpawnToy ();
			break;
		case 7:
			mouseNote = GameObject.Find ("MouseG");
			mouseNote.GetComponent<MouseController> ().SpawnToy ();
			break;
		case 6:
			mouseNote = GameObject.Find ("MouseA");
			mouseNote.GetComponent<MouseController> ().SpawnToy ();
			break;
		case 5:
			mouseNote = GameObject.Find ("MouseB");
			mouseNote.GetComponent<MouseController> ().SpawnToy ();
			break;
		case 4:
			mouseNote = GameObject.Find ("MouseC");
			mouseNote.GetComponent<MouseController> ().SpawnToy ();
			break;
		case 3:
			mouseNote = GameObject.Find ("MouseD");
			mouseNote.GetComponent<MouseController> ().SpawnToy ();
			break;
		case 2:
			mouseNote = GameObject.Find ("MouseEL");
			mouseNote.GetComponent<MouseController> ().SpawnToy ();
			break;
		case 1:
			mouseNote = GameObject.Find ("MouseFL");
			mouseNote.GetComponent<MouseController> ().SpawnToy ();
			break;
		default :
			Debug.Log (pitch);
			break;
		}

	}

}


