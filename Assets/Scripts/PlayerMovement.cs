using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speedForce = 50f;
	public float maxSpeed = 10f;
	public float jumpForce = 150f;

	public bool grounded;
	public bool squishing;

	private Rigidbody2D rb2d;
	private Animator anim;

	// Use this for initialization
	void Start () {

		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
	
	}
		

	// Update is called once per frame
	void Update() {

		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
		anim.SetBool ("Squishing", Input.GetButton("Squish"));


		/* Get Player to face correct direction */
		if (Input.GetAxis ("Horizontal") > 0.1f) {
			transform.localScale = new Vector3 (1, 1, 1);
		} else if (Input.GetAxis ("Horizontal") < -0.1f) {
			transform.localScale = new Vector3 (-1, 1, 1);
		}

		if (Input.GetButtonDown ("Jump") && grounded) {
			rb2d.AddForce (Vector2.up * jumpForce);
		}
			

	}


	void FixedUpdate () {

		/* Moving the player */
		float h = Input.GetAxis ("Horizontal");
		rb2d.AddForce ((Vector2.right * speedForce) * h);

		/* Limiting speed of Player */
		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		} else if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}


	
	}
}
