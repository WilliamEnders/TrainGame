﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {
	
	private float oy;
	public float speed;
	private float originalSpeed;
	private float jumpspeed;
	private float ground;
	private string control;
	
	public string up;
	public string down;
	public string left;
	public string right;
	public string action;
	public string action2;

	public float slowdown;

	public GameObject coal;
	public GameObject bucket;
	private GameObject coals;
	private GameObject buckets;
	private GameObject moon;
	public GameObject moonstone;
	private Transform button;
	private Transform face;
	private bool coalCarry;
	private bool bucketCarry;


	private bool moonCarry;
	private float xScale;
	private int bucketValue;
	private GameObject train;
	private Transform respawn;
	private float maxSpeed = 5f;
	private bool shoutted_log=false,shoutted_steam=false,shoutted_fire=false,shoutted_lightning=false,shoutted_asteroid=false;

	private GameObject bellowSound,putoutSound,coalSound,stoveSound,moonSound,moonStoveSound,waterSound;

	// Use this for initialization
	void Start () {
		moonCarry = false;
		bucketValue = 0;
		jumpspeed = 600f;
		ground = -1.20f;
		oy = GameObject.Find ("train").transform.position.y;
		train = GameObject.Find ("hub");
		coalCarry = bucketCarry = false;
		xScale = transform.localScale.x;
		respawn = GameObject.Find ("respawn").transform;
		button = gameObject.transform.GetChild(1);
		face = gameObject.transform.GetChild(0);
		bellowSound=GameObject.Find ("a_bellow");
		putoutSound=GameObject.Find ("a_putout");
		coalSound=GameObject.Find ("a_coal");
		stoveSound=GameObject.Find ("a_stove");
		moonSound=GameObject.Find ("a_moonrock");
		moonStoveSound=GameObject.Find ("a_moonstove");
		waterSound=GameObject.Find ("a_water");
	}
	public void shout(string _t){
		if (!getShout (_t)) {
			setShout (_t, true);
			GameObject.Find ("shout").GetComponent<Animator> ().Play (_t);
			GameObject.Find ("shout").GetComponent<SpriteRenderer> ().enabled = true;
			GameObject.Find ("Face4").GetComponent<Animator> ().Play ("facesad");
		}
	}
	public void setShout(string _t,bool _b){
		if (_t == "log") {
			shoutted_log = _b;
		} else if (_t == "steam") {
			shoutted_steam = _b;
		} else if (_t == "fire") {
			shoutted_fire  = _b;
		} else if (_t == "asteroid") {
			shoutted_asteroid = _b;
		} else if (_t == "lightning") {
			shoutted_lightning = _b;
		}
		if (!shoutted_log && !shoutted_steam && !shoutted_fire && !shoutted_asteroid && !shoutted_lightning) {
			GameObject.Find ("shout").GetComponent<SpriteRenderer> ().enabled = false;
			GameObject.Find ("Face4").GetComponent<Animator> ().Play ("faceidle");
		}
	}
	private bool getShout(string _t){
		if (_t == "log") {
			return shoutted_log;
		} else if (_t == "steam") {
			return shoutted_steam;
		} else if (_t == "fire") {
			return shoutted_fire;
		} else if (_t == "asteroid") {
			return	shoutted_asteroid ;
		} else if (_t == "lightning") {
			return shoutted_lightning;
		}
		return false;
	}
	void FixedUpdate(){
		if (Input.GetKey (up)) { 
			if(GetComponent<Rigidbody>().velocity.magnitude<maxSpeed){
				GetComponent<Rigidbody>().AddForce(Vector3.forward * speed);
			}
		} else if (Input.GetKey (down)) {
			if(GetComponent<Rigidbody>().velocity.magnitude<maxSpeed){
				GetComponent<Rigidbody>().AddForce(Vector3.back * speed);
			}
		}
		if (Input.GetKey (right)) {
			if(GetComponent<Rigidbody>().velocity.magnitude<maxSpeed){
				GetComponent<Rigidbody>().AddForce(Vector3.right * speed);
			}
			transform.localScale = new Vector3 (-xScale, transform.localScale.y, transform.localScale.z);
		} else if (Input.GetKey (left)) {
			if(GetComponent<Rigidbody>().velocity.magnitude<maxSpeed){
				GetComponent<Rigidbody>().AddForce(Vector3.left * speed);
			}
			transform.localScale = new Vector3 (xScale, transform.localScale.y, transform.localScale.z);
		}

	}

	// Update is called once per frame
	void Update () {

		if(transform.position.y < -5){
			transform.position = respawn.position;
		}
		if (Input.GetKeyDown (action)) {
			doAction(); 
		}
		if (Input.GetKeyDown (action2)) {
			if(transform.position.y<=ground + GameObject.Find ("train").transform.position.y-oy && !coalCarry){
				GetComponent<Rigidbody>().AddForce(Vector3.up*jumpspeed);
			}
		}
		if (Input.GetKey (up) || Input.GetKey (down) || Input.GetKey (right) || Input.GetKey (left)) {
			GetComponent<Animator> ().Play ("walk");
		} else {
			GetComponent<Animator> ().Play ("idle");
		}
	}
	private void doAction(){
		Debug.Log (control);
		switch (control) {
		case "coal":
			if (!coalCarry && !bucketCarry && train.GetComponent<trainShaking> ().overheat<=0) {
				coals = Instantiate (coal, new Vector3 (transform.position.x, transform.position.y - 0.2f, transform.position.z - 0.2f), transform.rotation) as GameObject;
				coals.transform.parent = transform;
				speed -= slowdown;
				coalSound.GetComponent<AudioSource> ().Play ();
				coalCarry = true;
			}else{
				if(bucketCarry){
					print ("po");
					bucketCarry=false;
					Destroy (buckets);
					speed=originalSpeed;
					putoutSound.GetComponent<AudioSource> ().Play ();
					train.GetComponent<trainShaking>().overheat -= bucketValue;
					if(train.GetComponent<trainShaking>().overheat<0){
						train.GetComponent<trainShaking>().overheat=0;
					}
					bucketValue=0;
					print (train.GetComponent<trainShaking>().overheat );
				}
			}
			break;
		case "bucket":
			if (!bucketCarry && !coalCarry) {
				buckets = Instantiate (bucket, new Vector3 (transform.position.x, transform.position.y - 0.2f, transform.position.z - 0.2f), transform.rotation) as GameObject;
				buckets.transform.parent = transform;
				buckets.transform.localScale *= 0.1f;
				coalSound.GetComponent<AudioSource> ().Play ();
				//speed -= slowdown;
				bucketCarry = true;
			}
			break;
		
		case "stove":
			if (coalCarry) {
				speed += slowdown;
				coalCarry = false;
				Destroy (coals);
				train.GetComponent<trainShaking> ().fuel = 5;
				stoveSound.GetComponent<AudioSource> ().Play ();
			}
			if (moonCarry) {
				
				speed += slowdown;
				moonCarry = false;
				Destroy (moon);
				train.GetComponent<trainShaking> ().moonfuel = 10;
				moonStoveSound.GetComponent<AudioSource> ().Play ();
			}
			break;
		case "wheel":
			train.GetComponent<trainShaking> ().broke -= 0.1f;
			if (train.GetComponent<trainShaking> ().broke < 0f) {
				train.GetComponent<trainShaking> ().broke = 0f;
			}
			if (GameObject.Find ("a_wheel").GetComponent<AudioSource> ().time == 0) {
				GameObject.Find ("a_wheel").GetComponent<AudioSource> ().Play ();
			}
			GameObject.Find("wheel").transform.Rotate(new Vector3(0,0,10));
			break;
		case "frontbutton":
			if(train.GetComponent<trainShaking>().chop == false){
				train.GetComponent<trainShaking>().StartCoroutine("chopObj");
				stoveSound.GetComponent<AudioSource> ().Play ();
			}
			break;
		case "fusion":
			if (coalCarry) {
				moon = Instantiate (moonstone, new Vector3 (transform.position.x, transform.position.y - 0.2f, transform.position.z - 0.2f), transform.rotation) as GameObject;
				moon.transform.parent = transform;
				coalCarry = false;
				moonCarry = true;
				moonSound.GetComponent<AudioSource> ().Play ();
				Destroy (coals);
			}
			break;
		}
	}
	private void OnTriggerStay(Collider collision) {
		switch (collision.gameObject.name) {
		case "dripping":
			if (bucketCarry) {
				if (bucketValue <= 100) {
					if(waterSound.GetComponent<AudioSource>().time==0){
						waterSound.GetComponent<AudioSource>().Play();
					}
					bucketValue++;
					buckets.transform.localScale = buckets.transform.localScale * 1.01f;
					originalSpeed = speed;
					speed *= 0.9998f;
				} else {
					buckets.GetComponent<Animator> ().Play ("full");
				}
			}
			control = "waterpipe";
			break;
		}
	}
	private void OnTriggerEnter(Collider collision) {
		string _collision = collision.gameObject.name;
	
		switch (_collision) {
		case "coal":
			control = "coal";
			break;
		case "stove":
			control = "stove";
			break;
		case "bucket":
			control = "bucket";
			break;
		case "bellow":
			if(GetComponent<Rigidbody>().velocity.y < 0){
				bellowSound.GetComponent<AudioSource> ().Play ();
				GameObject.Find("bellow").GetComponent<Animator>().Play("Bellow");
				if(train.GetComponent<trainShaking>().fuel >= 1){
					train.GetComponent<trainShaking>().speedUp(0.005f);
					train.GetComponent<trainShaking>().fuel -= 1;
					face.GetComponent<Animator>().Play ("facehappy");
				}
				if(train.GetComponent<trainShaking>().moonfuel >= 1){
					train.GetComponent<trainShaking>().speedUp(0.010f);
					train.GetComponent<trainShaking>().moonfuel -= 1;
				}
			}
			break;
		case "wheel":
			control = "wheel";
			break;
		case "frontbutton":
			control = "frontbutton";
			break;
		case "fusion":
			control = "fusion";
			break;
		}
		if(_collision != "dripping"){
		button.GetComponent<SpriteRenderer> ().enabled = true;
		}
		if (_collision == "bellow") {
			button.GetComponent<Animator> ().Play ("buttonAnimB");
		} else {
			button.GetComponent<Animator> ().Play ("buttonAnimA");
		}

	}
	private void OnTriggerExit(Collider collision) {
		string _collision = collision.gameObject.name;
		button.GetComponent<SpriteRenderer> ().enabled = false;
		if(control==_collision){
			control="";
		}
	}
}
