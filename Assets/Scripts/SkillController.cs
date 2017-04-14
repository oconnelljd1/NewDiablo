using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkillController : MonoBehaviour {

	private Button myButton;
	[SerializeField]Button[] nextSkills;
	[SerializeField]private string[] statsToUp;
	[SerializeField]private GameObject skill;
	[SerializeField]private WeaponController skillToUp;
	[SerializeField]private int[] upToStats;
	private HealthController myHealth;

	private bool isBought;

	private string[] stats = new string[20] {"health", "healthMultiplier", "mana", "manaMultiplier", "damage", "damageMultiplier", "lightningDamage", "lightningDamageMultiplier", "fireDamage", "fireDamageMultiplier", "iceDamage", "iceDamageMultiplier", "armor", "armorMultiplier", "lightningArmor", "lightningArmorMultiplier", "fireArmor", "fireArmorMultiplier", "iceArmor", "iceArmorMultiplier"};

	// Use this for initialization
	void Start () {
		myHealth = WeaponManager.instance.gameObject.GetComponent<HealthController> ();
		myButton = GetComponent<Button> ();
	}

	public void DoTheThing(){
		if (ExperienceManager.instance.GetSkillPoints () > 0) {
			ExperienceManager.instance.SpendSkillPoint();
			isBought = true;
			if(skill){
				skill.SetActive(true);
			}else if(skillToUp){
				List<int> myStats= new List<int>();
				for(int i = 0; i < stats.Length; i ++){
					for(int o = i; o < statsToUp.Length; o++){
						if(stats[i] == statsToUp[o]){
							myStats.Add(i);
						}	
					}
				}//if c = certain number then crash game
				for(int i = 0; i < myStats.Count; i++){
					switch(myStats[i]){
					case 0:
						skillToUp.ChangeHealthValue(upToStats[myStats[i]], 1);
						break;
					case 1:
						skillToUp.ChangeHealthMultiplier(upToStats[myStats[i]], 1);
						break;
					case 2:
						skillToUp.ChangeManaValue(upToStats[myStats[i]], 1);
						break;
					case 3:
						skillToUp.ChangeManaMultiplier(upToStats[myStats[i]], 1);
						break;
					case 4:
						skillToUp.ChangeDamageValue(upToStats[myStats[i]], 1);
						break;
					case 5:
						skillToUp.ChangeDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					case 6:
						skillToUp.ChangeLightningDamageValue(upToStats[myStats[i]], 1);
						break;
					case 7:
						skillToUp.ChangeLightningDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					case 8:
						skillToUp.ChangeFireDamageValue(upToStats[myStats[i]], 1);
						break;
					case 9:
						skillToUp.ChangeFireDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					case 10:
						skillToUp.ChangeIceDamageValue(upToStats[myStats[i]], 1);
						break;
					case 11:
						skillToUp.ChangeIceDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					case 12:
						skillToUp.ChangeArmorValue(upToStats[myStats[i]], 1);
						break;
					case 13:
						skillToUp.ChangeArmorMultiplier(upToStats[myStats[i]], 1);
						break;
					case 14:
						skillToUp.ChangeLightningArmorValue(upToStats[myStats[i]], 1);
						break;
					case 15:
						skillToUp.ChangeLightningDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					case 16:
						skillToUp.ChangeFireArmorValue(upToStats[myStats[i]], 1);
						break;
					case 17:
						skillToUp.ChangeFireDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					case 18:
						skillToUp.ChangeIceArmorValue(upToStats[myStats[i]], 1);
						break;
					case 19:
						skillToUp.ChangeIceDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					default:
						Debug.Log("You spelt something wrong in: " + gameObject.name);
						break;
					}
				}
			}else{
				List<int> myStats= new List<int>();
				for(int i = 0; i < stats.Length; i ++){
					for(int o = i; o < statsToUp.Length; o++){
						if(stats[i] == statsToUp[o]){
							myStats.Add(i);
						}	
					}
				}//if c = certain number then crash game
				for(int i = 0; i < myStats.Count; i++){
					switch(myStats[i]){
					case 0:
						myHealth.ChangeHealthValue(upToStats[myStats[i]], 1);
						break;
					case 1:
						myHealth.ChangeHealthMultiplier(upToStats[myStats[i]], 1);
						break;
					case 2:
						myHealth.ChangeManaValue(upToStats[myStats[i]], 1);
						break;
					case 3:
						myHealth.ChangeManaMultiplier(upToStats[myStats[i]], 1);
						break;
					case 4:
						myHealth.ChangeDamageValue(upToStats[myStats[i]], 1);
						break;
					case 5:
						myHealth.ChangeDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					case 6:
						myHealth.ChangeLightningDamageValue(upToStats[myStats[i]], 1);
						break;
					case 7:
						myHealth.ChangeLightningDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					case 8:
						myHealth.ChangeFireDamageValue(upToStats[myStats[i]], 1);
						break;
					case 9:
						myHealth.ChangeFireDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					case 10:
						myHealth.ChangeIceDamageValue(upToStats[myStats[i]], 1);
						break;
					case 11:
						myHealth.ChangeIceDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					case 12:
						myHealth.ChangeArmorValue(upToStats[myStats[i]], 1);
						break;
					case 13:
							myHealth.ChangeArmorMultiplier(upToStats[myStats[i]], 1);
						break;
					case 14:
						myHealth.ChangeLightningArmorValue(upToStats[myStats[i]], 1);
						break;
					case 15:
						myHealth.ChangeLightningDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					case 16:
						myHealth.ChangeFireArmorValue(upToStats[myStats[i]], 1);
						break;
					case 17:
						myHealth.ChangeFireDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					case 18:
						myHealth.ChangeIceArmorValue(upToStats[myStats[i]], 1);
						break;
					case 19:
						myHealth.ChangeIceDamageMultiplier(upToStats[myStats[i]], 1);
						break;
					default:
						Debug.Log("You spelt something wrong in: " + gameObject.name);
						break;
					}
				}
			}
			//myButton.Sprite = ;
			myButton.interactable = false;
			foreach (Button button in nextSkills) {
				SkillController buttonSkill = button.gameObject.GetComponent<SkillController> ();
				if (!buttonSkill.GetIsBought()) {
					button.interactable = true;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool GetIsBought(){
		return isBought;
	}
}
