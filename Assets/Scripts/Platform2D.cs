using UnityEngine;
using System.Collections;
using EventArgs=System.EventArgs;

public class Platform2D : MonoBehaviour {
	bool wait;
	public bool on;
	public bool movingToTarget;
	public bool thisIsSpecialPlatform;
	public float delay = 2f;
	public float endDelay = 0.1f;
	Vector3 initial;
	Vector3 speed;

	public Vector3 Target {
		get { return (movingToTarget)?(target.localPosition):(initial); }
	} public Transform target;

	void Awake() {
		initial = transform.localPosition;
		//foreach (var piece in pieces)
		//	piece.SolveEvent += OnSolve;
		if (!target)
			throw new System.Exception("Holy shit. Please assign target");
	}

	public int OnSolve(
				//IPiece<int> sender,
				EventArgs e,
				bool solved) {
		on = solved;
		return 0;
	}



	IEnumerator SwitchingDirection() {
		if (wait) yield break;
		wait = true;
		yield return new WaitForSeconds (endDelay);
		movingToTarget = !movingToTarget;
		wait = false;
	}

	void FixedUpdate() {
		if (!on) return;
		if ((transform.localPosition - Target).magnitude < .3f)
			StartCoroutine(SwitchingDirection());
		transform.localPosition = Vector3.SmoothDamp(
			transform.localPosition, Target, ref speed, delay);
	}

}
