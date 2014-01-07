using UnityEngine;
using System.Collections;

public class SantaControlScript : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;
	
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public LayerMask whatIsDeadZone;
	public float jumpForce = 700f;
	public bool IsAlive = true;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (IsAlive) {
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			anim.SetBool ("Ground", grounded);
			anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

			float move = Input.GetAxis ("Horizontal");
			anim.SetFloat ("Speed", Mathf.Abs (move));
			rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

			if (move > 0 && !facingRight) {
					FlipXAxis ();
			} else if (move < 0 && facingRight) {
					FlipXAxis ();
			}

			if (Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsDeadZone)) {
					LoadLevel (Application.loadedLevel);
					StartCoroutine (LoadLevel (Application.loadedLevel));
			}
		}
	}

	IEnumerator LoadLevel(int level){
		IsAlive = false;
		yield return new WaitForSeconds(5);
		Application.LoadLevel (Application.loadedLevel);
	}

	void FlipXAxis() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Update(){
		if (IsAlive) {
			if (grounded && Input.GetKeyDown (KeyCode.Space)) {
					anim.SetBool ("Ground", false);		
					rigidbody2D.AddForce (new Vector2 (0, jumpForce));
			}

			anim.SetBool ("Duck", Input.GetKey (KeyCode.DownArrow));
		}
	}
}
