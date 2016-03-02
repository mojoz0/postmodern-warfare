using UnityEngine;
using System.Collections;

public class FireChoco : MonoBehaviour {

	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// when it hits anything
	void OnCollisionEnter2D(Collision2D c) {

		if (c.gameObject.tag == "Baddie")
			return;

		// hurt other thing (look at gun2d code)
		// destroy self

		/* Explode on contact with anything */
		if (explosion)
			Object.Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
