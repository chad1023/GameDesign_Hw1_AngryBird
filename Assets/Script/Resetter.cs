using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour {
	public Rigidbody2D projectile;
	public float resetSpeed;
	private float resetSpeedSqr;
	private SpringJoint2D spring;
	public GameController gamecontroller;
	public float resettime=1f;
	public float time=0f;
	// Use this for initialization
	void Start () {
		resetSpeedSqr = resetSpeed * resetSpeed;
//		spring = projectile.GetComponent<SpringJoint2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			Reset ();
		}
		if (projectile.isKinematic) {
			time = 0f;
		}
		else if (projectile.velocity.sqrMagnitude < resetSpeedSqr) {
			time += Time.deltaTime;

		
		}
		if (time > resettime) {
			time = 0f;
			gamecontroller.TurnOver ();
		}


		
	}


	void Reset(){
		Application.LoadLevel (Application.loadedLevel);
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.name == projectile.name) {
			Debug.Log ("trigger");
			gamecontroller.TurnOver ();

		}
	}
}
