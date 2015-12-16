using UnityEngine;
using System.Collections;

public class obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.x < 4) {
			GameObject.Find ("hub").GetComponent<trainShaking> ().trainBump ();
			transform.Translate(Vector3.right*100);
			GameObject.Find ("p4").GetComponent<player> ().setShout ("log",false);
		}
		if (transform.position.x <= 15) {
			GameObject.Find ("p4").GetComponent<player> ().shout ("log");
		}
	}
}
