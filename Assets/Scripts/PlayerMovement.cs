using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour {

	public float speedForce = 50f;
	public float maxSpeed = 10f;
	public float jumpForce = 150f;

	public bool grounded;
	public bool squishing;

	public GameObject potentialGun;

	public LayerMask layerMask;

	public GameObject gun;

	Rigidbody2D rb2d;
	Animator anim;

	public bool HasGun {get;set;}

	public float Radius { get { return 2f; } }

	void Awake() {
		layerMask = ~LayerMask.NameToLayer("Gun");
			//~(LayerMask.NameToLayer("Gun")
			//& LayerMask.NameToLayer("Powerup"));
	}

	void Start() {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
	}


	// Update is called once per frame
	void Update() {

		anim.SetBool("Grounded", grounded);
		anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
		anim.SetBool("Squishing", Input.GetButton("Squish"));

		/* Get Player to face correct direction */
		if (Input.GetAxis ("Horizontal") > 0.1f)
			transform.localScale = new Vector3 (1, 1, 1);
		else if (Input.GetAxis ("Horizontal") < -0.1f)
			transform.localScale = new Vector3 (-1, 1, 1);
		if (Input.GetButtonDown ("Jump") && grounded)
			rb2d.AddForce (Vector2.up * jumpForce);
		if (Input.GetButtonDown("Throw")) {
			if (HasGun) ThrowGun();
			else GetGun();
		}
	}

	void OnCollisionEnter2D(Collision2D c) {
		if (!c.rigidbody || c.rigidbody.tag!="Gun") return;
		potentialGun = c.rigidbody.gameObject;
		//GetGun(c.rigidbody.gameObject);
	}

	void ThrowGun() {
		gun.GetComponent<Gun2D>().IsHeld = false;
		gun.transform.parent = null;
		HasGun = false;
		gun.GetComponent<Rigidbody2D>().AddForce(
			transform.forward*300f, ForceMode2D.Impulse);
		gun.GetComponent<Rigidbody2D>().AddRelativeForce(
			Vector2.right*100f, ForceMode2D.Impulse);
		gun.GetComponent<Rigidbody2D>().AddTorque(1200f);
	}


	void GetGun() {
		if (potentialGun) GetGun(potentialGun);
	}

	void GetGun(GameObject gun) {
		gun.transform.parent = this.transform;
		gun.transform.localPosition = Vector3.zero;
		HasGun = true;
        gun.GetComponent<Gun2D>().IsHeld = true;
        this.gun = gun;
	}


	void FixedUpdate() {
        var nearby = Physics.OverlapSphere(
            transform.position, Radius);//, layerMask,
            //QueryTriggerInteraction.Collide);
        foreach (var elem in nearby) {
            var o = elem.attachedRigidbody.GetComponent<Gun2D>()
                ?? elem.gameObject.GetComponent<Gun2D>();
            if (o==null) continue;
        }


		/* Moving the player */
		float h = Input.GetAxis ("Horizontal");
		rb2d.AddForce ((Vector2.right * speedForce) * h);

		/* Limiting speed of Player */
		if (rb2d.velocity.x > maxSpeed)
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		else if (rb2d.velocity.x < -maxSpeed)
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
	}
}
