using UnityEngine;
using System.Collections;

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
	
	// Use this for initialization
	void Start () {
		jumpspeed = 600f;
		ground = -1.20f;
		oy = GameObject.Find ("train").transform.position.y;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (up)){ 
			transform.Translate(transform.forward * speed * Time.deltaTime);
		}
		else if (Input.GetKey(down)) {
			transform.Translate(-transform.forward * speed * Time.deltaTime);
		}
		if (Input.GetKey (right)) {
			transform.Translate (transform.right * speed * Time.deltaTime); 
			transform.rotation = new Quaternion (0, 180, 0, 0);
		} else if (Input.GetKey (left)) {
			transform.Translate (-transform.right * speed * Time.deltaTime); 
			transform.rotation = new Quaternion (0, 0, 0, 0);
		}
		if (Input.GetKeyDown (action)) {
			doAction(); 
		}
		if (Input.GetKeyDown (action2)) {
			if(transform.position.y<=ground + GameObject.Find ("train").transform.position.y-oy){
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
		switch(control){
		case "coal":
			GetComponent<Rigidbody>().AddForce(Vector3.right*1000f);
			GetComponent<Rigidbody>().AddForce(Vector3.up*1000f);
			break;
		}
	}

	private void OnCollisionEnter(Collision collision) {
		switch(collision.gameObject.name){
		case "coal":
			control="coal";
			break;
		}
	}
	private void OnCollisionExit(Collision collision) {
		if(control==collision.gameObject.name){
			control="";
		}
	}
}
