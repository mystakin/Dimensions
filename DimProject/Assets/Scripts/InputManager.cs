using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public Rigidbody2D playerRig;
	public CircleCollider2D playerCol;
	public float speed;
	public float gravity;
	public float gravityActive;
	public float gravityMax;
	public float jumpSpeed;
	public int flip;
	public bool onGround;
	public bool nearRope;
	public bool onRope;
	public Vector2 frameMovement;
	public GameObject ropePrefab;
	public float ropeCooldown;
	public AudioClip flipSFX;
	public AudioClip jumpSFX;
	public AudioClip ropeSFX;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if( Global.gameState == 1 ){
			GetHorizontal();
			GetVertical();
			GetJump();
			GetFlip();
			GetRope();
		}
		else if( Global.gameState != 1 ){
			frameMovement = Vector2.zero;
		}

		if( !nearRope ){
			onRope = false;
		}

		if( ropeCooldown <= 0 ){
			ropeCooldown = 0;
		}
		else{
			ropeCooldown -= Time.deltaTime;
		}
	}

	void FixedUpdate() {
		CalcGravity();
	}

	void LateUpdate(){
		playerRig.MovePosition( playerRig.position + frameMovement * Time.fixedDeltaTime );
	}

	void GetHorizontal(){
		if( Input.GetAxisRaw("Horizontal") > 0 ){
			frameMovement = new Vector2( speed, frameMovement.y );
		}
		
		if( Input.GetAxisRaw("Horizontal") < 0 ){
			frameMovement = new Vector2( -speed, frameMovement.y );
		}

		if( Input.GetAxisRaw("Horizontal") == 0 ){
			frameMovement = new Vector2( 0f, frameMovement.y );
		}
	}

	void GetVertical(){
		if( nearRope ){
			if( Input.GetAxisRaw("Vertical") > 0 ){
				onRope = true;
				frameMovement = new Vector2( frameMovement.x, speed );
				onGround = false;
			}

			if( Input.GetAxisRaw("Vertical") < 0 ){
				onRope = true;
				frameMovement = new Vector2( frameMovement.x, -speed );
				onGround = false;
			}

			if( Input.GetAxisRaw("Vertical") == 0 && onRope ){
				frameMovement = new Vector2( frameMovement.x, 0f );
			}
		}
	}

	void GetJump(){
		if ( Input.GetButtonDown("Jump") && onGround && Global.abilities >= 1 ){
			frameMovement = new Vector2( frameMovement.x, jumpSpeed * -flip );
			onGround = false;
			AudioSource.PlayClipAtPoint( jumpSFX, transform.position, Global.masterVolume );
		}
	}

	void GetFlip(){
		if( Input.GetButtonDown("Flip") && Global.abilities >= 3 ){
			AudioSource.PlayClipAtPoint( flipSFX, transform.position, Global.masterVolume );
			onGround = false;

			if( flip > 0 ){
				flip = -1;
			}
			else{
				flip = 1;
			}
		}
	}

	void GetRope(){
		if( Input.GetButtonDown("Rope") && ropeCooldown == 0 && Global.abilities >= 2 ){
			Instantiate( (Object)ropePrefab, playerRig.transform.position, playerRig.transform.rotation); 
			ropeCooldown = 1f;
			AudioSource.PlayClipAtPoint( ropeSFX, transform.position, Global.masterVolume );
		}
	}

	void CalcGravity(){
		float groundYPosLeft = 0f;
		float groundYPosRight = 0f;
		float playerPos = playerRig.transform.position.y;

		CalcRays( flip, out groundYPosLeft, out groundYPosRight );

		float contactDifLeft = playerPos - groundYPosLeft;
		float contactDifRight = playerPos - groundYPosRight;

		if( flip < 0 ){
			if ( contactDifLeft > -0.025f && contactDifRight > -0.025f ){
				EnactGravity();
				onGround = false;
			}
			else{
				if( frameMovement.y < 0 ){
					frameMovement = new Vector2( frameMovement.x, 0f );
					onGround = true;
				}
			}
		}
		else{
			if ( contactDifLeft < 0f && contactDifRight < 0f ){
				EnactGravity();
				onGround = false;
			}
			else{
				if( frameMovement.y > 0 ){
					frameMovement = new Vector2( frameMovement.x, 0f );
					onGround = true;
				}
			}
		}

	}

	void CalcRays( int flip, out float groundYPosLeft, out float groundYPosRight ){
		groundYPosLeft = 0f;
		groundYPosRight = 0f;
		
		Vector2 vectorOffsetLeft = new Vector2( -playerCol.radius + 0.05f, flip * (playerCol.radius + 0.1f)  );
		Vector2 vectorOffsetRight = new Vector2( playerCol.radius - 0.05f, flip * (playerCol.radius + 0.1f) );
		
		RaycastHit2D hitInfoLeft = Physics2D.Raycast( playerRig.position + vectorOffsetLeft, flip * (Vector2.up) );
		RaycastHit2D hitInfoRight = Physics2D.Raycast( playerRig.position + vectorOffsetRight, flip * (Vector2.up) );

		if( hitInfoLeft.collider != null )
			groundYPosLeft = (hitInfoLeft.collider.transform.position.y - flip);
		
		if( hitInfoRight.collider != null )
			groundYPosRight = (hitInfoRight.collider.transform.position.y - flip);
	}

	void EnactGravity(){
		if( onRope ){
			gravity = 0f;
		}
		else{
			gravity = gravityActive;
		}

		float frameGrav = frameMovement.y + gravity * flip;

		if( flip < 0 ){
			if( frameGrav < gravityMax * flip ){
				frameGrav = gravityMax * flip;
			}
		}
		else{
			if( frameGrav > gravityMax * flip ){
				frameGrav = gravityMax * flip;
			}
		}

		frameMovement = new Vector2( frameMovement.x, frameGrav );
	}
}

























