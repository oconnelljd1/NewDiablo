using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScrollabelInventory : MonoBehaviour {

	[SerializeField]private GameObject slotPrefab, content;
	//private GameObject itemSlot;
	[SerializeField]private ToggleGroup myToggle;
	[SerializeField]private int preLength, width;
	[SerializeField]private bool isItem;

	//private int xPos, yPos, xDif, yDif, xStart, yStart;
	private List<GameObject> slots = new List<GameObject> ();

	[SerializeField]private Scrollbar myScrollbar;

	//follow mouse
	//[SerializeField]private RectTransform myImage;

	//[SerializeField]private CanvasScaler myCanvasScaler;
	//private RectTransform myRect;
	//private Vector3 myRatio;
	//private Vector3 myDif;

	void Awake(){
		/*
		xDif = Mathf.RoundToInt(slotPrefab.GetComponent<RectTransform> ().rect.width);
		yDif = Mathf.RoundToInt(slotPrefab.GetComponent<RectTransform> ().rect.height);
		xStart = Mathf.RoundToInt(slotPrefab.GetComponent<RectTransform> ().rect.position.x);
		yStart = Mathf.RoundToInt(slotPrefab.GetComponent<RectTransform> ().rect.position.y);
		*/
		//follow mouse
		//myRect = GetComponent<RectTransform>();
		//myRatio = new Vector3(myCanvasScaler.referenceResolution.x / Screen.width, myCanvasScaler.referenceResolution.y / Screen.height);
		//myRatio = new Vector3( Screen.width / myCanvasScaler.referenceResolution.x, Screen.height / myCanvasScaler.referenceResolution.y);
		//myDif = new Vector3(Screen.width, Screen.height, 0) / 2;
	}

	// Use this for initialization
	void Start () {
		CreateExtraInventorySlotsInWindow ();
		//UpdateInventory ();

	}

	void OnEnable(){
		UpdateInventory ();
		myScrollbar.value = 1;
	}
	
	// Update is called once per frame
	void Update () {
		//follow mouse
		//myImage.anchoredPosition =  Vector3.Scale(Input.mousePosition - myDif, myRatio) - myRect.localPosition;

		//scrollbar
		myScrollbar.value += Input.GetAxis("Mouse ScrollWheel");
	}

	public void CreateExtraInventorySlotsInWindow(){
		int invLength = ItemManager.instance.GetInventoryCount () - preLength;
		if (invLength < 0) {
			invLength = 0;
		}
		invLength = Mathf.CeilToInt(invLength/width);
		int slotsLength = Mathf.CeilToInt(slots.Count/width);
		if(invLength > slotsLength){
			for(int i = 0; i < (invLength - slotsLength) * width; i ++){
				GameObject item = Instantiate(slotPrefab) as GameObject;
				item.GetComponent<Toggle>().group = myToggle;
				item.transform.SetParent (content.transform, false);
				//item.GetComponent<RectTransform> ().localPosition = new Vector3(xPos, yPos, -1);
				item.GetComponent<ItemSlotController>().SetStuff(i + (slotsLength * width), isItem);
				slots.Add (item);
			}
		}
	}

	public void DestroyExtraInventorySlotsInWindow(){
		int invLength = ItemManager.instance.GetInventoryCount () - preLength;
		if(invLength < 0){
			invLength = 0;
		}
		invLength = Mathf.CeilToInt (invLength / width);
		int slotsLength = Mathf.CeilToInt((slots.Count - 1) / width);
		if(invLength < slotsLength){
			for(int i = invLength * width; i < slots.Count; i ++){
				Object.Destroy (slots[i]);
			}
			slots.RemoveRange (invLength * width, slots.Count -1);
		}
	}

	public void UpdateInventory(){
		if (isItem) {
			List<ItemController> myInventory = ItemManager.instance.GetInventory ();
			for (int i = 0; i < slots.Count + preLength; i++) {
				if (i < myInventory.Count) {
					content.transform.GetChild (i).gameObject.GetComponent<Image> ().sprite = myInventory [i].GetSprite ();
				} else {
					content.transform.GetChild (i).gameObject.GetComponent<Image> ().sprite = null;
				}
			}
		} else {
			List<WeaponController> myWeapons = WeaponManager.instance.GetAllWeapons ();
			for(int i = 0; i < slots.Count + preLength; i++){
				if (i < myWeapons.Count) {
					content.transform.GetChild (i).gameObject.GetComponent<Image> ().sprite = myWeapons [i].GetSprite ();
				} else {
					content.transform.GetChild (i).gameObject.GetComponent<Image> ().sprite = null;
				}
			}
		}
	}

}
