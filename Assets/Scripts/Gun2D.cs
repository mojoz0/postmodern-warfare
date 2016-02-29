using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
class Gun2D : MonoBehaviour {
	string gunTag = "Gun";
	public float scale = 0.5f;
	public float damage = 214f;
	public float force = 6000f;

	public bool CollidersEnabled {
		get { return collidersEnabled; }
		set { if (collidersEnabled==value) return;
			collidersEnabled = value;
			foreach (Transform child in transform)
				foreach (var c in child.gameObject.GetComponents<Collider2D>())
					c.enabled = collidersEnabled;
		}
	} bool collidersEnabled;

	public bool IsHeld {
		get { return isHeld; }
		set { if (isHeld==value) return;
			isHeld = value;
			GetComponent<Rigidbody2D>().isKinematic = isHeld;
			CollidersEnabled = !isHeld;
		}
	} bool isHeld;


	void Start() {
		collidersEnabled = true;
		foreach (Transform child in transform)
			child.gameObject.tag = gunTag;
	}

	void LateUpdate() {
		if (IsHeld) transform.localPosition = Vector3.zero;
	}

	void FixedUpdate() {
		transform.localScale = new Vector3(scale,scale,scale);
		if (IsHeld) transform.localPosition = Vector3.zero;
	}

	void OnCollisionEnter2D(Collision2D c) {
		if (!c.rigidbody) return;
		var other = c.rigidbody.GetComponent<IDamageable>();
		if (other!=null) other.Apply(damage);
	}
}


