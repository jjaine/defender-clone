using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour {

	[HideInInspector]
	public GameObject player;

	//init
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	void Update () {
    	if(enemyControl.playerExists)
			gameObject.transform.position = new Vector3(player.transform.position.x, 0, gameObject.transform.position.z);
		
	}
}
