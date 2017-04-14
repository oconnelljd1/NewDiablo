using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	[SerializeField] private int thisDoor, nextDoor, nextScene;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = PlayerController.instance.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	///*
	void OnTriggerEnter(Collider trigger){
		if (trigger.CompareTag("Player")){
			PlayerController.instance.SetNextDoor (nextDoor);
			SceneController.instance.LoadScene(nextScene);
		}
	}
	//*/
	public int GetNextDoor(){
		return nextDoor;
	}

	public int GetNextScene(){
		return nextScene;
	}

	public void CheckDoor(){
		Debug.Log ("CheckDoor");
		if(PlayerController.instance.GetDoor() == thisDoor){
			Debug.Log ("Got the door");
			player.transform.position = transform.position;
			//SceneController.instance.PositionPlayer (transform.position);
		}
	}
	/*
	public void SetNextDoor(int NextDoor){
		nextDoor = NextDoor;
	}
	*/
}
