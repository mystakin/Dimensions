﻿using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public int checkpointNumber;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D col ){
		if( col.gameObject.tag == "Player" ){
			if( checkpointNumber > Global.currentCheckpoint ){
				Global.currentCheckpoint = checkpointNumber;
			}
		}
	}
}
