using UnityEngine;
using System.Collections;

public class EventTriggerer2D : MonoBehaviour {

	public MessageTrigger trigger;

	Collider2D coll;

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
}
