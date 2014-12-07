using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int currentHealth;
	public float invincibilityCountdown;
	public float invincibilityTime;
	public Sprite[] healthSprites;
	public SpriteRenderer spriteRend;
	public Transform[] spawnPoints;
	public AudioClip deathSFX;
	public AudioClip damageSFX;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( invincibilityCountdown <= 0f ){
			invincibilityCountdown = 0f;
		}
		else{
			invincibilityCountdown -= Time.deltaTime;
		}
	}

	public void TakeDamage(){
		if( invincibilityCountdown == 0 && Global.gameState == 1 ){
			currentHealth--;
			if( currentHealth < 0 ){
				currentHealth = 0;
			}
			spriteRend.sprite = healthSprites[currentHealth];

			if( currentHealth > 0 ){
				invincibilityCountdown = invincibilityTime;
				StartCoroutine( InvincibilityFlash() );
			}
			else{
				Global.gameState = 0;
				StartCoroutine( ManageDeath() );
			}
		}
	}

	public void Killbox(){
		if( Global.gameState == 1 && invincibilityCountdown == 0 ){
			currentHealth = 0;
			spriteRend.sprite = healthSprites[currentHealth];
			
			Global.gameState = 0;
			StartCoroutine( ManageDeath() );
		}
	}

	public void HealDamage(){
		if( currentHealth < 3 ){
			currentHealth++;
			spriteRend.sprite = healthSprites[currentHealth];
		}
	}

	IEnumerator InvincibilityFlash(){
		AudioSource.PlayClipAtPoint( damageSFX, transform.position, Global.masterVolume );

		while ( invincibilityCountdown > 0 ){
			if( spriteRend.enabled == true ){
				spriteRend.enabled = false;
			}
			else{
				spriteRend.enabled = true;
			}

			yield return new WaitForSeconds(0.1f);
		}

		spriteRend.enabled = true;
	}

	IEnumerator ManageDeath(){
		AudioSource.PlayClipAtPoint( deathSFX, transform.position, Global.masterVolume );

		for(int x = 0; x < 10; x++){
			if( spriteRend.enabled == true ){
				spriteRend.enabled = false;
			}
			else{
				spriteRend.enabled = true;
			}
			
			yield return new WaitForSeconds(0.1f);
		}

		for(int x = 0; x < 4; x++){
			if( spriteRend.enabled == true ){
				spriteRend.enabled = false;
			}
			else{
				spriteRend.enabled = true;
			}
			
			yield return new WaitForSeconds(0.25f);
		}

		for(int x = 0; x < 2; x++){
			if( spriteRend.enabled == true ){
				spriteRend.enabled = false;
			}
			else{
				spriteRend.enabled = true;
			}
			
			yield return new WaitForSeconds(0.5f);
		}

		spriteRend.enabled = false;

		yield return new WaitForSeconds(0.5f);

		if( Global.currentCheckpoint < 1 ){
			Camera.main.transform.position = new Vector3( 28f, -14f, -10f );
		}
		else if( Global.currentCheckpoint > 2 ){
			Camera.main.transform.position = new Vector3( -48f, -14f, -10f );
		}
		else{
			Camera.main.transform.position = new Vector3( 102f, -14f, -10f );
		}

		transform.position = spawnPoints[ Global.currentCheckpoint ].position;
		currentHealth = 3;
		spriteRend.sprite = healthSprites[currentHealth];
		spriteRend.enabled = true;
		Global.gameState = 1;
	}
}



















