using UnityEngine;
using System.Collections;

public class obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -30) {
			GameObject.Find ("hub").GetComponent<trainShaking> ().trainBump ();
			transform.Translate(Vector3.right*150);
		}
	}
}
