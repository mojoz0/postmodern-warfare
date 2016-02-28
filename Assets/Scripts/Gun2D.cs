using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
class Gun2D : MonoBehaviour {
	bool wait;
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


	void FixedUpdate() {
		transform.localScale = new Vector3(scale,scale,scale);
	}

	public void DisableCollisions(float delay) {
		StartCoroutine(DisablingCollisions(delay));
	}

	IEnumerator DisablingCollisions(float delay) {
		if (wait) yield break;
		wait = true;
		collidersEnabled = false;
		yield return new WaitForSeconds(delay);
		collidersEnabled = true;
		wait = false;
	}

	void OnCollision2D(Collider c) {
		var other = c.GetComponent<IDamageable>();
		if (other==null) return;
		other.Apply(damage);
	}
}


