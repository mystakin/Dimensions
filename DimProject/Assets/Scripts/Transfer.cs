using UnityEngine;
using System.Collections;

public class Transfer : MonoBehaviour {

	public Vector3[] cameraPos = new Vector3[3];
	public int destination;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D col ){
		if( col.gameObject.tag == "Player" ){
			if( destination == 1 ){
				col.gameObject.transform.position += new Vector3( 74f, 0f, 0f );
				Camera.main.transform.position = cameraPos[1];
			}

			if( destination == -1 ){
				col.gameObject.transform.position += new Vector3( -76f, 0f, 0f );
				Camera.main.transform.position = cameraPos[2];
			}

			if( destination == 0 && transform.position.x > 28 ){
				col.gameObject.transform.position += new Vector3( -74f, 0f, 0f );
				Camera.main.transform.position = cameraPos[0];
			}

			if( destination == 0 && transform.position.x < 28 ){
				col.gameObject.transform.position += new Vector3( 76f, 0f, 0f );
				Camera.main.transform.position = cameraPos[0];
			}
		}
	}
}
