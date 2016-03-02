using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	private PlayerMovement player;

	void Start() {
		player = gameObject.GetComponentInParent<PlayerMovement>();
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.isTrigger) return;
		player.IsGrounded = true;
	}

	void OnTriggerStay2D(Collider2D c) {
		if (c.isTrigger) return;
		player.IsGrounded = true;
	}

	void OnTriggerExit2D(Collider2D c) {
		if (c.isTrigger) return;
		player.IsGrounded = false;
	}
}
