using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWorld : MonoBehaviour {

	[HideInInspector]
	public GameObject player;
	[HideInInspector]
	public GameObject world1;
	[HideInInspector]
	public GameObject world2;

	//init
	void Start () {
		player = GameObject.FindWithTag("Player");
		world1 = GameObject.FindWithTag("World1");
		world2 = GameObject.FindWithTag("World2");
	}
	
	void Update () {
		float playerpos = player.transform.position.x;
		float world1pos = world1.transform.position.x;
		float world2pos = world2.transform.position.x;

		//on world1, move world2
		if(playerpos >= world1pos -12.25f && playerpos <= world1pos + 12.25f){
			if(playerpos < world1pos && world2pos > world1pos)
				world2.transform.position = new Vector2(world1pos-25.5f, 0);
			else if(playerpos > world1pos && world2pos < world1pos)
				world2.transform.position = new Vector2(world1pos+25.5f, 0);
		}

		//on world2, move world1
		if(playerpos >= world2pos -12.25f && playerpos <= world2pos + 12.25f){
			if(playerpos < world2pos && world1pos > world2pos)
				world1.transform.position = new Vector2(world2pos-25.5f, 0);
			else if(playerpos > world2pos && world1pos < world2pos)
				world1.transform.position = new Vector2(world2pos+25.5f, 0);
		}
		
	}
}
