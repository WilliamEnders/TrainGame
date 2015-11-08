using UnityEngine;
using System.Collections;

public class tree : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -40f) {
			transform.Translate(Vector3.right*100f);
		}
	}
}
