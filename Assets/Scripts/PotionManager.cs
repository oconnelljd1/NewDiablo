using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PotionManager : MonoBehaviour {

	public static PotionManager instance;

	private PotionController[] myPotions = new PotionController[3];
	[SerializeField]private Image[] myImages;

	void Awake(){
		if (instance) {
			Object.Destroy (gameObject);
		} else {
			Object.DontDestroyOnLoad (gameObject);
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < myPotions.Length; i++){
			if(Input.GetButtonDown("Potion" + (i +1))){
				UsePotion (i);
			}
		}
	}

	public void RemovePotion(PotionController _potionC){
		for(int i = 0; i < myPotions.Length; i++){
			if(myPotions[i] == _potionC){
				UnequipPotion (GetIndexFromPotion(_potionC));
			}
		}
		ItemManager.instance.AddItemToInventory (_potionC.GetComponent<ItemController> ());
	}
	
	public void UsePotion(int _index){
		if (myPotions [_index]) {
			myPotions [_index].RestoreStats ();
		}

	}

	public void EquipPotion(int _index, PotionController _potionC){
		myPotions [_index] = _potionC;
		ItemManager.instance.RemoveItem (_potionC.gameObject.GetComponent<ItemController>());
		myImages [_index].sprite = myPotions [_index].GetComponent<ItemController> ().GetSprite ();
	}

	public void UnequipPotion(int _index){
		ItemManager.instance.AddItemToInventory(myPotions[_index].GetComponent<ItemController>());
		myPotions [_index] = null;
		myImages [_index].sprite = null;
	}

	public PotionController GetPotionFromIndex(int _index){
		return myPotions [_index];
	}

	public int GetIndexFromPotion(PotionController _potionC){
		for(int i = 0; i < myPotions.Length; i++){
			if(myPotions[i] == _potionC){
				return i;
			}
		}
		Debug.Log ("Can't find potion with GetIndexFromPotion");
		return 0;
	}

	public PotionController[] GetEquippedPotions(){
		return myPotions;
	}
}
