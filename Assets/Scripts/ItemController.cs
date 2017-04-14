using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

	[SerializeField]private Sprite sprite;
	[SerializeField]private bool stackable = false;
	[SerializeField]private string itemType, questName;

	private bool isWeapon, isEquipmenet, isStackable;
	private int stackSize;

	// Use this for initialization
	void Start () {
		if(GetComponent<EquipmentController>()){
			isEquipmenet = true;
			if(GetComponent<WeaponController>()){
				isWeapon = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Set3D(bool _bool){
		gameObject.SetActive (_bool);
	}

	public Sprite GetSprite(){
		return sprite;
	}

	public string GetQuestName(){
		return questName;
	}

	public void IncrementStack(){
		stackSize++;
	}

	public void DecrementStack(){
		stackSize--;
		if (stackSize == 0){
			if (GetComponent<PotionController> ()) {
				PotionManager.instance.RemovePotion (GetComponent<PotionController>());
			}
			ItemManager.instance.RemoveItem (this);
			Object.Destroy (gameObject);
		}

	}

	public int GetStackSize(){
		return stackSize;
	}

	public bool GetStackable(){
		return stackable;
	}

	public string GetItemType(){
		return itemType;
	}

}
