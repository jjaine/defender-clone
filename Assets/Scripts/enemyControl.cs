using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class enemyControl : MonoBehaviour {
	public GameObject player;
	public GameObject explosion;
	public GameObject ball;
	public GameObject reloadTextObject;
	public GameObject humanTextObject;

	GameObject stolenHuman;
	GameObject[] humanArray;

	public static bool playerExists = true;

	float shootTime = 6f;
	bool shot = false;
	bool steal = false;
	static int humansStolen = 0;

	float moveForceX = 2f;
    float moveForceY = 1f;

	void Start(){
		player = GameObject.FindWithTag("Player");
		reloadTextObject = GameObject.Find("ReloadText");
		reloadTextObject.GetComponent<Text>().enabled = false;
		humanTextObject = GameObject.Find("HumansText");
		humanTextObject.GetComponent<Text>().enabled = true;	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;

		if (collider.tag == "Laser") { 
			GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
			if(steal){
				stolenHuman = gameObject.transform.GetChild(0).gameObject;
				stolenHuman.transform.parent=transform.parent;
				stolenHuman.GetComponent<Collider>().enabled=true;
				steal = false;
				humansStolen--;
			}
			Destroy(gameObject);
			Destroy(collider.gameObject);
			Destroy(exp, 2);
			spawnEnemies.enemyCount--;
		}
		else if(collider.tag == "Player"){
			GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(collider.gameObject);
			Destroy(gameObject);
			Destroy(exp, 2);
			playerExists = false;
		}
		else if(collider.tag == "Human"){
			steal = true;
			collider.gameObject.transform.position = new Vector2(transform.position.x-0.1f,transform.position.y);
			collider.gameObject.transform.parent = gameObject.transform;
			collider.enabled = false;
			humansStolen++;
		}
	}

	void Update ()
	{
		Physics2D.IgnoreLayerCollision (8, 8, true);

		if(playerExists){
			humanArray = GameObject.FindGameObjectsWithTag("Human");
			foreach(GameObject h in humanArray){
				if(h.transform.position.y > 2.4f){
					Destroy(h);
				}
			}
			if(steal){
				if(transform.position.y < 2.5f){
					GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.1f);
				}
				else{				
					steal = false;
					Destroy(gameObject);
					spawnEnemies.enemyCount--;
					humansStolen--;
				}
			}
			else{
				humanTextObject.GetComponent<Text>().text = "Humans abducted: " + (5-humanArray.Length)+"/5,\nmoving: " + humansStolen +"/5";
				if(humanArray.Length < 1)
					playerExists = false;

				float dist = Vector3.Distance(player.transform.position, transform.position);
				//move towards player if somewhat close, but do not move to touching
				if(dist < 4){
					if(Mathf.Abs(player.transform.position.x-transform.position.x) > 0.8f){
						if(player.transform.position.x < transform.position.x)
							GetComponent<Rigidbody2D>().AddForce (Vector2.left * moveForceX);
						else
							GetComponent<Rigidbody2D>().AddForce (Vector2.right * moveForceX);
					}
					else if(Mathf.Abs(player.transform.position.y-transform.position.y) > 0.5f){
						if(player.transform.position.y < transform.position.y)
							GetComponent<Rigidbody2D>().AddForce (Vector2.down * moveForceY);
						else
							GetComponent<Rigidbody2D>().AddForce (Vector2.up * moveForceY);
					}
					if(shot && shootTime > 0){
						shootTime -= Time.deltaTime;
					}
					else{
						shot = false;
						shootTime = 6f;
					}
					if(!shot){
						shot = true;
						Shoot();
					}
				}
			}

		}
		else{
			StartCoroutine(Reload());
		}
	}

	IEnumerator Reload(){
		humanTextObject.GetComponent<Text>().enabled = false;
		reloadTextObject.GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(3);
		playerExists = true;
		spawnEnemies.enemyCount=0;
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	void Shoot(){
		GameObject shot = Instantiate(ball, transform.position, Quaternion.identity);
		int dir = 1;
		if(player.transform.position.x < transform.position.x)
			dir = -1;
		if(player.transform.position.y > transform.position.y)
			shot.GetComponent<Rigidbody2D>().velocity = new Vector2 (dir*0.2f, 0.5f);
		else
			shot.GetComponent<Rigidbody2D>().velocity = new Vector2 (dir*0.2f, -0.5f);

		Destroy(shot, 2f);
	}

}
