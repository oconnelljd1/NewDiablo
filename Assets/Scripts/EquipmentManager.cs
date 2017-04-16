using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentManager : MonoBehaviour {

	public static EquipmentManager instance;

	private EquipmentController tempEquip;
	[SerializeField]EquipmentController[] equipment = new EquipmentController[10];
	private string[] equipmentTypes = new string[10] {"chest","arms","legs","head","ring","ring","necklace", "belt","weapon","weapon"};
	[SerializeField]private Image[] equipmentImages;

	private HealthController myHealth;

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
		myHealth = GetComponent<HealthController> ();
	}

	public void Equip(int _index, EquipmentController _equipC){
		if (_equipC) {
			if(_equipC.GetEquipmentType() ==  "weapon"){
				if(equipment[8]){
					if(_equipC.GetTwohanded () && equipment [9]){
						return;
					}
					if(_index == 9 && (equipment [8].GetTwohanded () ||_equipC.GetTwohanded())){
						_index = 8;
					}
				}
			}
		}
		if(equipment [_index]){
			myHealth.ChangeStats (equipment[_index], -1);
			ItemManager.instance.AddItemToInventory (equipment[_index].gameObject.GetComponent<ItemController>());
			equipment [_index] = null;
			if(_index == 8){
				WeaponManager.instance.UnequipPrimary ();
			}
		}
		if(equipment[9] && !equipment[8]){
			equipment [8] = equipment [9];
			equipment [9] = null;
			_index = 8;
		}
		if (_equipC) {
			Debug.Log ("Have equipment" + _equipC);
			if(_equipC.GetEquipmentType() == equipmentTypes[_index]){
				myHealth.ChangeStats (_equipC, 1);
				ItemManager.instance.RemoveItem (_equipC.gameObject.GetComponent<ItemController>());
				equipment [_index] = _equipC;
			}
		}
	
		for(int i = 0; i < equipment.Length; i++){
			if (equipment [i]) {
				equipmentImages [i].sprite = equipment [i].GetSprite ();
			} else {
				equipmentImages [i].sprite = null;
			}
		}
		if(equipment[8]){
			WeaponManager.instance.EquipPrimary (equipment[8].GetComponent<WeaponController>());
		}

	}

	public EquipmentController GetEquipment(int _index){
		return equipment [_index];
	}

}
