﻿
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
class Baddie : MonoBehaviour, IDamageable {

	Rigidbody2D rb2d;
	BoxCollider2D bc2d;

	public bool isAlive;

	public GameObject explosion;
	public GameObject fireChoco;

	public UnityEvent m_ClickEvent;

	public void Click() {
		isAlive = !isAlive;
		if (!explosion) return;
		Object.Instantiate(explosion,transform.position,Quaternion.identity);
		explosion = null;
	}

	/* This kills the crab */
	public void Die() {
		rb2d.constraints &= ~(RigidbodyConstraints2D.FreezeAll);
		bc2d.isTrigger = true;
	}

	public void Apply(float damage) {
		if (damage > 0) {
			m_ClickEvent.Invoke ();
			Die (); 
		}
	}

	void Start() {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		bc2d = gameObject.GetComponent<BoxCollider2D>();

		if (m_ClickEvent==null)
			m_ClickEvent = new UnityEvent();
		m_ClickEvent.AddListener(Click);
	}
}
