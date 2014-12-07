using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeManager : MonoBehaviour {

	public List<GameObject> ropeList = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ClearList(){
		foreach(GameObject gO in ropeList){
			Destroy(gO);
		}

		ropeList.Clear();
	}
}
