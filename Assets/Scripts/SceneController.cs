using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	//private int nextDoor;
	public static SceneController instance;
	[SerializeField] private string[] scenes;
	private GameObject player;

	void Awake(){
		if(instance != null){
			Object.Destroy (gameObject);
		}else{
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		player = PlayerController.instance.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene("MainMenu");
		}
	}

	public void LoadScene(int scene){
		Debug.Log ("loading");
		SceneManager.LoadScene (scenes [scene]);
	}
	/*
	public void SetNextDoor(int NextDoor){
		nextDoor = NextDoor;
	}
	*/
	public void PositionPlayer (Vector3 temp){
		player.transform.position = temp;
	}
}
