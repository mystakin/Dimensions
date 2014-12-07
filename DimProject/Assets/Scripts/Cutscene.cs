using UnityEngine;
using System.Collections;

public class Cutscene : MonoBehaviour {

	public AudioClip powerUpSFX;
	public MusicManager music;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D col ){
		if( col.gameObject.tag == "Player" ){
			GetComponent<Collider2D>().enabled = false;
			Global.gameState = 2;
			StartCoroutine( CollectionCutscene( col.gameObject ) );
		}
	}

	IEnumerator CollectionCutscene( GameObject player ){
		music.TurnDown();

		AudioSource.PlayClipAtPoint( powerUpSFX, transform.position, Global.masterVolume );

		Vector2 startPos = transform.position;
		Vector2 endPos = (Vector2)transform.position + new Vector2( 0f, 2f );

		for( float x = 0; x < 1; x += 0.025f ){
			transform.position = Vector2.Lerp( startPos, endPos, x );

			yield return new WaitForSeconds(0.025f);
		}

		startPos = transform.position;
		endPos = (Vector2)transform.position + new Vector2( 0f, -3f );

		for( float x = 0; x < 1; x += 0.1f ){
			transform.position = Vector2.Lerp( startPos, endPos, x );
			
			yield return new WaitForSeconds(0.01f);
		}

		GetComponent<SpriteRenderer>().enabled = false;

		yield return new WaitForSeconds(1f);

		Global.gameState = 1;
		Global.abilities++;

		music.TurnUp();
	}
}




















