
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
class Baddie : MonoBehaviour, IDamageable {

	Rigidbody2D rb2d;
	BoxCollider2D bc2d;

	public bool isAlive;
	public float throwStrength = 20f;

	public GameObject explosion;
	public GameObject projectile;
	public GameObject deadReplacement;

	public UnityEvent m_ClickEvent;

	public void Click() {
		isAlive = !isAlive;
		if (explosion) {
			Object.Instantiate(explosion,transform.position,Quaternion.identity);
			explosion = null;
		}

		GameObject temp;
		if (deadReplacement) {
			temp = Object.Instantiate(deadReplacement,transform.position,Quaternion.identity) as GameObject;
			foreach (var rb in temp.GetComponentsInChildren<Rigidbody2D>())
				rb.AddExplosionForce(4000f, transform.position,400f,200);
		}
		Destroy(gameObject);
	}

	/* This kills the crab */
	public void Die() {
		rb2d.constraints &= ~(RigidbodyConstraints2D.FreezeAll);
		bc2d.isTrigger = true;
	}

	public void Apply(float damage) {
		if (damage > 0) {
			m_ClickEvent.Invoke();
			Die();
		}
	}

	void ThrowProjectile() {
		GameObject projObj = (GameObject) Object.Instantiate (projectile, gameObject.transform.position,
			gameObject.transform.rotation);
		//projObj.transform = gameObject.transform;
		Debug.Log(transform.forward);

		var throwVector = new Vector2 (1, 1);
		Debug.Log (throwVector);
		projObj.GetComponent<Rigidbody2D> ().AddForce(throwVector * throwStrength);

	}

	void Start() {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		bc2d = gameObject.GetComponent<BoxCollider2D>();

		ThrowProjectile ();

		if (m_ClickEvent==null)
			m_ClickEvent = new UnityEvent();
		m_ClickEvent.AddListener(Click);
	}
}

















