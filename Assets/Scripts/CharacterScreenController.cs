using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterScreenController : MonoBehaviour {

	/*[SerializeField]*/private HealthController myHealth;
	[SerializeField]private Text valueText, multiplierText, currentText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnStart(){
		myHealth = PlayerController.instance.gameObject.GetComponent<HealthController> ();
		Debug.Log (myHealth);
		SetStatTexts ();
	}

	void OnEnable(){
		if (myHealth) {
			SetStatTexts ();
		} else {
			OnStart ();
		}
	}

	public void SetStatTexts(){
		//Debug.Log (valueText + " " + myHealth);
		valueText.text = "";
		valueText.text = myHealth.GetDamageValue() + "\n" + myHealth.GetLightningDamageValue() + "\n" + myHealth.GetFireDamageValue() + "\n" + myHealth.GetIceDamageValue() + "\n" + myHealth.GetArmorValue() + "\n" + myHealth.GetLightningArmorValue() + "\n" + myHealth.GetFireArmorValue() + "\n" + myHealth.GetIceArmorValue();
		multiplierText.text = myHealth.GetDamageMultiplier () + "\n" + myHealth.GetLightningDamageMultiplier () + "\n" + myHealth.GetFireDamageMultiplier () + "\n" + myHealth.GetIceDamageMultiplier () + "\n" + myHealth.GetArmorMultiplier () + "\n" + myHealth.GetLightningArmorMultiplier () + "\n" + myHealth.GetFireArmorMultiplier () + "\n" + myHealth.GetIceArmorMultiplier ();
		currentText.text = myHealth.GetCurrentDamage () + "\n" + myHealth.GetCurrentLightningDamage () + "\n" + myHealth.GetCurrentFireDamage () + "\n" + myHealth.GetCurrentIceDamage () + "\n" + myHealth.GetCurrentArmor () + "\n" + myHealth.GetCurrentLightningArmor () + "\n" + myHealth.GetCurrentFireArmor () + "\n" + myHealth.GetCurrentIceArmor ();
	}
}
