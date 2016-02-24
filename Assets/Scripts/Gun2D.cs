using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
class Gun2D : MonoBehaviour {

	string gunTag = "Gun";

	public float scale = 0.5f;

	public float damage = 214f;

	public bool IsHeld {
		get { return isHeld; }
		set { if (isHeld==value) return;
			isHeld = value;
			GetComponent<Rigidbody2D>().isKinematic = isHeld;
			foreach (Transform child in transform)
				foreach (var c in child.gameObject.GetComponents<Collider2D>())
					c.enabled = !isHeld;

		}
	} bool isHeld;


	void Start() {
		foreach (Transform child in transform)
			child.gameObject.tag = gunTag;
	}


	void FixedUpdate() {
		transform.localScale = new Vector3(scale,scale,scale);
	}

	void OnCollision2D(Collider c) {
		var other = c.GetComponent<IDamageable>();
		if (other==null) return;
		other.Apply(damage);
	}
}


interface IDamageable {

	void Apply(float damage);
}




