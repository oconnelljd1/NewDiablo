using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	//private Collider hitbox;

	[SerializeField]private bool useThis = true;

	/*[SerializeField]*/private Collider myCollider;
	[SerializeField]private Sprite mySprite;

	[SerializeField]private string ownerTag;
	[SerializeField]private bool support = false;
	[SerializeField]private bool ranged = false;
	[SerializeField]private GameObject projectile;
	[SerializeField]private int pierces;

	[SerializeField] private float attackDelay, supportDelay, attackCoolDown, manaCost, reach;

	[SerializeField]private int healthValue, healthMultiplier;
	[SerializeField]private int manaValue, manaMultiplier;
	[SerializeField]private int damageValue, damageMultiplier;
	[SerializeField]private int lightningDamageValue, lightningDamageMultiplier;
	[SerializeField]private int fireDamageValue, fireDamageMultiplier;
	[SerializeField]private int iceDamageValue, iceDamageMultiplier;

	[SerializeField]private int armorValue, armorMultiplier;
	[SerializeField]private int lightningArmorValue, lightningArmorMultiplier;
	[SerializeField]private int fireArmorValue, fireArmorMultiplier;
	[SerializeField]private int iceArmorValue, iceArmorMultiplier;

	// Use this for initialization
	void Start () {
		myCollider = GetComponent<Collider> ();
		Debug.Log (myCollider);
		myCollider.enabled = true;
		if(GetComponent<ItemController>()){
			mySprite = GetComponent<ItemController> ().GetSprite();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void Attack(){
		if (support) {
			StartCoroutine ("SupportDuration");
		} else {
			StartCoroutine ("AttackDuration");
		}

	}

	private IEnumerator AttackDuration(){
		WeaponController myWeaponC = null;
		if (useThis) {
			myWeaponC = this;
		} else {
			if(WeaponManager.instance.GetPrimaryWeapon () == ranged)
			myWeaponC = WeaponManager.instance.GetPrimaryWeapon ();
			myWeaponC = ChangeStats (myWeaponC);
		}
		GetComponentInParent<HealthController> ().BoostStats (myWeaponC, +1);
		if (ranged) {
			ProjectileController newProjectile = Instantiate (projectile, transform.position + transform.forward, transform.rotation) as ProjectileController;
			newProjectile.SetStuff (transform.parent.gameObject, myWeaponC);
		} else {
			//hitbox = transform.parent.gameObject.GetComponent<Collider> ();
			//hitbox = myCollider;
			//hitbox.enabled = true;
			Debug.Log("triying to enable shit");
			myCollider.enabled = true;
		}
		yield return new WaitForSeconds(attackDelay);
		if (!ranged) {
			//GetComponent<Collider>().enabled = false;
			myCollider.enabled = false;
		}
		GetComponentInParent<HealthController> ().BoostStats (myWeaponC, -1);
	}

	private IEnumerator SupportDuration(){
		GetComponentInParent<HealthController> ().BoostStats (this, +1);
		yield return new WaitForSeconds(supportDelay);
		GetComponentInParent<HealthController> ().BoostStats (this, -1);
	}

	public float GetAttackDelay(){
		return attackDelay;
	}

	public float GetAttackCoolDown(){
		return attackCoolDown;
	}

	public float GetManaCost(){
		return manaCost;
	}

	public float GetReach(){
		return reach;
	}

	public bool GetRanged(){
		return ranged;
	}

	public void SetProjectile(GameObject _projectile){
		projectile = _projectile;
	}

	public WeaponController ChangeStats(WeaponController _weaponC){
		_weaponC = WeaponManager.instance.GetPrimaryWeapon ();
		_weaponC.ChangeDamageValue (damageValue, 1);
		_weaponC.ChangeLightningDamageValue (lightningDamageValue, 1);
		_weaponC.ChangeFireDamageValue (fireDamageValue, 1);
		_weaponC.ChangeIceDamageValue (iceDamageValue, 1);
		_weaponC.ChangeArmorValue (armorValue, 1);
		_weaponC.ChangeLightningArmorValue (lightningArmorValue, 1);
		_weaponC.ChangeFireArmorValue (fireArmorValue, 1);
		_weaponC.ChangeIceArmorValue (iceArmorValue, 1);
		_weaponC.ChangeHealthValue (healthValue, 1);
		_weaponC.ChangeManaValue (manaValue, 1);

		_weaponC.ChangeDamageMultiplier (damageMultiplier, 1);
		_weaponC.ChangeLightningDamageMultiplier (lightningDamageMultiplier, 1);
		_weaponC.ChangeFireDamageMultiplier (fireDamageMultiplier, 1);
		_weaponC.ChangeIceDamageMultiplier (iceDamageMultiplier, 1);
		_weaponC.ChangeArmorMultiplier (armorMultiplier, 1);
		_weaponC.ChangeLightningArmorMultiplier (lightningArmorMultiplier, 1);
		_weaponC.ChangeFireArmorMultiplier (fireArmorMultiplier, 1);
		_weaponC.ChangeIceArmorMultiplier (iceArmorMultiplier, 1);
		_weaponC.ChangeHealthMultiplier (healthMultiplier, 1);
		_weaponC.ChangeManaMultiplier (manaMultiplier, 1);

		_weaponC.SetAttackCooldown (attackCoolDown);
		_weaponC.SetAttackDelay (attackDelay);
		_weaponC.SetManaCost (manaCost);
		_weaponC.SetReach (reach);

		return _weaponC;
	}

	void OnTriggerEnter(Collider trigger){
		if(trigger.gameObject.GetComponent<HealthController>()){
			if (!trigger.isTrigger) {
				if (!trigger.CompareTag (ownerTag)) {
					HealthController enemyHealth = trigger.gameObject.GetComponent<HealthController> ();
					HealthController myHealth = GetComponentInParent<HealthController> ();
					int damage = myHealth.GetCurrentDamage () - enemyHealth.GetCurrentArmor ();
					int lightningDamage = myHealth.GetCurrentLightningDamage () - enemyHealth.GetCurrentLightningArmor ();
					int fireDamage = myHealth.GetCurrentFireDamage () - enemyHealth.GetCurrentFireArmor ();
					int iceDamage = myHealth.GetCurrentIceDamage () - enemyHealth.GetCurrentIceArmor ();
					if (damage < 0) {
						damage = 0;
					}
					if (lightningDamage < 0) {
						lightningDamage = 0;
					}
					if (fireDamage < 0) {
						fireDamage = 0;
					}
					if (iceDamage < 0) {
						iceDamage = 0;
					}
					int totalDamage = damage + lightningDamage + fireDamage + iceDamage;
					enemyHealth.Damage (totalDamage);
				}
			}
		}
	}

	//attackDelay, supportDuration, attackCoolDown, manaCost, reach;

	public void SetAttackCooldown(float _float){
		attackDelay = _float;
	}

	public void SetAttackDelay(float _float){
		attackDelay = _float;
	}

	public void SetManaCost(float _float){
		attackDelay = _float;
	}

	public void SetReach(float _float){
		attackDelay = _float;
	}

	public float GetAttackCooldown(){
		return attackCoolDown;
	}

	public int GetHealthValue(){
		return healthValue;
	}

	public int GetHealthMultiplier(){
		return healthMultiplier;
	}

	public int GetManaValue(){
		return manaValue;
	}

	public int GetManaMultiplier(){
		return manaMultiplier;
	}

	public int GetDamageValue(){
		return damageValue;
	}

	public int GetDamageMultiplier (){
		return damageMultiplier;
	}

	public int GetLightningDamageValue(){
		return lightningDamageValue;
	}

	public int GetLightningDamageMultiplier (){
		return lightningDamageMultiplier;
	}

	public int GetFireDamageValue(){
		return fireDamageValue;
	}

	public int GetFireDamageMultiplier (){
		return fireDamageMultiplier;
	}

	public int GetIceDamageValue(){
		return iceDamageValue;
	}

	public int GetIceDamageMultiplier (){
		return iceDamageMultiplier;
	}

	public int GetArmorValue(){
		return armorValue;
	}

	public int GetArmorMultiplier (){
		return armorMultiplier;
	}

	public int GetLightningArmorValue(){
		return lightningArmorValue;
	}

	public int GetLightningArmorMultiplier (){
		return lightningArmorMultiplier;
	}

	public int GetFireArmorValue(){
		return fireArmorValue;
	}

	public int GetFireArmorMultiplier (){
		return fireArmorMultiplier;
	}

	public int GetIceArmorValue(){
		return iceArmorValue;
	}

	public int GetIceArmorMultiplier (){
		return iceArmorMultiplier;
	}

	public void ChangeHealthValue(int _value, int _posNeg){
		healthValue += _value * _posNeg;
	}

	public void ChangeHealthMultiplier(int _value, int _posNeg){
		healthMultiplier += _value * _posNeg;
	}

	public void ChangeManaValue(int _value, int _posNeg){
		manaValue += _value * _posNeg;
	}

	public void ChangeManaMultiplier(int _value, int _posNeg){
		manaMultiplier += _value * _posNeg;
	}

	public void ChangeDamageValue(int _value, int _posNeg){
		damageValue += _value * _posNeg;
	}

	public void ChangeLightningDamageValue(int _value, int _posNeg){
		lightningDamageValue += _value * _posNeg;
	}

	public void ChangeFireDamageValue(int _value, int _posNeg){
		fireDamageValue += _value * _posNeg;
	}

	public void ChangeIceDamageValue(int _value, int _posNeg){
		iceDamageValue += _value * _posNeg;
	}

	public void ChangeDamageMultiplier(int _value, int _posNeg){
		damageMultiplier += _value * _posNeg;
	}

	public void ChangeLightningDamageMultiplier(int _value, int _posNeg){
		lightningDamageMultiplier += _value * _posNeg;
	}

	public void ChangeFireDamageMultiplier(int _value, int _posNeg){
		fireDamageMultiplier += _value * _posNeg;
	}

	public void ChangeIceDamageMultiplier(int _value, int _posNeg){
		iceDamageMultiplier += _value * _posNeg;
	}

	public void ChangeArmorValue(int _value, int _posNeg){
		armorValue += _value * _posNeg;
	}

	public void ChangeLightningArmorValue(int _value, int _posNeg){
		lightningArmorValue += _value * _posNeg;
	}

	public void ChangeFireArmorValue(int _value, int _posNeg){
		fireArmorValue += _value * _posNeg;
	}

	public void ChangeIceArmorValue(int _value, int _posNeg){
		iceArmorValue += _value * _posNeg;
	}

	public void ChangeArmorMultiplier(int _value, int _posNeg){
		armorMultiplier += _value * _posNeg;
	}

	public void ChangeLightningArmorMultiplier(int _value, int _posNeg){
		lightningArmorMultiplier += _value * _posNeg;
	}

	public void ChangeFireArmorMultiplier(int _value, int _posNeg){
		fireArmorMultiplier += _value * _posNeg;
	}

	public void ChangeIceArmorMultiplier(int _value, int _posNeg){
		iceArmorMultiplier += _value * _posNeg;
	}

	public void ChangePierces(int _value, int _posNeg){
		pierces += _value * _posNeg;
	}

	public int GetPierces(){
		return pierces;
	}

	public Sprite GetSprite(){
		return mySprite;
	}
}
