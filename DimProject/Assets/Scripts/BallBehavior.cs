using UnityEngine;
using System.Collections;

public class BallBehavior : MonoBehaviour {

	GameObject player;
	public float speed;
	public float lifeCounter;
	public AudioClip shotSFX;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		AudioSource.PlayClipAtPoint( shotSFX, transform.position, Global.masterVolume );

		rigidbody2D.AddForce( ((Vector2)player.transform.position - (Vector2)transform.position).normalized * speed );
	}
	
	// Update is called once per frame
	void Update () {
		lifeCounter += Time.deltaTime;

		if( lifeCounter > 4f ){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D( Collider2D col ) {
		if( col.tag == "Player" ){
			player.GetComponent<Health>().TakeDamage();
			Destroy(gameObject);
		}
	}
}
