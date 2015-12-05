using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class trainShaking : MonoBehaviour {

	// Use this for initialization
	public float speed;
	private Vector3 op;
	private Vector3 op2;
	private float bump;
	public int buttonCount;
	public float broke;
	private GameObject mph;
	public int fuel;

	public GameObject fire;
	public GameObject steam;

	void Start () {
		op = GameObject.Find ("train").transform.position;
		//op2 = GameObject.Find ("train2").transform.position;
		op.x -= 10f;
		op2.x -= 10f;
		speed = 0f;
		bump = 1f;
		broke = 0f;
		mph = GameObject.Find ("MPH");
		fuel = 0;
		steam=GameObject.Find ("steam");
	}

	// Update is called once per frame
	public void trainBump(){
		broke = 1f;
		bump = 20;
		if(GameObject.Find ("p1")) GameObject.Find ("p1").GetComponent<Rigidbody> ().AddExplosionForce (1000f,  GameObject.Find ("p1").transform.position+new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)), 10f);
		if(GameObject.Find ("p2")) GameObject.Find ("p2").GetComponent<Rigidbody> ().AddExplosionForce (1050f, GameObject.Find ("p2").transform.position+new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)), 10f);
		if(GameObject.Find ("p3")) GameObject.Find ("p3").GetComponent<Rigidbody> ().AddExplosionForce (1050f, GameObject.Find ("p3").transform.position+new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)), 10f);
		if(GameObject.Find ("p4")) GameObject.Find ("p4").GetComponent<Rigidbody> ().AddExplosionForce (1000f, GameObject.Find ("p4").transform.position+new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)), 10f);
		GameObject.Find ("a_explosion").GetComponent<AudioSource> ().Play ();
		speedDown ();

	}
	void Update () {
		fire.GetComponent<ParticleSystem> ().emissionRate = fuel * 4;
		steam.GetComponent<ParticleSystem> ().emissionRate = fuel * 2+10;
		GameObject.Find ("mph").GetComponent<Text> ().text = (speed*100f).ToString() + "MPH";
		speedDown (broke / 10000f);
		GameObject.Find ("brokepipe").GetComponent<ParticleSystem> ().emissionRate = broke*100;

		GameObject.Find ("steam").GetComponent<ParticleSystem> ().startSpeed = speed * 10f;
		GameObject.Find ("steam").GetComponent<ParticleSystem> ().emissionRate = speed > 0f ? 10f  : 0f;


		if (bump > 1f) {
			bump -= 0.4f;
		} else {
			bump = 1f;
		}
		if (op.x < -0.9f) {
			op.x += 0.05f;
		}
		if (op2.x < -9.47f) {
			op2.x += 0.05f;
		}else{
			GameObject.Find ("maintrack").transform.Translate(Vector3.left*speed);
			GameObject.Find ("trees").transform.Translate(Vector3.left*speed);

			if(GameObject.Find ("maintrack").transform.position.x<=-18.49f){
				GameObject.Find ("maintrack").transform.Translate(Vector3.right*(33.49f));

			}
		}
		int isStop = (speed > 0) ? 1 : 0;
		GameObject.Find ("train").transform.position = op + Random.Range(0f,0.05f) * Vector3.up*bump*isStop;
		//GameObject.Find ("train2").transform.position = op2 + Random.Range(0f,0.05f) * Vector3.up*bump;
	}

	public void speedUp(float incSpeed){
		if(speed < 10f){
		speed += incSpeed;
			GameObject.Find ("Face").GetComponent<Animator>().Play ("facehappy");
		}
	}
	public void speedDown(float _n = -1f){
		if (_n == -1f) {
			speed -= 0.01f;
			//op+=Vector3.up*1f;
			//Camera.main.transform.Translate(Vector3.up*1f);
			//Camera.main.transform.Translate(Vector3.back*1f);
		} else {
		speed -= _n;
		}
		if(speed < 0f){
			speed = 0f;
		}
		
	}
}
