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
	private bool coalCarry;
	private bool bucketCarry;

	private float xScale;
	private int bucketValue;
	private GameObject train;
	private Transform respawn;
	private float maxSpeed = 5f;
	private bool justteleported;
	private int drippingCount = 0;
	// Use this for initialization
	void Start () {
		justteleported = false;
		bucketValue = 0;
		jumpspeed = 600f;
		ground = -1.20f;
		oy = GameObject.Find ("train").transform.position.y;
		train = GameObject.Find ("hub");
		coalCarry = bucketCarry = false;
		xScale = transform.localScale.x;
		respawn = GameObject.Find ("respawn").transform;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(transform.position.y < -5){

			transform.position = respawn.position;

		}
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
				coalCarry = true;
			}else{
				if(bucketCarry){
					print ("po");
					bucketCarry=false;
					Destroy (buckets);
					speed=originalSpeed;
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
			}
			break;
		case "wheel":
			train.GetComponent<trainShaking>().broke -=0.1f;
			if(train.GetComponent<trainShaking>().broke<0f)train.GetComponent<trainShaking>().broke=0f;
			GameObject.Find ("a_wheel").GetComponent<AudioSource> ().Play ();
			GameObject.Find("wheel").transform.Rotate(new Vector3(0,0,10));
			break;
		case "frontbutton":
			if(train.GetComponent<trainShaking>().chop == false){
			train.GetComponent<trainShaking>().StartCoroutine("chopObj");
			}
			break;
		}
	}
	private void OnTriggerStay(Collider collision) {
		switch (collision.gameObject.name) {
		case "dripping":
			if (bucketCarry) {
				if(bucketValue<=100){
					bucketValue++;
					buckets.transform.localScale = buckets.transform.localScale * 1.01f;
					originalSpeed=speed;
					speed *= 0.9998f;
				}
			}
			control = "waterpipe";
			break;
		}
	}
	private void OnTriggerEnter(Collider collision) {
		switch (collision.gameObject.name) {
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
				GameObject.Find("bellow").GetComponent<Animator>().Play("Bellow");
				if(train.GetComponent<trainShaking>().fuel >= 1){
					train.GetComponent<trainShaking>().speedUp(0.005f);
					train.GetComponent<trainShaking>().fuel -= 1;
				}
			}
			break;
		case "wheel":
			control = "wheel";
			break;
		case "teleport1":
			if(!justteleported){
				justteleported=true;
				transform.position = new Vector3(GameObject.Find ("teleport2").transform.position.x - 0.6f,transform.position.y,transform.position.z);
			}
				break;
		case "teleport2":
			if(!justteleported){
				justteleported=true;
				transform.position = new Vector3(GameObject.Find ("teleport1").transform.position.x + 0.6f,transform.position.y,transform.position.z);
			}
			break;
		case "teleport3":
			if(!justteleported){
				justteleported=true;
				transform.position = new Vector3(GameObject.Find ("teleport4").transform.position.x - 0.6f,transform.position.y,transform.position.z);
			}
			break;
		case "teleport4":
			if(!justteleported){
				justteleported=true;
				transform.position = new Vector3(GameObject.Find ("teleport3").transform.position.x + 0.6f,transform.position.y,transform.position.z);
			}
			break;
		case "frontbutton":
			control = "frontbutton";
			break;
		}
	}
	private void OnTriggerExit(Collider collision) {
		if (collision.gameObject.name == "teleport1" || collision.gameObject.name == "teleport2" || collision.gameObject.name == "teleport3" || collision.gameObject.name == "teleport4") {
			justteleported = false;
		}
		if(control==collision.gameObject.name){
			control="";
		}
	}
}
