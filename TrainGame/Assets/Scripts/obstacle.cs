using UnityEngine;
using System.Collections;

public class obstacle : MonoBehaviour {

	public string name;
	public int distance=100;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.x < 4) {
			GameObject.Find ("hub").GetComponent<trainShaking> ().trainBump ();
			transform.Translate(Vector3.right*distance);

		}
		if (transform.position.x <= 15) {
			GameObject.Find ("p2").GetComponent<player> ().shout (name);
		} else {
			GameObject.Find ("p2").GetComponent<player> ().setShout (name, false);
		}
	}
}
