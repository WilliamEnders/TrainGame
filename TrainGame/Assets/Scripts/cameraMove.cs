using UnityEngine;
using System.Collections;
using System.Linq;

public class cameraMove : MonoBehaviour {

	public float distance;
	public Transform[] obj;
	private Vector3 startPos;
	private Vector3 endPos;
	private float zoom;
	private float average;

	// Use this for initialization
	void Start () {
		zoom = -8;
		average = 0;
	}
	
	// Update is called once per frame
	void Update () {

		findAverage ();

		transform.position = new Vector3(average,1f,zoom);
		
	}

	void findAverage(){
		float[] xVals = new float[obj.Length];

		for (int i = 0; i < obj.Length; i++) {

			xVals[i] = obj[i].position.x;

		}
		average = (xVals.Max() + xVals.Min()) / 2;
		
		if(xVals.Max() - xVals.Min() > 8f){
			zoom = -(xVals.Max() - xVals.Min());
		}

	}

}
