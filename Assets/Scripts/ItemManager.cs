using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {

	public static ItemManager instance;

	private ItemController currentItem;
	private int currentInt;

	private List<ItemController> inventory = new List<ItemController>();
	[SerializeField]private ScrollabelInventory myScrollInv;

	//[SerializeField] private GameObject image;
	//[SerializeField] private GameObject inv;

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
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (currentItem);
		/* KEEP THIS, NEEDS TO BE MOVED TO PLAYER CONTROLLER
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity)){
				if (hit.collider.CompareTag ("Item")) {
					if(!inv.activeInHierarchy){
						ItemController newItem = hit.collider.GetComponent<ItemController> ();
						inventory.Add (newItem);
						newItem.Set3D (false);
					}
				}
			}
		}
		*/
	}

	public void InteractWithInventory(int _index){

		/*if (currentItem) {
			ItemController tempItem = inventory [_index];
			inventory [_index] = currentItem;
			inventory [currentInt] = tempItem;
			currentItem = null;
		} else {
		*/
			Debug.Log ("Not Holding Anything!");
			if (_index<inventory.Count) {
				Debug.Log ("FoundSomething");
				currentItem = inventory [_index];
				currentInt = _index;
			}
		//}
		myScrollInv.UpdateInventory ();
	}

	public void InteractWithEquipment(int _index){
		Debug.Log ("Interacting with equipment");
		if (currentItem && currentItem.gameObject.GetComponent<EquipmentController>()) {
			Debug.Log ("EquippingItem");
			EquipmentManager.instance.Equip (_index, currentItem.gameObject.GetComponent<EquipmentController> ());
		} else {
			EquipmentManager.instance.Equip (_index, null);
		}
		currentItem = null;
		myScrollInv.UpdateInventory ();
	}

	public void InteractWithPotions(int _index){
		if (!currentItem) {
			PotionManager.instance.UnequipPotion (_index);
		}
		if(currentItem && PotionManager.instance.GetPotionFromIndex(_index)){
			PotionManager.instance.UnequipPotion (_index);
		}
		if(currentItem && currentItem.GetComponent<PotionController>()){
			PotionManager.instance.EquipPotion (_index, currentItem.GetComponent<PotionController>());
		}
		currentItem = null;
		/*
		if (currentItem && currentItem.gameObject.GetComponent<PotionController>()){
			PotionManager.instance.EquipPotion (_index, currentItem.gameObject.GetComponent<PotionController>());
		}else if(PotionManager.instance.GetPotionFromIndex(_index) && !currentItem){
			PotionManager.instance.UnequipPotion (_index);
		}
		currentItem = null;
		*/
	}

	public int GetInventoryCount(){
		return inventory.Count;
	}

	public void AddItemToInventory(ItemController _itemC){
		bool good = false;
		if (_itemC.GetStackable ()) {
			for(int i = 0; i < inventory.Count; i++){
				if(inventory[i].GetItemType() == _itemC.GetItemType()){
					good = true;
					inventory [i].IncrementStack ();
					Object.Destroy (_itemC.gameObject);
				}
			}
		}
		if (!good) {
			inventory.Add (_itemC);
			_itemC.gameObject.transform.SetParent (transform.GetChild (0).transform);
			_itemC.gameObject.transform.localPosition = Vector3.zero;
			_itemC.gameObject.transform.localRotation = Quaternion.Euler (Vector3.zero);
			_itemC.gameObject.transform.GetChild (0).gameObject.SetActive (false);
		}
		QuestManager.instance.IncrementQuest (_itemC.GetQuestName ());
		myScrollInv.UpdateInventory ();
	}

	public void RemoveItem(ItemController _itemC){
		inventory.Remove (_itemC);
		myScrollInv.UpdateInventory ();
	}

	public List<ItemController> GetInventory	(){
		return inventory;
	}

}
