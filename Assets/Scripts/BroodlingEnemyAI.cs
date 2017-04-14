using UnityEngine;
using System.Collections;

public class BroodlingEnemyAI : MonoBehaviour {

	private BroodEnemyAI parent;

	private bool aggro;
	private GameObject target;
	[SerializeField]private float moveSpeed = 1;
	private float attackDelay;
	private float lastAttack;

	private HealthController myHealth;
	[SerializeField]private WeaponController[] weapons;

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

	public void SetParent(BroodEnemyAI _parent){
		parent = _parent;
	}

	private void Death(){
		parent.ChildDeath ();
	}
}
