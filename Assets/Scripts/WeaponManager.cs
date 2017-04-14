using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {

	public static WeaponManager instance;

	private WeaponController[] equippedWeapons = new WeaponController[7];
	private List<WeaponController> weapons = new List<WeaponController> ();//{null, null, null, null, null, null, null, null, null, null};
	[SerializeField]private Image[] weaponImages;

	private WeaponController lastWeapon = new WeaponController(), nextWeapon, currentWeapon;
	private float lastAttack;

	private HealthController myHealthController;

	void Awake(){
		if(instance != null){
			Object.Destroy (gameObject);
		}else{
			instance = this;
			Object.DontDestroyOnLoad (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		myHealthController = GetComponent<HealthController> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		for(int i =1; i< equippedWeapons.Length; i++){
			if(Input.GetButtonDown("Weapon" + (i+1))){
				if(equippedWeapons[i]){
					nextWeapon = equippedWeapons [i];
				}
			}
		}

		if(Input.GetKeyDown(KeyCode.I)){
			currentWeapon = null;
		}
	}

	void OnDisable(){
		currentWeapon = null;
	}

	public void AddWeapon(WeaponController _weapon){
		weapons.Add (_weapon);
	}

	public void EquipWeapon(int _index){
		equippedWeapons [_index] = null;
		weaponImages [_index - 1].sprite = null;
		if (currentWeapon) {
			for (int i = 1; i < equippedWeapons.Length; i++) {
				if (equippedWeapons [i] == currentWeapon) {
					equippedWeapons [i] = null;
					weaponImages [i - 1].sprite = null;
				}
			}
			equippedWeapons [_index] = currentWeapon;
			weaponImages [_index - 1].sprite = currentWeapon.GetSprite ();
		}
		currentWeapon = null;
	}

	public void EquipPrimary(WeaponController _weaponC){
		equippedWeapons [0] = _weaponC;
		nextWeapon = equippedWeapons [0];
	}

	public void HoldWeapon(int _index){
		if(_index < weapons.Count){
			currentWeapon = weapons [_index];
		}
	}


	public void UnequipPrimary(){
		equippedWeapons [0] = null;
	}

	public float GetNextWeaponSqrRange(){
		if (nextWeapon) {
			return Mathf.Pow (nextWeapon.GetReach (), 2);
		} else {
			return 0;
		}
	}

	public void TryAttack(){
		if(!nextWeapon){
			nextWeapon = equippedWeapons [0];
		}
		if(Time.time > lastAttack + lastWeapon.GetAttackCooldown()){
			if (nextWeapon.GetManaCost () <= myHealthController.GetCurrentMana ()) {
				Debug.Log (nextWeapon.gameObject.name);
				nextWeapon.Attack ();
				lastWeapon = nextWeapon;
				lastAttack = Time.time;
				nextWeapon = null;
			}
		}
	}

	public WeaponController[] GetEquippedWeapons(){
		return equippedWeapons;
	}

	public int GetNextWeapon(){
		for(int i = 0; i < 7; i++){
			if(equippedWeapons[i]){
				if(equippedWeapons[i] == nextWeapon){
					return i;
				}
			}
		}
		return 0;
	}

	public WeaponController GetPrimaryWeapon(){
		return equippedWeapons [0];
	}

	public void CloseWindow(){
		currentWeapon = null;
	}
	public List<WeaponController> GetAllWeapons(){
		return weapons;
	} 

}
