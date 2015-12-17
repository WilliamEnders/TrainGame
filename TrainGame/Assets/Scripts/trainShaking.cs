using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class trainShaking : MonoBehaviour {

	// Use this for initialization
	public Vector3 op;
	private Vector3 op2;
	public bool chop;
	public int buttonCount;
	public int fuel;
	public int moonfuel;
	public int overheat;
	public float broke;
	private float bump;
	public float speed;
	public float maxSpeed;

	public GameObject fire;
	public GameObject magicfire;
	public GameObject steam;
	private GameObject overheatFire;
	private GameObject maintrack;
	public GameObject p1,p2,p3,p4;
	private GameObject trainSound;
	private GameObject fireSound;
	private GameObject steamSound;
	public int level=0;

	void Start () {
		overheat = 0;
		chop = false;
		op.x -= 10f;
		op2.x -= 10f;
		speed = 0f;
		bump = 1f;
		broke = 0f;
		fuel = 0;
		//StartCoroutine("drippingObj");

		maintrack = GameObject.Find ("maintrack");
		overheatFire = GameObject.Find ("overheatFire");
		steam = GameObject.Find ("steam");
		op = GameObject.Find ("train").transform.position;
		p1 = GameObject.Find ("p1");
		p2 = GameObject.Find ("p2");
		p3 = GameObject.Find ("p3");
		p4 = GameObject.Find ("p4");
		trainSound= GameObject.Find ("TrainSound");
		fireSound= GameObject.Find ("a_fire");
		steamSound= GameObject.Find ("a_steam");
	}

	// Update is called once per frame
	public void trainBump(){
		if(!chop){
		broke = 1f;
		bump = 20;
		//bumpPlayer(p1);
		//bumpPlayer(p2);
		//bumpPlayer(p3);
		//bumpPlayer(p4);
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
			p2.GetComponent<player> ().setShout ("fire",false);

			if (Random.Range (0, 2000-(int)Mathf.Round(speed*6000)) == 1 && level!=1) {
				overheat = 100;
				p2.GetComponent<player> ().shout ("fire");
			}
		} 
		fireSound.GetComponent<AudioSource> ().volume = overheat / 100;
			
		overheatFire.GetComponent<ParticleSystem> ().emissionRate = overheat ;

		fire.GetComponent<ParticleSystem> ().emissionRate = fuel * 4;
		magicfire.GetComponent<ParticleSystem> ().emissionRate = moonfuel * 6;
		speedDown (broke / 10000f);

		GameObject.Find ("brokepipe").GetComponent<ParticleSystem> ().emissionRate = broke*100;
		steamSound.GetComponent<AudioSource> ().volume = broke ;

		if (broke > 0) {
			p2.GetComponent<player> ().shout ("steam");
		} else {
			p2.GetComponent<player> ().setShout ("steam", false);
		}

		steam.GetComponent<ParticleSystem> ().startSpeed = speed * 10f;
		steam.GetComponent<ParticleSystem> ().emissionRate = speed > 0f ? 10f  : 0f;

		trainSound.GetComponent<AudioSource> ().pitch = speed*10;

		if (bump > 1f) {
			bump -= 0.4f;
		} else {
			bump = 1f;
		}
		if (op.x < -0.9f || speed>=maxSpeed) {
			op.x += 0.05f;
		} else {

			maintrack.transform.Translate (Vector3.left * speed);
			GameObject.Find ("trees").transform.Translate (Vector3.left * speed);

			if (maintrack.transform.position.x <= -18.49f) {
				maintrack.transform.Translate (Vector3.right * (33.49f));
			}
		}
		int isStop = (speed > 0) ? 1 : 0;
		GameObject.Find ("train").transform.position = op + Random.Range(0f,0.05f) * Vector3.up*bump*isStop;
	}

	public void speedUp(float incSpeed){
		if(speed < 10f){
		speed += incSpeed;
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

	public IEnumerator chopObj(){
		chop = true;
		GameObject.Find ("frontchop").GetComponent<MeshRenderer> ().enabled = true;
		yield return new WaitForSeconds(2);
		chop = false;
		GameObject.Find ("frontchop").GetComponent<MeshRenderer> ().enabled = false;
	}
	
}
