using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class trainSpeedo : MonoBehaviour {

	public Slider slide;
	private float currentSpeed;
	private float maxSpeed;
	private trainShaking train;

	// Use this for initialization
	void Start () {
		maxSpeed= GameObject.Find ("hub").GetComponent<trainShaking> ().maxSpeed;
		train = GameObject.Find ("hub").GetComponent<trainShaking> ();
		currentSpeed = train.speed;
	}
	
	// Update is called once per frame
	void Update () {
		currentSpeed = (train.speed ) / maxSpeed;

		GetComponent<Slider> ().value = currentSpeed;
	}
}
