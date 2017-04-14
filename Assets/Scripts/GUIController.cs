using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {

	[SerializeField]private Image health, mana;
	[SerializeField]private RectTransform highlighter;
	[SerializeField]private Image[] weapons;
	private HealthController playerHealth;

	// Use this for initialization
	void Start () {
		playerHealth = PlayerController.instance.gameObject.GetComponent<HealthController> ();
		//health.fillAmount = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		WeaponController[] equippedWeapons = WeaponManager.instance.GetEquippedWeapons();
		for(int i = 0; i < equippedWeapons.Length; i++){
			if(equippedWeapons[i] != null){
				weapons [i].sprite = equippedWeapons[i].GetSprite ();
			}
		}
		highlighter.anchoredPosition = weapons [WeaponManager.instance.GetNextWeapon ()].GetComponent<RectTransform> ().anchoredPosition;

		float currentHealth = playerHealth.GetCurrentHealth (), maxHealth = playerHealth.GetMaxHealth ();
		health.fillAmount = currentHealth / maxHealth;
		float currentMana = playerHealth.GetCurrentMana(), maxMana = playerHealth.GetMaxMana();
		mana.fillAmount = currentMana / maxMana;
	}
}
