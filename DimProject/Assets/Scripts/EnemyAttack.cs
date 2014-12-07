using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public bool alive;
	public bool dead;
	public float countdown;
	public float countdownMax;
	public GameObject player;
	public GameObject ball;
	public AudioClip killSFX;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if( Vector2.Distance( (Vector2)transform.position, (Vector2)player.transform.position ) < 10f ){
			alive = true;
		}
		else{
			alive = false;
		}

		if ( countdown <= 0 ){
			countdown = 0;
		}
		else{
			countdown -= Time.deltaTime;
		}

		if( alive && !dead && countdown == 0 ){
			Instantiate( (Object)ball, transform.position, transform.rotation);
			countdown = countdownMax;
		}

		if( dead && Vector2.Distance( (Vector2)transform.position, (Vector2)player.transform.position ) > 30f ){
			dead = false;
			
			GetComponent<SpriteRenderer>().enabled = true;
			GetComponent<Collider2D>().enabled = true;

		}
	}

	void OnCollisionEnter2D( Collision2D col ){
		if ( col.gameObject.tag == "Player" ){
			if ( col.gameObject.transform.position.y > transform.position.y + 0.5f ){
				AudioSource.PlayClipAtPoint( killSFX, transform.position, Global.masterVolume );

				alive = false;
				dead = true;

				GetComponent<SpriteRenderer>().enabled = false;
				GetComponent<Collider2D>().enabled = false;

				player.GetComponent<Health>().HealDamage();
			}
		}
	}
}





















