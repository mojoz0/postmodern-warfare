using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventTriggerer2D : MonoBehaviour {

	public MessageTrigger trigger;

	Collider2D coll;

	public UnityEvent m_MessageEvent;

	public float delay;


	void Awake() {
		coll = GetComponent<Collider2D>();
		switch (trigger) {
			case MessageTrigger.Collider:
				if (!coll)
					throw new System.Exception("no collider");
				break;
			case MessageTrigger.Default:
			case MessageTrigger.Event:
			case MessageTrigger.Time: break;
		}
	}

	void Start() {
		if (m_MessageEvent==null)
			m_MessageEvent = new UnityEvent();
		m_MessageEvent.AddListener(OnEvent);
	}

	void OnCollisionEnter2D(Collision2D c) {
		if (trigger!=MessageTrigger.Collider) return;
		m_MessageEvent.Invoke();
	}

	public void OnEvent() { print("something happened!"); }
}





