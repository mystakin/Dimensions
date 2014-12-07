using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

	public static int gameState = 4; // 0 = game over, 1 = gameplay, 2 = cutscene, 3 = Victory!, 4 = Title Screen
	public static int currentCheckpoint = 0; // 0 = start, 1 = rope room, 2 = rope shrine, 3 = flip room, 4 = flip shrine, 5 = final ascent
	public static int abilities = 0; // 1 = jump, 2 = rope, 3 = flip
	public static float masterVolume = .3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( gameState == 3 ){
			if( Input.GetKey(KeyCode.Space) || Input.GetKey (KeyCode.Escape) || Input.GetKey(KeyCode.Return) ){
				Application.Quit();
			}
		}
	}
}
