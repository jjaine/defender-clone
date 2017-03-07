using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class enemyControl : MonoBehaviour {
	//float speed = 2f;		
	//bool dead = false;	
	public GameObject player;
	public GameObject explosion;
	public GameObject reloadTextObject;

	public static bool playerExists = true;

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


		}
		else{
			StartCoroutine(Reload());
		}
	}

	IEnumerator Reload(){
		reloadTextObject.GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(5);
		playerExists = true;
		spawnEnemies.enemyCount=0;
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}
}
