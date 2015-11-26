﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {
	
	private float oy;
	public float speed;
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
	private GameObject coals;
	private bool coalCarry;

	private float xScale;

	private GameObject train;
	private Transform respawn;

	
	// Use this for initialization
	void Start () {
		jumpspeed = 600f;
		ground = -1.20f;
		oy = GameObject.Find ("train").transform.position.y;
		train = GameObject.Find ("hub");
		coalCarry = false;
		xScale = transform.localScale.x;
		respawn = GameObject.Find ("respawn").transform;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(transform.position.y < -5){

			transform.position = respawn.position;

		}

		if (Input.GetKey (up)){ 
			transform.Translate(transform.forward * speed * Time.deltaTime);
		}
		else if (Input.GetKey(down)) {
			transform.Translate(-transform.forward * speed * Time.deltaTime);
		}
		if (Input.GetKey (right)) {
			transform.Translate (transform.right * speed * Time.deltaTime); 
			transform.localScale = new Vector3(-xScale,transform.localScale.y,transform.localScale.z);
		} else if (Input.GetKey (left)) {
			transform.Translate (-transform.right * speed * Time.deltaTime); 
			transform.localScale = new Vector3(xScale,transform.localScale.y,transform.localScale.z);
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
		switch(control){
		case "coal":

			if(!coalCarry){
			coals = Instantiate(coal,new Vector3(transform.position.x,transform.position.y - 0.2f,transform.position.z - 0.2f),transform.rotation) as GameObject;
			coals.transform.parent = transform;
			speed -= slowdown;
			coalCarry = true;
			}
			break;

		case "stove":
			if(coalCarry){
			speed += slowdown;
			coalCarry = false;
			Destroy (coals);
				if(train.GetComponent<trainShaking>().fuel == 0){
			train.GetComponent<trainShaking>().fuel = 5;
			}
			}
			break;
		case "wheel":
			train.GetComponent<trainShaking>().broke -=10;
			if(train.GetComponent<trainShaking>().broke<0f)train.GetComponent<trainShaking>().broke=0f;
			GameObject.Find ("a_wheel").GetComponent<AudioSource> ().Play ();
			GameObject.Find("wheel").transform.Rotate(new Vector3(0,0,10));
			Debug.Log("a");
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
		}
	}
	private void OnTriggerExit(Collider collision) {
		if(control==collision.gameObject.name){
			control="";
		}
	}
}