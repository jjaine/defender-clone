using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {

    public bool facing = true; 
	public Vector3 originalPosition;
	public bool killed = false;

    //forces for moving
    public float moveForce = 200f;
    public float maxSpeed = 3f;

    // Use this for initialization
    void Start () {
		originalPosition = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate ()
    {
        float h = Input.GetAxis("Horizontal");

		if (h * GetComponent<Rigidbody2D> ().velocity.x < maxSpeed){
			if (GetComponent<Rigidbody2D> ().velocity.y == 0)
				GetComponent<Rigidbody2D> ().AddForce (Vector2.right * h * moveForce);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
			}
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) >= maxSpeed)
			if (GetComponent<Rigidbody2D> ().velocity.y == 0)
            	GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

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

	//kill player on enemy contact, check if grounded 
	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;

		if (collider.tag == "enemy") { 
			killed = true;
			gameObject.transform.position = originalPosition;
		}
	}


}
