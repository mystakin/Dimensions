using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioSource audioManager;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void TurnOn(){
		audio.volume = Global.masterVolume - .1f;
		audio.Play();
	}

	public void TurnDown(){
		StartCoroutine( VolRoutine( 0f ) );
	}

	public void TurnUp(){
		StartCoroutine( VolRoutine( Global.masterVolume - .1f ) );
	}
	
	IEnumerator VolRoutine( float vol ){
		for( float x = 0f; x < 1; x += (Time.fixedDeltaTime / 2) ){
			float originalVol = audioManager.volume;
			
			audioManager.volume = Mathf.Lerp( originalVol, vol, x );
			
			yield return new WaitForFixedUpdate();
		}
	}
}






















