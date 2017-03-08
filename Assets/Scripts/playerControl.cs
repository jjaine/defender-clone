using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {

    public bool facing = true; 
	public GameObject laser;
	float speed = 20f;

    //forces for moving
    float moveForceX = 20f;
    float moveForceY = 5f;
    float maxSpeed = 100f;

    void Update ()
    {
    	Physics2D.IgnoreLayerCollision (9, 9, true);

    	if(enemyControl.playerExists){
	        float h = Input.GetAxis("Horizontal");
			float w = Input.GetAxis("Vertical");

			//horizontal flying
			if (h * GetComponent<Rigidbody2D> ().velocity.x < maxSpeed){
					GetComponent<Rigidbody2D> ().AddForce (Vector2.right * h * moveForceX);
			}

	        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) >= maxSpeed)
				if (GetComponent<Rigidbody2D> ().velocity.y == 0)
	            	GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

	        //vertical flying
			if (w * GetComponent<Rigidbody2D> ().velocity.y < maxSpeed){
					GetComponent<Rigidbody2D> ().AddForce (Vector2.up * w * moveForceY);
			}

			//don't go off the screen
			if(gameObject.transform.position.y >= 2.0f)
				gameObject.transform.position = new Vector2(gameObject.transform.position.x, 2.0f);
			else if(gameObject.transform.position.y <= -2.0f)
				gameObject.transform.position = new Vector2(gameObject.transform.position.x, -2.0f);

	        //flip the player
	        if (h > 0 && !facing) 
	            Flip();
	        else if (h < 0 && facing) 
	            Flip();

	        //shoot laser
	        if(Input.GetButtonDown("Fire1")) {
				if(facing) {
					GameObject laserInstance = Instantiate(laser, new Vector3(transform.position.x+0.8f, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,0)));
					laserInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
					Destroy(laserInstance, 1f);
				}
				else {
					GameObject laserInstance = Instantiate(laser, new Vector3(transform.position.x-0.8f, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,0)));
					laserInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
					Destroy(laserInstance, 1f);
				}
			}
		}
    }

    void Flip()
    {
        //switch the way the player is labelled as facing.
        facing = !facing;

        //multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
