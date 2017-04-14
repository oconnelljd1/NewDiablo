using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	private string ownerTag;
	[SerializeField]private float moveSpeed;
	private int power;
	private GameObject parent;
	private int pierces;
	private WeaponController myWeaponC;

	// Use this for initialization
	void Start () {
		myWeaponC = GetComponent<WeaponController> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.forward * moveSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider trigger){
		if(trigger.gameObject != parent){
			if(!trigger.isTrigger){
				pierces--;
				if(pierces < 0){
					Object.Destroy (gameObject);
				}
			}

		}
	}

	public void SetStuff(GameObject _parent, WeaponController _weaponC){
		ownerTag = _parent.tag;
		parent = _parent;
		myWeaponC = _weaponC;
		pierces = myWeaponC.GetPierces ();
	}

}
