using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PublicFunctions : MonoBehaviour {

	public WeaponController AttackFuntcion(WeaponController[] _weapons, HealthController _health, float _distance, float _time){
		if(Time.time > _time){
			List<WeaponController> inReach = new List<WeaponController> ();
			for(int i = 0; i < _weapons.Length; i++){
				if(_weapons[i].GetReach() > _distance){
					inReach.Add (_weapons [i]);
				}
			}
			List<WeaponController> canAfford= new List<WeaponController> ();
			foreach(WeaponController WC in inReach){
				if(WC.GetManaCost() < _health.GetCurrentMana()){
					canAfford.Add (WC);
				}
			}
			WeaponController selectedAttack = null;
			float highestCooldown = 0;
			foreach(WeaponController WC in canAfford){
				if(WC.GetAttackCoolDown() > highestCooldown){
					selectedAttack = WC;
				}
			}
			return selectedAttack;
		}
		return null;
	}

	public void MoveTowards(GameObject _victim, Vector3 _target, float _moveSpeed){
		_victim.transform.position = Vector3.MoveTowards (_victim.transform.position, _target, _moveSpeed *Time.deltaTime);
		Vector3 displacement = _target - _victim.transform.position;
		Debug.Log (displacement);
		float theta = Mathf.Atan2 ( displacement.x, displacement.z) * Mathf.Rad2Deg;
		Debug.Log (theta);
		_victim.transform.rotation = Quaternion.RotateTowards (_victim.transform.rotation, Quaternion.Euler(new Vector3(0, theta,0)), 180 * Time.deltaTime);
		//_victim.transform.eulerAngles = Vector3.MoveTowards(_victim.transform.eulerAngles, new Vector3(0,theta,0), 180 * Time.deltaTime);
	}

	public bool ExitTrigger(Collider trigger, SphereCollider _radius, GameObject _this){
		if(trigger.CompareTag("Player")){
			Vector3 Vdistance = _this.transform.position - trigger.gameObject.transform.position;
			float distance = Vdistance.sqrMagnitude;
			if (distance > _radius.radius) {
				return true;
			}
		}
		return false;
	}
}


