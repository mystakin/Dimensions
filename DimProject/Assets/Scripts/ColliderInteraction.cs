using UnityEngine;
using System.Collections;

public class ColliderInteraction : MonoBehaviour {
	
	InputManager inputManager;
	Health health;

	// Use this for initialization
	void Start () {
		inputManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
		health = GetComponent<Health>();
	}

	void LateUpdate(){

	}

	// Update is called once per frame
	void OnCollisionEnter2D ( Collision2D col ) {
		if( inputManager.flip < 0 ){
			if( col.contacts[0].point.y > transform.position.y && Mathf.Abs(col.contacts[0].point.x - transform.position.x) < 0.4f ){
				if( inputManager.frameMovement.y > 0 )
					inputManager.frameMovement = new Vector2( inputManager.frameMovement.x, 0f );
			}
		}
		else{
			if( col.contacts[0].point.y < transform.position.y ){
				if( inputManager.frameMovement.y < 0 )
					inputManager.frameMovement = new Vector2( inputManager.frameMovement.x, 0f );
			}
		}


	}

	void OnCollisionStay2D( Collision2D col ){

	}

	void OnCollisionExit2D( Collision2D col ){

	}

	void OnTriggerStay2D( Collider2D col ){
		if ( col.gameObject.tag == "Rope" ){
			inputManager.nearRope = true;
		}

		if( col.gameObject.tag == "Killbox" ){
			health.Killbox();
		}
	}

	void OnTriggerExit2D( Collider2D col ){
		if ( col.gameObject.tag == "Rope" ){
			inputManager.nearRope = false;
		}
	}
}





















