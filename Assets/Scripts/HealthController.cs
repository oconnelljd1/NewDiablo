using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	[SerializeField]private int healthValue = 100, healthMultiplier = 100;
	private float currentHealth, currentMana;
	[SerializeField]private int manaValue = 100, manaMultiplier = 100;
	[SerializeField]private int healthRegenValue, healthRegenMultiplier = 100;
	[SerializeField]private int manaRegenValue = 1, manaRegenMultiplier = 100;

	[SerializeField] private int damageValue, damageMultiplier = 100;
	[SerializeField] private int lightningDamageValue, lightningDamageMultiplier = 100;
	[SerializeField] private int fireDamageValue, fireDamageMultiplier = 100;
	[SerializeField] private int iceDamageValue, iceDamageMultiplier = 100;

	[SerializeField] private int armorValue, armorMultiplier = 100;
	[SerializeField] private int lightningArmorValue, lightningArmorMultiplier = 100;
	[SerializeField] private int fireArmorValue, fireArmorMultiplier = 100;
	[SerializeField] private int iceArmorValue, iceArmorMultiplier = 100;

	[SerializeField]private int givenExperience;
	[SerializeField]private string questDeath;

	// Use this for initialization
	void Start () {
		currentHealth = healthValue * healthMultiplier / 100;
		currentMana = manaValue * manaMultiplier / 100;
	}
	
	// Update is called once per frame
	void Update () {
		float dir = currentHealth - (healthValue * healthMultiplier / 100);
		if (dir > 0) {
			currentHealth -= (healthRegenValue * healthRegenMultiplier / 100) * Time.deltaTime;
		} else {
			currentHealth += (healthRegenValue * healthRegenMultiplier / 100) * Time.deltaTime;
		}

		dir = currentMana - (manaValue * manaMultiplier / 100);
		if (dir > 0) {
			currentMana -= (manaRegenValue * manaRegenMultiplier / 100) * Time.deltaTime;
		} else {
			currentMana += (manaRegenValue * manaRegenMultiplier / 100) * Time.deltaTime;
		}
	}

	public void Damage (int _damage) {
		currentHealth -= _damage;
		Debug.Log (gameObject.tag + " hit! " + currentHealth + " health left! " + _damage + " damage done!");
		if(currentHealth < 1){
			ExperienceManager.instance.GetExperience (givenExperience);
			QuestManager.instance.IncrementQuest (questDeath);
			Object.Destroy (gameObject);
		}
	}

	public void ChangeStats(EquipmentController _equipment, int _posNeg){
		Debug.Log (_equipment.gameObject);
		healthValue += _equipment.GetHealthValue () * _posNeg;
		healthMultiplier += _equipment.GetHealthMultiplier () * _posNeg;
		manaValue += _equipment.GetManaValue () * _posNeg;
		manaMultiplier += _equipment.GetManaMultiplier () * _posNeg;

		damageValue += _equipment.GetDamageValue() * _posNeg;
		lightningDamageValue += _equipment.GetLightningDamageValue () * _posNeg;
		fireDamageValue += _equipment.GetFireDamageValue () * _posNeg;
		iceDamageValue += _equipment.GetIceDamageValue () * _posNeg;

		armorValue += _equipment.GetArmorValue () * _posNeg;
		lightningArmorValue += _equipment.GetLightningArmorValue () * _posNeg;
		fireArmorValue += _equipment.GetFireArmorValue () * _posNeg;
		iceArmorValue += _equipment.GetIceArmorValue () * _posNeg;

		damageMultiplier += _equipment.GetDamageMultiplier () * _posNeg;
		lightningDamageMultiplier += _equipment.GetLightningDamageMultiplier () * _posNeg;
		fireDamageMultiplier += _equipment.GetLightningDamageMultiplier () * _posNeg;
		iceDamageMultiplier += _equipment.GetIceDamageMultiplier () * _posNeg;

		armorMultiplier += _equipment.GetArmorMultiplier () * _posNeg;
		lightningArmorMultiplier += _equipment.GetLightningArmorMultiplier () * _posNeg;
		fireArmorMultiplier += _equipment.GetFireArmorMultiplier () * _posNeg;
		iceArmorMultiplier += _equipment.GetIceArmorMultiplier () * _posNeg;
	}

	public void BoostStats(WeaponController _weapon, int _posNeg){
		healthValue += _weapon.GetHealthValue () * _posNeg;
		healthMultiplier += _weapon.GetHealthMultiplier () * _posNeg;
		manaValue += _weapon.GetManaValue () * _posNeg;
		manaMultiplier += _weapon.GetManaMultiplier () * _posNeg;

		damageValue += _weapon.GetDamageValue() * _posNeg;
		lightningDamageValue += _weapon.GetLightningDamageValue () * _posNeg;
		fireDamageValue += _weapon.GetFireDamageValue () * _posNeg;
		iceDamageValue += _weapon.GetIceDamageValue () * _posNeg;

		armorValue += _weapon.GetArmorValue () * _posNeg;
		lightningArmorValue += _weapon.GetLightningArmorValue () * _posNeg;
		fireArmorValue += _weapon.GetFireArmorValue () * _posNeg;
		iceArmorValue += _weapon.GetIceArmorValue () * _posNeg;

		damageMultiplier += _weapon.GetDamageMultiplier () * _posNeg;
		lightningDamageMultiplier += _weapon.GetLightningDamageMultiplier () * _posNeg;
		fireDamageMultiplier += _weapon.GetLightningDamageMultiplier () * _posNeg;
		iceDamageMultiplier += _weapon.GetIceDamageMultiplier () * _posNeg;

		armorMultiplier += _weapon.GetArmorMultiplier () * _posNeg;
		lightningArmorMultiplier += _weapon.GetLightningArmorMultiplier () * _posNeg;
		fireArmorMultiplier += _weapon.GetFireArmorMultiplier () * _posNeg;
		iceArmorMultiplier += _weapon.GetIceArmorMultiplier () * _posNeg;
	}

	public int GetCurrentMana(){
		return Mathf.RoundToInt(currentMana);
	}

	public int GetMaxMana(){
		return manaValue * manaMultiplier / 100;
	}

	public int GetCurrentHealth(){
		return Mathf.RoundToInt(currentHealth);
	}

	public int GetMaxHealth(){
		return healthValue * healthMultiplier / 100;
	}

	public int GetDamageValue(){
		return damageValue;
	}

	public int GetLightningDamageValue(){
		return lightningDamageValue;
	}

	public int GetFireDamageValue(){
		return fireDamageValue;
	}

	public int GetIceDamageValue(){
		return iceDamageValue;
	}

	public int GetArmorValue(){
		return armorValue;
	}

	public int GetLightningArmorValue(){
		return lightningArmorValue;
	}

	public int GetFireArmorValue(){
		return fireArmorValue;
	}

	public int GetIceArmorValue(){
		return iceArmorValue;
	}

	public int GetDamageMultiplier(){
		return damageMultiplier;
	}

	public int GetLightningDamageMultiplier(){
		return lightningDamageMultiplier;
	}

	public int GetFireDamageMultiplier(){
		return fireDamageMultiplier;
	}

	public int GetIceDamageMultiplier(){
		return iceDamageMultiplier;
	}

	public int GetArmorMultiplier(){
		return armorMultiplier;
	}

	public int GetLightningArmorMultiplier(){
		return lightningArmorMultiplier;
	}

	public int GetFireArmorMultiplier(){
		return fireArmorMultiplier;
	}

	public int GetIceArmorMultiplier(){
		return iceArmorMultiplier;
	}

	public int GetCurrentDamage(){
		return damageValue * damageMultiplier / 100;
	}

	public int GetCurrentLightningDamage(){
		return lightningDamageValue * lightningDamageMultiplier / 100;
	}

	public int GetCurrentFireDamage(){
		return fireDamageValue * fireDamageMultiplier / 100;
	}
	public int GetCurrentIceDamage(){
		return iceDamageValue * iceDamageMultiplier / 100;
	}

	public int GetCurrentArmor(){
		return armorValue * armorMultiplier / 100;
	}

	public int GetCurrentLightningArmor(){
		return lightningArmorValue * lightningArmorMultiplier / 100;
	}

	public int GetCurrentFireArmor(){
		return fireArmorValue * fireArmorMultiplier / 100;
	}

	public int GetCurrentIceArmor(){
		return iceArmorValue * iceArmorMultiplier / 100;
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

	public void RestoreHealth(int _restore){
		currentHealth += _restore;
	}

	public void RestoreMana(int _restore){
		currentMana += _restore;
	}

}
