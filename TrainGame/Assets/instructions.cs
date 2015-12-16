using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class instructions : MonoBehaviour {

	private GameObject inst;
	// Use this for initialization
	void Start () {
		inst = GameObject.Find ("instructions");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void reveal(){
		inst.GetComponent<Animator> ().Play ("instructions up");
	}
	public void hide(){
		inst.GetComponent<Animator> ().Play ("instructions down");
	}
}
