using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class trainSpeedo : MonoBehaviour {

	public Slider slide;
	private float currentSpeed;
	public float maxSpeed;
	private trainShaking train;

	// Use this for initialization
	void Start () {
		train = GameObject.Find ("hub").GetComponent<trainShaking> ();
		currentSpeed = train.speed;
	}
	
	// Update is called once per frame
	void Update () {
		currentSpeed = (train.speed * 100) / maxSpeed;

		GetComponent<Slider> ().value = currentSpeed;
	}
}
