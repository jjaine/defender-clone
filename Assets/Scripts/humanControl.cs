using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanControl : MonoBehaviour {

	bool stolen = false;

	void Update () {
		if(transform.parent.tag == "Enemy")
			stolen = true;
		else 
			stolen = false;

		if(!stolen && transform.position.y > -1.5f)
			transform.position -= new Vector3(0, 0.3f*Time.deltaTime, 0);
	}
}
