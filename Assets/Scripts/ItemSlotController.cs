using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemSlotController : MonoBehaviour {

	//private Toggle myToggle;
	private EventTrigger myTrigger;

	[SerializeField]private int myIndex;
	[SerializeField]private bool isItem;

	void Awake(){
		
	}

	// Use this for initialization
	void Start () {
		/*
		myToggle = GetComponent<Toggle> ();
		myToggle.onValueChanged.AddListener(delegate{
			if(!myToggle.isOn){
				//Debug.Log("changing Value" + myIndex + myToggle.isOn);
				if(isItem){
					Debug.Log("Interacting");
					ItemManager.instance.InteractWithInventory (myIndex);
				}else{
					WeaponManager.instance.HoldWeapon(myIndex);
				}
			}
		});
		*/
		//myTrigger.OnPointerDown.AddListener (delegate {
				
		//});
	}

	public void SetStuff(int _index, bool _TF){
		myIndex = _index;
		isItem = _TF;
	}

	public void DoStuff(){
		if(isItem){
			Debug.Log("Interacting");
			ItemManager.instance.InteractWithInventory (myIndex);
		}else{
			WeaponManager.instance.HoldWeapon(myIndex);
		}
	}
}
