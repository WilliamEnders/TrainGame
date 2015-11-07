using UnityEngine;
using System.Collections;

public class trainShaking : MonoBehaviour {

	// Use this for initialization
	void Start () {
		op = GameObject.Find ("train").transform.position;
		op2 = GameObject.Find ("train2").transform.position;
	}
	private Vector3 op;
	private Vector3 op2;
	// Update is called once per frame
	void Update () {
		GameObject.Find ("train").transform.position = op + Random.Range(0f,0.1f) * Vector3.up;
		GameObject.Find ("train2").transform.position = op2 + Random.Range(0f,0.1f) * Vector3.up;
	}
}
