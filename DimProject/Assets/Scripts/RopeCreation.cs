using UnityEngine;
using System.Collections;

public class RopeCreation : MonoBehaviour {
	
	RopeManager ropeManager;

	public GameObject ropeContainer;
	public GameObject ropeFiller;
	public bool filled;
	public Sprite topSprite;
	public float speed;
	public AudioClip ropeHitSFX;

	// Use this for initialization
	void Awake(){
		ropeManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<RopeManager>();
		ropeContainer = GameObject.FindGameObjectWithTag("RopeContainer");
	}

	void Start () {
		transform.parent = ropeContainer.transform;

		ropeManager.ClearList();
		ropeManager.ropeList.Add(gameObject);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody2D.MovePosition( (Vector2)transform.position + new Vector2( 0f, speed ) * Time.fixedDeltaTime );
	}

	void OnTriggerEnter2D( Collider2D col ){
		if( (col.gameObject.tag != "Player" && col.gameObject.tag != "IgnoreCol" && col.gameObject.tag != "Respawn") && !filled ){
			AudioSource.PlayClipAtPoint( ropeHitSFX, transform.position, Global.masterVolume );
			speed = 0f;
			GetComponent<SpriteRenderer>().sprite = topSprite;
			Instantiate( (Object)ropeFiller, transform.position, transform.rotation);
			filled = true;
		}
			


	}
}















