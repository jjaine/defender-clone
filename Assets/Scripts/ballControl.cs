using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballControl : MonoBehaviour {
	public GameObject player;
	public GameObject explosion;

	void Start(){
		player = GameObject.FindWithTag("Player");
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;

		if(collider.tag == "Player"){
			GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(collider.gameObject);
			Destroy(gameObject);
			Destroy(exp, 2);
			enemyControl.playerExists = false;
		}
		else if (collider.tag == "Laser") { 
			Destroy(gameObject);
			Destroy(collider.gameObject);
		}
	}
}
