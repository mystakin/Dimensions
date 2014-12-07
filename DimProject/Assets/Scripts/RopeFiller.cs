using UnityEngine;
using System.Collections;

public class RopeFiller : MonoBehaviour {

	public GameObject ropeContainer;
	public GameObject ropeFiller;
	public Sprite bottomSprite;
	RopeManager ropeManager;

	Vector2 startingPos;
	Vector2 endingPos;

	void Awake(){
		startingPos = transform.position;
		endingPos = new Vector2( startingPos.x, startingPos.y - 1f );
		
		ropeManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<RopeManager>();
		ropeContainer = GameObject.FindGameObjectWithTag("RopeContainer");
	}

	// Use this for initialization
	void Start () {
		ropeManager.ropeList.Add(gameObject);

		transform.parent = ropeContainer.transform;

		StartCoroutine(FillOutRope());
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	IEnumerator FillOutRope(){
		for( float x = 0; x < 1; x += 0.25f ){
			rigidbody2D.MovePosition( Vector2.Lerp( startingPos, endingPos, x ) );

			yield return new WaitForFixedUpdate();
		}


		RaycastHit2D hitInfo = Physics2D.Raycast( (Vector2)endingPos - Vector2.up, -Vector2.up );

		if( hitInfo.distance != 0f && ropeManager.ropeList.Count < 10 ){
			Instantiate( (Object)ropeFiller, transform.position, transform.rotation);
		}
		else{
			GetComponent<SpriteRenderer>().sprite = bottomSprite;
		}
	}
}

















