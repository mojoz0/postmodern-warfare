using UnityEngine;
using System.Collections;

public class FireChoco : MonoBehaviour {

	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// when it hits anything
	void  () {
		if (!explosion) return;
		Object.Instantiate(explosion,transform.position,Quaternion.identity);
		explosion = null;
		// destroy self
	}
}
