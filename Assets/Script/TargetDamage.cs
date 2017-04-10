using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamage : MonoBehaviour {

	public int hitpoints = 2;
//	public Sprite damagedSprite;
	public float damageImpactSpeed;
	public GameController gameController;
	public int currenthitpoints;
	private ParticleSystem particle;

	private float damagedImpactSpeedSqr;
	private SpriteRenderer spriteRenderer;
	private Collider2D collider2D;
	private AudioSource audio;
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		currenthitpoints = hitpoints;
		damagedImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
		particle = GetComponent < ParticleSystem >();
		collider2D = GetComponent<Collider2D> ();
		audio = GetComponent<AudioSource> ();
//		rigidbody2D = GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D collision){
		Debug.Log ("DF");
		if (collision.collider.tag != "Damager")
			return;
		if (collision.relativeVelocity.sqrMagnitude < damagedImpactSpeedSqr)
			return;
//		spriteRenderer.sprite = damagedSprite;
		currenthitpoints--;

		if (currenthitpoints <= 0)
			Kill ();
	}
	void Kill(){
		Debug.Log ("Kill");
//		Time.timeScale = 0;
		spriteRenderer.enabled = false;
		collider2D.enabled = false;
		audio.Play ();
		particle.Play ();
		gameController.StageComplete ();
//		gameController.StageComplete ();

	}
	void complete(){
		
	}

}
