using UnityEngine;
using System.Collections;

public class WindowManager : MonoBehaviour {

	public static WindowManager instance;

	[SerializeField]private GameObject eventSystem;
	[SerializeField]private GameObject inventory, character, map, quests, skillTree;
	private ScrollabelInventory myInv;
	private CharacterScreenController myChar;

	// Use this for initialization
	void Start () {
		if (instance) {
			Object.Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
			DontDestroyOnLoad (eventSystem.gameObject);
		}

		myInv = inventory.GetComponent<ScrollabelInventory> ();
		myChar = character.GetComponent<CharacterScreenController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.I)){
			inventory.SetActive (!inventory.activeInHierarchy);
			if(inventory.activeInHierarchy){
				myInv.CreateExtraInventorySlotsInWindow ();
			}
		}else if(Input.GetKeyDown(KeyCode.C)){
			character.SetActive (!character.activeInHierarchy);
			if(character.activeInHierarchy){
				//myChar.SetStatTexts ();
			}
		}else if(Input.GetKeyDown(KeyCode.M)){
			map.SetActive (!map.activeInHierarchy);
		}else if(Input.GetKeyDown(KeyCode.B)){
			quests.SetActive (!quests.activeInHierarchy);
		}else if(Input.GetKeyDown(KeyCode.Z)){
			skillTree.SetActive (!skillTree.activeInHierarchy);
		}
	}

	public void CloseInventory(){
		myInv.DestroyExtraInventorySlotsInWindow ();
		inventory.SetActive (false);
	}

	public void CloseCharacter(){
		character.SetActive (false);
	}

	public void CloseMap(){
		map.SetActive (false);
	}

	public void CloseQuests(){
		quests.SetActive (false);
	}

	public void CloseSkillTree(){
		skillTree.SetActive (false);
	}

	public GameObject GetEventSystem(){
		return eventSystem;
	}

}