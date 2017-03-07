using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {

    public bool facing = true; 
	public bool killed = false;

    //forces for moving
    float moveForceX = 20f;
    float moveForceY = 5f;
    float maxSpeed = 100f;

    // Use this for initialization
    void Start () {

	}

    void Update ()
    {
        float h = Input.GetAxis("Horizontal");
		float w = Input.GetAxis("Vertical");

		// Horizontal flying
		if (h * GetComponent<Rigidbody2D> ().velocity.x < maxSpeed){
				GetComponent<Rigidbody2D> ().AddForce (Vector2.right * h * moveForceX);
		}

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) >= maxSpeed)
			if (GetComponent<Rigidbody2D> ().velocity.y == 0)
            	GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        // Vertical flying
		if (w * GetComponent<Rigidbody2D> ().velocity.y < maxSpeed){
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * w * moveForceY);
		}

		// Don't go off the screen
		if(gameObject.transform.position.y >= 2.0f)
			gameObject.transform.position = new Vector2(gameObject.transform.position.x, 2.0f);
		else if(gameObject.transform.position.y <= -2.0f)
			gameObject.transform.position = new Vector2(gameObject.transform.position.x, -2.0f);

        // Flip the player
        if (h > 0 && !facing) 
            Flip();
        else if (h < 0 && facing) 
            Flip();
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facing = !facing;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	//kill player on enemy contact
	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;

		if (collider.tag == "enemy") { 
			killed = true;
		}
	}


}
