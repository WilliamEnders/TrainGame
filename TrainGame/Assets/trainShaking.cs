﻿using UnityEngine;
using System.Collections;

public class trainShaking : MonoBehaviour {

	// Use this for initialization
	public float speed;
	void Start () {
		op = GameObject.Find ("train").transform.position;
		op2 = GameObject.Find ("train2").transform.position;
		op.x -= 10f;
		op2.x -= 10f;
		speed = 0.24f;
	}
	private Vector3 op;
	private Vector3 op2;
	// Update is called once per frame
	void Update () {
		if (op.x < -0.9f) {
			op.x += 0.05f;
		}
		if (op2.x < -9.47f) {
			op2.x += 0.05f;
		}else{
			GameObject.Find ("maintrack").transform.Translate(Vector3.left*speed);
			GameObject.Find ("trees").transform.Translate(Vector3.left*speed);

			if(GameObject.Find ("maintrack").transform.position.x<=-22.4f){
				GameObject.Find ("maintrack").transform.Translate(Vector3.right*22.4f);

			}
		}
		GameObject.Find ("train").transform.position = op + Random.Range(0f,0.05f) * Vector3.up;
		GameObject.Find ("train2").transform.position = op2 + Random.Range(0f,0.05f) * Vector3.up;
	}
}
