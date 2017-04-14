using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class PackEnemyAI : MonoBehaviour {

	private PublicFunctions publicFunctions;

	private bool aggro = false;
	private GameObject target = null;
	[SerializeField]private float reach;
	[SerializeField]private float moveSpeed;
	[SerializeField]private WeaponController[] weapons;
	[SerializeField]private SphereCollider myRadius;
	private float attackDelay;
	private float lastAttack;

	private HealthController myHealth;

	private GameObject closestAlly;
	private List<GameObject> allies = new List<GameObject>();
	private bool desperate;

	private UnityAction PackEnemyDeath;
	private bool dead = false;

	void Awake(){
		publicFunctions = new PublicFunctions();
	}

	// Use this for initialization
	void Start () {
		CheckForAllies ();
		myHealth = GetComponent<HealthController> ();
	}
	
	// Update is called once per frame
	void Update () {
		WeaponController selectedAttack = null;
		if(desperate){
			if (closestAlly) {
				publicFunctions.MoveTowards (gameObject, closestAlly.transform.position, moveSpeed);
				Vector3 displacement = transform.position - closestAlly.transform.position; 
				if (displacement.sqrMagnitude < 25) {
					desperate = false;
				}
				return;
			}
		}
		if(target != null){
			selectedAttack = publicFunctions.AttackFuntcion (weapons, myHealth, Vector3.Distance(transform.position, target.transform.position), lastAttack + attackDelay);
			if (selectedAttack) {
				selectedAttack.Attack ();
				attackDelay = selectedAttack.GetAttackDelay ();
				lastAttack = Time.time;
				return;
			}
			publicFunctions.MoveTowards (gameObject, target.transform.position, moveSpeed);
		}
	}

	void OnEnable(){
		EventManager.StartListening ("PackEnemyDeath", CheckForAllies);
	}

	void OnDisable(){
		EventManager.TriggerEvent ("PackEnemyDeath");
	}

	void OnTriggerEnter(Collider trigger){
		if(trigger.CompareTag("Player")){
			Debug.Log (trigger);
				aggro = true;
				target = trigger.gameObject;
		}
	}

	void OnTriggerExit(Collider trigger){
		if (publicFunctions.ExitTrigger (trigger, myRadius, gameObject)) {
			target = null;
			aggro = false;
		}
	}

	private void CheckForAllies(){
		//Debug.Log ("checking");
		Collider[] localAllies = Physics.OverlapSphere (transform.position, 15);
		foreach (Collider LA in localAllies) {
			if (LA.tag == "enemy") {
				return;
			}
		}
		desperate = true;
		allies = new List<GameObject> ();
		Collider[] items = Physics.OverlapSphere (transform.position, 15);
		foreach (Collider i in items) {
			if (i.tag == "enemy") {
				allies.Add (i.gameObject);
			}
		}
		closestAlly = null;
		if (allies.Count > 0) {
			Vector3 displacement = allies [0].transform.position - transform.position;
			closestAlly = allies [0];
			foreach (GameObject ally in allies) {
				Vector3 newDisplacement = ally.transform.position - transform.position;
				if (newDisplacement.sqrMagnitude < displacement.sqrMagnitude) {
					closestAlly = ally;
				}
			}
		}else {
			aggro = true;
			return;
		}
	}

}
