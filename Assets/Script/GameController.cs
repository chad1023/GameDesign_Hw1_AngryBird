using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int maxturn;
	public int turn;
	public int maxstage;
	public int stage = 0;
	public Rigidbody2D projectile;
	public Transform emitpoint;
	public Resetter resetter;
	public Text showtext;
	public AudioSource audio;

	private Vector3 emitposition;


	// Use this for initialization
	void Start () {
		turn = 1;
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	public void TurnOver(){
		if (turn <= maxturn) {
			resetter.time = 0f;
			ResetProjectile ();
			//resetprojectile
			turn++;
			if (turn > maxturn) {
				Reset ();
			}

		} 
	}
	public void StageComplete(){
		Debug.Log ("stagecomplete" + stage);
		showtext.text="Complete!!";
		stage++;
		if (stage < maxstage) {
			Debug.Log ("stageload" + stage);
			Invoke ("Load", 1f);
		}
	}

	void ResetProjectile(){
		projectile.transform.position = emitpoint.position;
		projectile.transform.rotation = emitpoint.rotation;

		projectile.gameObject.GetComponent<DragCatapultbang> ().Reset ();
		Debug.Log ("resetprojectile"+turn);

	}

	void Load(){
		Application.LoadLevel (stage);
	}

	void Reset(){
		audio.Play ();
		showtext.text="You're dead!!\nPress 'R' for replay";

	}
}
