using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		speed = 0.1f;
		jumpspeed = 600f;
		ground = -1.20f;
		oy = GameObject.Find ("train").transform.position.y;
	}
	private float oy;
	private float speed;
	private float jumpspeed;
	private float ground;
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("w")){ 
			transform.Translate(transform.forward*speed);
		}
		else if (Input.GetKey("s")) {
			transform.Translate(-transform.forward*speed);
		}
		if (Input.GetKey ("d")) {
			transform.Translate (transform.right * speed); 
			transform.rotation = new Quaternion (0, 180, 0, 0);
		} else if (Input.GetKey ("a")) {
			transform.Translate (-transform.right * speed); 
			transform.rotation = new Quaternion (0, 0, 0, 0);
		}
		if (Input.GetKeyDown ("space")) {
			if(transform.position.y<=ground + GameObject.Find ("train").transform.position.y-oy){
				GetComponent<Rigidbody>().AddForce(Vector3.up*jumpspeed);
			}
		}
		if (Input.GetKey ("w") || Input.GetKey ("a") || Input.GetKey ("s") || Input.GetKey ("d")) {
			GetComponent<Animator> ().Play ("walk");
		} else {
			GetComponent<Animator> ().Play ("idle");
		}
	}
}
