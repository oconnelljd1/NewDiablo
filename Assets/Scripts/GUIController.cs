using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {

	[SerializeField]private Image health, mana, enemyHealth;
	[SerializeField]private RectTransform highlighter;
	[SerializeField]private Image[] weapons, potions;
	[SerializeField]private Text[] potionTexts;
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
			if (equippedWeapons [i] != null) {
				//Debug.Log (weapons [i]);
				weapons [i].sprite = equippedWeapons [i].GetSprite ();
			} else {
				weapons [i].sprite = null;
			}
		}
		PotionController[] equippedPotions = PotionManager.instance.GetEquippedPotions ();
		for(int i = 0; i < potions.Length; i++){
			if (equippedPotions [i] != null) {
				potions [i].sprite = equippedPotions [i].GetComponent<ItemController> ().GetSprite ();
			} else {
				potions [i].sprite = null;
			}
		}

		for(int i = 0; i < potionTexts.Length; i++){
			potionTexts[i].text = "x";
			if(equippedPotions[i] != null){
				potionTexts[i].text += "" + equippedPotions [i].GetComponent<ItemController> ().GetStackSize ();
			}
		}
		highlighter.anchoredPosition = weapons [WeaponManager.instance.GetNextWeapon ()].GetComponent<RectTransform> ().anchoredPosition;

		float currentHealth = playerHealth.GetCurrentHealth (), maxHealth = playerHealth.GetMaxHealth ();
		health.fillAmount = currentHealth / maxHealth;
		float currentMana = playerHealth.GetCurrentMana(), maxMana = playerHealth.GetMaxMana();
		mana.fillAmount = currentMana / maxMana;
		GameObject currentTarget = PlayerController.instance.GetCurrentTarget ();
		if (currentTarget && currentTarget.CompareTag ("Enemy")) {
			enemyHealth.gameObject.SetActive (true);
			float currentEnemyHealth = currentTarget.GetComponent<HealthController> ().GetCurrentHealth ();
			float maxEnemyHealth = currentTarget.GetComponent<HealthController> ().GetMaxHealth ();
			enemyHealth.fillAmount = currentEnemyHealth / maxEnemyHealth;
		} else {
			enemyHealth.gameObject.SetActive (false);
		}
	}
}
