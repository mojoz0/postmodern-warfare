using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 50f;
	public float jumpForce = 150f;

	public bool grounded;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {

		rb2d = gameObject.GetComponent<Rigidbody2D> ();
	
	}

	// Update is called once per frame
	void Update() {

	}

	void FixedUpdate () {

		float h = Input.GetAxis ("Horizontal");

		rb2d.AddForce ((Vector2.right * speed) * h);


	
	}
}
