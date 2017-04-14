using UnityEngine;
using System.Collections;

public class BroodEnemyAI : MonoBehaviour {

	private bool aggro;
	private GameObject target;
	[SerializeField]private float reach = 1;
	[SerializeField]private float moveSpeed = 1;
	[SerializeField]private WeaponController[] weapons;
	private float attackDelay;
	private float lastAttack;

	private HealthController myHealth;
	private float eggTimeDelay = 5;
	private float eggTime = 0;
	[SerializeField] private GameObject egg;
	private int babyCount;

	private PublicFunctions publicFunctions;
	[SerializeField]private SphereCollider myRadius;

	void Awake(){
		publicFunctions = new PublicFunctions();
	}

	// Use this for initialization
	void Start () {
		myHealth = GetComponent<HealthController> ();
	}
	
	// Update is called once per frame
	void Update () {
		WeaponController selectedAttack = null;
		LayEgg ();
		if (target != null) {
			selectedAttack = publicFunctions.AttackFuntcion (weapons, myHealth, Vector3.Distance (transform.position, target.transform.position), lastAttack + attackDelay);
			if (selectedAttack) {
				selectedAttack.Attack ();
				attackDelay = selectedAttack.GetAttackDelay ();
				lastAttack = Time.time;
			}
		}
	}

	void OnTriggerEnter(Collider trigger){
		if(trigger.CompareTag("Player")){
			aggro = true;
			target = trigger.gameObject;
		}
	}

	void OnTriggerExit(Collider trigger){
		if (trigger && target) {
			if (publicFunctions.ExitTrigger (trigger, myRadius, gameObject)) {
				target = null;
				aggro = false;
			}
		}
	}

	void LayEgg(){
		if(Time.time > eggTime + eggTimeDelay){
			if(babyCount < 5){
				eggTime = Time.time;
				Vector3 position = new Vector3(-Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad),0,-Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad));
				/*
				EggEnemyAI newEgg = Object.Instantiate (egg, position, Quaternion.Euler(new Vector3(0, Random.Range(0,360),0))) as EggEnemyAI;
				newEgg.SetParent (this);
				*/
				GameObject newEgg = Object.Instantiate (egg, position, Quaternion.Euler(new Vector3(0, Random.Range(0,360),0))) as GameObject;
				newEgg.GetComponent<EggEnemyAI>(). SetParent (this);
				babyCount++;

			}
		}
	}

	public void ChildDeath(){
		babyCount--;
	}

}
