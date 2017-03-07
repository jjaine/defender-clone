﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour {
	[HideInInspector]
	public GameObject world1;
	[HideInInspector]
	public GameObject world2;
	public GameObject enemy;

	public static int enemyCount=0;

	//init
	void Start () {
		world1 = GameObject.FindWithTag("World1");
		world2 = GameObject.FindWithTag("World2");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 world1pos = world1.transform.position;
		Vector3 world2pos = world2.transform.position;


		if(enemyCount < 10){
			if(Random.Range(0,2)==0){
				float x = Random.Range(world1pos.x-12f, world1pos.x+12f);
				float y = Random.Range(world1pos.y, world1pos.y+1.8f);
				GameObject enemyInstance = Instantiate(enemy, new Vector3(x, y, 0), Quaternion.Euler(new Vector3(0,0,0)));
				enemyInstance.transform.parent = world1.transform;
			}
			else{
				float x = Random.Range(world2pos.x-12f, world2pos.x+12f);
				float y = Random.Range(world2pos.y, world2pos.y+1.8f);
				GameObject enemyInstance = Instantiate(enemy, new Vector3(x, y, 0), Quaternion.Euler(new Vector3(0,0,0)));
				enemyInstance.transform.parent = world2.transform;
			}
			enemyCount++;
		}
	}
}