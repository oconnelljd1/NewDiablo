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
	}
	
	// Update is called once per frame
	void Update () {
		WeaponController[] equippedWeapons = WeaponManager.instance.GetEquippedWeapons();
		for(int i = 0; i < 7; i++){
			if(equippedWeapons[i] != null){
				weapons [i].sprite = equippedWeapons[i].gameObject.GetComponent<ItemController>().GetSprite ();
			}
		}
		highlighter.anchoredPosition = weapons [WeaponManager.instance.GetNextWeapon ()].GetComponent<RectTransform> ().anchoredPosition;

		health.fillAmount = playerHealth.GetCurrentHealth () / playerHealth.GetMaxHealth ();
		mana.fillAmount = playerHealth.GetCurrentMana () / playerHealth.GetMaxMana ();
	}
}
