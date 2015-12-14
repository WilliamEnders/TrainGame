using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class trainShaking : MonoBehaviour {

	// Use this for initialization
	private Vector3 op;
	private Vector3 op2;
	public bool chop;
	public int buttonCount;
	public int fuel;
	public int overheat;
	public float broke;
	private float bump;
	public float speed;

	public GameObject fire;
	public GameObject steam;
	private GameObject overheatFire;
	private GameObject maintrack;
	public GameObject p1,p2,p3,p4;
	private GameObject mph;

	void Start () {
		overheat = 0;
		chop = false;
		op.x -= 10f;
		op2.x -= 10f;
		speed = 0f;
		bump = 1f;
		broke = 0f;
		fuel = 0;
		StartCoroutine("drippingObj");

		maintrack = GameObject.Find ("maintrack");
		overheatFire = GameObject.Find ("overheatFire");
		steam = GameObject.Find ("steam");
		op = GameObject.Find ("train").transform.position;
		mph = GameObject.Find ("mph");

		p1 = GameObject.Find ("p1");
		p2 = GameObject.Find ("p2");
		p3 = GameObject.Find ("p3");
		p4 = GameObject.Find ("p4");
	}

	// Update is called once per frame
	public void trainBump(){
		if(!chop){
		broke = 1f;
		bump = 20;
		bumpPlayer(p1);
		bumpPlayer(p2);
		bumpPlayer(p3);
		bumpPlayer(p4);
		GameObject.Find ("a_explosion").GetComponent<AudioSource> ().Play ();
		speedDown ();
		}
	}

	void bumpPlayer(GameObject _p){
		if (_p) {
			_p.GetComponent<Rigidbody> ().AddExplosionForce (1000f, _p.transform.position + new Vector3 (Random.Range (-0.5f, 0.5f), Random.Range (-0.5f, 0.5f), Random.Range (-0.5f, 0.5f)), 10f);
		}
	}

	void Update () {

		if(chop){

		}
		if (overheat <= 0) {
			if (Random.Range (0,1000)  == 1) {
				overheat = 100;
			}
		}

		overheatFire.GetComponent<ParticleSystem> ().emissionRate = overheat ;

		fire.GetComponent<ParticleSystem> ().emissionRate = fuel * 4;


		steam.GetComponent<ParticleSystem> ().emissionRate = fuel * 2+10;
		mph.GetComponent<Text> ().text = (speed*100f).ToString() + "MPH";
		speedDown (broke / 10000f);

		GameObject.Find ("brokepipe").GetComponent<ParticleSystem> ().emissionRate = broke*100;

		steam.GetComponent<ParticleSystem> ().startSpeed = speed * 10f;
		steam.GetComponent<ParticleSystem> ().emissionRate = speed > 0f ? 10f  : 0f;


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
			maintrack.transform.Translate(Vector3.left*speed);
			GameObject.Find ("trees").transform.Translate(Vector3.left*speed);

			if(maintrack.transform.position.x<=-18.49f){
				maintrack.transform.Translate(Vector3.right*(33.49f));

			}
		}
		int isStop = (speed > 0) ? 1 : 0;
		GameObject.Find ("train").transform.position = op + Random.Range(0f,0.05f) * Vector3.up*bump*isStop;
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
		} else {
		speed -= _n;
		}
		if(speed < 0f){
			speed = 0f;
		}
		
	}
	public IEnumerator drippingObj(){

		yield return new WaitForSeconds(5);
		GameObject.Find ("dripping").GetComponent<ParticleSystem> ().Play ();
		print ("s");
		Vector3 _v = new Vector3(GameObject.Find ("TrainCar2").transform.position.x+Random.Range (-2, 2),GameObject.Find ("dripping").transform.position.y,GameObject.Find ("dripping").transform.position.z);
		GameObject.Find ("dripping").transform.position = _v;
		StartCoroutine("drippingObj");
	}

	public IEnumerator chopObj(){
		chop = true;
		GameObject.Find ("frontchop").GetComponent<MeshRenderer> ().enabled = true;
		yield return new WaitForSeconds(2);
		chop = false;
		GameObject.Find ("frontchop").GetComponent<MeshRenderer> ().enabled = false;
	}
	
}
