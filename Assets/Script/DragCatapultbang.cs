using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCatapultbang : MonoBehaviour {
	public float maxStretch;
	public LineRenderer catapultLineFront;
	public LineRenderer catapultLineBack;

	private SpringJoint2D spring;

	private Rigidbody2D rigidbody2D;

	private Ray rayToMouse;
	private Ray leftCatapultToProjectile;

	private float circleRadius;
	private Transform catapult;
	public bool clickedOn;
	private Vector2 preVelocity;
	public AudioSource shoot;
	public AudioSource hit;




	// Use this for initialization

	void Awake(){
		spring = GetComponent<SpringJoint2D> ();
		rigidbody2D = GetComponent<Rigidbody2D> ();
		catapult = spring.connectedBody.transform;
	
	}
	void Start () {
		LineRendererSetup ();
		rayToMouse = new Ray (catapult.position, Vector3.zero);

		leftCatapultToProjectile = new Ray (catapultLineFront.transform.position, Vector3.zero);
	
		CircleCollider2D circle = GetComponent<Collider2D>() as CircleCollider2D;
		circleRadius = circle.radius;
	}
	
	// Update is called once per frame
	void Update () {


//		RaycastHit hitInfo = new RaycastHit2D();
//		Ray2D ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//		// OnMouseDown
//		if (Input.GetMouseButtonDown(0))
//		{
//			if (Physics2D.Raycast(ray, out hitInfo))
//			{
//				hitInfo.collider.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
//			}
//		}
//
//		// OnMouseUp
//		if (Input.GetMouseButtonUp(0))
//		{
//			if (Physics2D.Raycast(ray, out hitInfo))
//			{
//				hitInfo.collider.SendMessage("OnMouseUp", SendMessageOptions.DontRequireReceiver);
//			}
//		}
//	
		Debug.DrawRay (rayToMouse.origin,rayToMouse.direction, Color.cyan);
		Debug.DrawRay (leftCatapultToProjectile.origin,leftCatapultToProjectile.direction, Color.red);
		if (clickedOn) {
			Dragging ();		
		}
		if (spring != null) {
			if (!rigidbody2D.isKinematic && preVelocity.sqrMagnitude > rigidbody2D.velocity.sqrMagnitude) {
				spring.enabled = false;
				spring = null;
				rigidbody2D.velocity = preVelocity;
			} 
			if (!clickedOn) {
				preVelocity = rigidbody2D.velocity;
			}
			LineRendererUpdate ();
		}
		else {
				catapultLineFront.enabled=false;
				catapultLineBack.enabled=false;
				
		}

	}
	void LineRendererSetup(){
		catapultLineFront.SetPosition (0, catapultLineFront.transform.position);
		catapultLineBack.SetPosition (0, catapultLineBack.transform.position);

		catapultLineFront.sortingLayerName= "Foreground";
		catapultLineBack.sortingLayerName = "Background";

		catapultLineFront.sortingOrder = 3;
		catapultLineBack.sortingOrder = 1;
	}

	void LineRendererUpdate(){

		Vector2 catapultToProjectile = transform.position - catapultLineFront.transform.position;
		leftCatapultToProjectile.direction = catapultToProjectile;
		Vector3 holdPoint = leftCatapultToProjectile.GetPoint (catapultToProjectile.magnitude+ circleRadius);
	
		catapultLineFront.SetPosition (1, holdPoint);
		catapultLineBack.SetPosition (1, holdPoint);		
	
	}
	void OnMouseDown(){
		Debug.Log ("MOUSEDOWN");
		spring.enabled = false;
		clickedOn = true;
	
	
	}
	void OnMouseUp(){
		Debug.Log ("MOUSEUP");
		if (clickedOn) {
			spring.enabled = true;
			rigidbody2D.isKinematic = false;
			clickedOn = false;
			shoot.Play ();
		}
	}
	void Dragging(){
		Debug.Log ("Drag");
		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		Vector2 catapultToMouse = mouseWorldPoint - catapult.position;
		if (catapultToMouse.magnitude > maxStretch) {
			rayToMouse.direction = catapultToMouse;
			mouseWorldPoint = rayToMouse.GetPoint (maxStretch);
		}

		mouseWorldPoint.z = 0f;

		transform.position = mouseWorldPoint;
	}
	public void Reset(){
		catapultLineFront.enabled=true;
		catapultLineBack.enabled=true;
		spring = GetComponent<SpringJoint2D> ();
		spring.enabled = true;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0f;
		rigidbody2D.isKinematic = true; 
		Debug.Log("Reset1");
		rayToMouse = new Ray (catapult.position, Vector3.zero);
		leftCatapultToProjectile = new Ray (catapultLineFront.transform.position, Vector3.zero);

		catapultLineFront.sortingLayerName= "Foreground";
		catapultLineBack.sortingLayerName = "Background";

		catapultLineFront.sortingOrder = 3;
		catapultLineBack.sortingOrder = 1;
	
		Debug.Log("Reset");
	}
	void OnCollisionEnter2D(Collision2D collision){
		hit.Play ();
	}
}
