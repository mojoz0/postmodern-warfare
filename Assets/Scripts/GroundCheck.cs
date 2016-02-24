using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	private PlayerMovement player;


	// Use this for initialization
	void Start () {
		player = gameObject.GetComponentInParent<PlayerMovement> ();
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		player.grounded = true;
	}

	void OnTriggerStay2D (Collider2D col) {
		player.grounded = true;
	}

	void OnTriggerExit2D (Collider2D col) {
		player.grounded = false;
	}
}
