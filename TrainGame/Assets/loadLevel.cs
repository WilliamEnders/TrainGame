using UnityEngine;
using System.Collections;

public class loadLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void load1(){
		Application.LoadLevel("Level1");
	}
	public void load2(){
		Application.LoadLevel("Level2");
	}
	public void load3(){
		Application.LoadLevel("Level3");
	}
}
