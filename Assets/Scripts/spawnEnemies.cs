using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour {
	[HideInInspector]
	public GameObject world1;
	[HideInInspector]
	public GameObject world2;
	public GameObject enemy;
	public GameObject human;

	public static int enemyCount=0;
	public static int humanCount=5;

	//init, spawn humans
	void Start () {
		world1 = GameObject.FindWithTag("World1");
		world2 = GameObject.FindWithTag("World2");
		for(int i=0; i<humanCount; i++){
			if(Random.Range(0,2)==0){
				float x = Random.Range(world2.transform.position.x-12f, world2.transform.position.x+12f);
				float y = Random.Range(world2.transform.position.y-1.9f, world2.transform.position.y-1.5f);
				GameObject humanInstance = Instantiate(human, new Vector3(x, y, 0), Quaternion.identity);
				humanInstance.transform.parent = world2.transform;
			}
			else{
				float x = Random.Range(world1.transform.position.x-12f, world1.transform.position.x+12f);
				float y = Random.Range(world1.transform.position.y-1.9f, world1.transform.position.y-1.5f);
				GameObject humanInstance = Instantiate(human, new Vector3(x, y, 0), Quaternion.identity);
				humanInstance.transform.parent = world1.transform;
			}
		}

	}
	
	void Update () {
		Vector3 world1pos = world1.transform.position;
		Vector3 world2pos = world2.transform.position;

		//have always 10 enemies
		if(enemyCount < 10){
			if(Random.Range(0,2)==0){
				float x = Random.Range(world1pos.x-12f, world1pos.x+12f);
				float y = Random.Range(world1pos.y, world1pos.y+1.8f);
				GameObject enemyInstance = Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);
				enemyInstance.transform.parent = world1.transform;
			}
			else{
				float x = Random.Range(world2pos.x-12f, world2pos.x+12f);
				float y = Random.Range(world2pos.y, world2pos.y+1.8f);
				GameObject enemyInstance = Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);
				enemyInstance.transform.parent = world2.transform;
			}
			enemyCount++;
		}
	}
}
