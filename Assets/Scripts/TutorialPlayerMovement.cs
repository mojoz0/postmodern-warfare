using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TutorialPlayerMovement : PlayerMovement {
	bool tutAlreadyPickedUp, tutAlreadyThrown;

	public UnityEvent m_ThrowEvent;
	public UnityEvent m_GetGunEvent;

	void UpdateButActuallyDont() {
		IsSquishing = Input.GetButton("Squish");
		IsStretching = Input.GetButton("Stretch") && !IsSquishing;

		IsSquishing = Input.GetButton("Squish");
		IsStretching = Input.GetButton("Stretch") && !IsSquishing;

		speed = Input.GetAxis("Horizontal")*0.5f;
	}

	public override void ThrowGun() { base.ThrowGun();
		if (tutAlreadyThrown) return;
		m_ThrowEvent.Invoke();
		tutAlreadyThrown = true;
	}

	public override void GetGun(GameObject gun) { base.GetGun(gun);
		if (tutAlreadyPickedUp) return;
		m_GetGunEvent.Invoke();
		tutAlreadyPickedUp = true;
	}
}
