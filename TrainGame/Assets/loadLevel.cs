using UnityEngine;
using System.Collections;

public class loadLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.GetStreamProgressForLevel("level1") == 1){
			Application.LoadLevel("level1");
		}
	}
}
