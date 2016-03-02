using UnityEngine;
using System.Collections;

public class OffCameraDelete : MonoBehaviour {

	public float delay = 2f;

	IEnumerator Start() {
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
}
