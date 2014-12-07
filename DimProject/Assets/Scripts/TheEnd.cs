using UnityEngine;
using System.Collections;

public class TheEnd : MonoBehaviour {

	public SpriteRenderer thanks;
	public SpriteRenderer fade;
	public Vector3 overlayPos;
	public bool displayThanks;
	public MusicManager music;
	public AudioClip victory;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D col ){
		if( Global.gameState == 1 && col.gameObject.tag == "Player" ){
			Global.gameState = 3;
			StartCoroutine( FadeOut() );
			StartCoroutine( Thanks() );
		}

	}

	IEnumerator FadeOut(){
		for( float x = 0f; x < 1f; x += Time.fixedDeltaTime ){
			fade.color = Color.Lerp( new Color(0f, 0f, 0f, 0f), Color.white, x);

			yield return new WaitForFixedUpdate();
		}

		yield return new WaitForSeconds(1f);

		displayThanks = true;
	}

	IEnumerator Thanks(){
		while( !displayThanks ){
			yield return new WaitForFixedUpdate();
		}

		music.TurnDown();

		for( float x = 0f; x < 1f; x += Time.fixedDeltaTime ){
			thanks.color = Color.Lerp( new Color(0f, 0f, 0f, 0f), Color.white, x);
			
			yield return new WaitForFixedUpdate();
		}

		AudioSource.PlayClipAtPoint( victory, transform.position, Global.masterVolume + .1f );
	}
}


















