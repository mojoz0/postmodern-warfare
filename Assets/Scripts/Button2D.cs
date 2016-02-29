
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
class Button2D : MonoBehaviour, IDamageable {

	public bool isOn;

	public GameObject explosion;

	public UnityEvent m_ClickEvent;

	public void Click() {
		isOn = !isOn;
		if (!explosion) return;
		Object.Instantiate(explosion,transform.position,Quaternion.identity);
		explosion = null;
	}

	public void Apply(float damage) {
		if (damage>0) m_ClickEvent.Invoke();
	}

	void Start() {
		if (m_ClickEvent==null)
			m_ClickEvent = new UnityEvent();
		m_ClickEvent.AddListener(Click);
	}
}
