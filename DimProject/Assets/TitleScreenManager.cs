using UnityEngine;
using System.Collections;

public class TitleScreenManager : MonoBehaviour {

	public SpriteRenderer titleScreen;
	public AudioSource titleMusic;
	public MusicManager music;

	// Use this for initialization
	void Awake () {
		Screen.SetResolution( 1081, 608, false );
		titleMusic.volume = Global.masterVolume;
		titleMusic.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if( Global.gameState == 4 ){
			if( Input.GetKey(KeyCode.Space) || Input.GetKey (KeyCode.Escape) || Input.GetKey(KeyCode.Return) ){
				StartCoroutine( StartGame() );
				Global.gameState = 5;
			}
		}
	}

	IEnumerator StartGame(){
		for( float x = 0f; x < 1; x += Time.fixedDeltaTime ){
			float originalVol = titleMusic.volume;
			
			titleMusic.volume = Mathf.Lerp( originalVol, 0f, (x / 2) );
			titleScreen.color = Color.Lerp( Color.white, new Color(0f, 0f, 0f, 0f) , x);

			yield return new WaitForFixedUpdate();
		}

		music.TurnOn();
		Global.gameState = 1;
	}
}
























