using UnityEngine;
using System.Collections;

public class tree : MonoBehaviour {
	public int distance=40;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -distance) {
			transform.Translate(Vector3.right*2*distance);
		}
	}
}
