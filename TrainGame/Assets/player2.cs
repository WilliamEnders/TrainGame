using UnityEngine;
using System.Collections;

public class player2 : MonoBehaviour {
	
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
		if (Input.GetKey ("up")) transform.Translate(Vector3.forward*speed);
		else if (Input.GetKey("down")) transform.Translate(Vector3.back*speed);	
		if (Input.GetKey ("right")) transform.Translate(Vector3.right*speed);
		else if (Input.GetKey("left")) transform.Translate(Vector3.left*speed);	
		if (Input.GetKeyDown ("l")) {
			if(transform.position.y<=ground + GameObject.Find ("train").transform.position.y-oy){
				GetComponent<Rigidbody>().AddForce(Vector3.up*jumpspeed);
			}
		}
	}
}
