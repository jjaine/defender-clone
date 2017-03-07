using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class enemyControl : MonoBehaviour {
	public GameObject player;
	public GameObject explosion;
	public GameObject reloadTextObject;

	public static bool playerExists = true;

	float moveForceX = 2f;
    float moveForceY = 1f;

	void Start(){
		player = GameObject.FindWithTag("Player");
		reloadTextObject = GameObject.Find("ReloadText");
		reloadTextObject.GetComponent<Text>().enabled = false;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;

		if (collider.tag == "Laser") { 
			Destroy(gameObject);
			Destroy(collider.gameObject);
			spawnEnemies.enemyCount--;
		}
		else if(collider.tag == "Player"){
			GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(collider.gameObject);
			Destroy(gameObject);
			Destroy(exp, 2);
			playerExists = false;
		}
	}

	void Update ()
	{
		Physics2D.IgnoreLayerCollision (8, 8, true);

		if(playerExists){
			float dist = Vector3.Distance(player.transform.position, gameObject.transform.position);
			//move towards player if somewhat close, but do not move to touching
			if(dist < 4){
				if(Mathf.Abs(player.transform.position.x-gameObject.transform.position.x) > 0.5f){
					if(player.transform.position.x < gameObject.transform.position.x)
						GetComponent<Rigidbody2D>().AddForce (Vector2.left * moveForceX);
					else
						GetComponent<Rigidbody2D>().AddForce (Vector2.right * moveForceX);
				}
				else if(Mathf.Abs(player.transform.position.y-gameObject.transform.position.y) > 0.5f){
					if(player.transform.position.y < gameObject.transform.position.y)
						GetComponent<Rigidbody2D>().AddForce (Vector2.down * moveForceY);
					else
						GetComponent<Rigidbody2D>().AddForce (Vector2.up * moveForceY);
				}
			}

		}
		else{
			StartCoroutine(Reload());
		}
	}

	IEnumerator Reload(){
		reloadTextObject.GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(3);
		playerExists = true;
		spawnEnemies.enemyCount=0;
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}
}
