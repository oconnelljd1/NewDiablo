using UnityEngine;
using System.Collections;

public class EquipmentController : MonoBehaviour {

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

	[SerializeField]private string equipmentType;

	[SerializeField]private bool twoHanded;

	private GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string GetEquipmentType(){
		return equipmentType;
	}

	public bool GetTwohanded(){
		return twoHanded;
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



}
