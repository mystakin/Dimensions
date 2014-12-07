using UnityEngine;
using System.Collections;

public class OverlayVerticality : MonoBehaviour {

	public float[] cameraYPos;
	public float[] cameraXPos;
	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float playerPosY = player.transform.position.y;
		float playerPosX = player.transform.position.x;

		if( playerPosY > cameraYPos[0] ){
			playerPosY = cameraYPos[0];
		}
		else if( playerPosY < cameraYPos[1] ){
			playerPosY = cameraYPos[1];
		}

		if( playerPosX < cameraXPos[0] + Camera.main.transform.position.x ){
			playerPosX = cameraXPos[0] + Camera.main.transform.position.x;
		}
		else if( playerPosX > cameraXPos[1] + Camera.main.transform.position.x ){
			playerPosX = cameraXPos[1] + Camera.main.transform.position.x;
		}

		transform.position = new Vector2( playerPosX, playerPosY );
	}
}
