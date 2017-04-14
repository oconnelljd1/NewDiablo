using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private GameObject player;
	private Vector3 oldPos;
	private Vector3 dif;

	// Use this for initialization
	void Start () {
		player = PlayerController.instance.gameObject;
		Vector3 temp = player.transform.position;
		temp.y += 15;
		transform.position = temp;
		oldPos = player.transform.position;
		dif = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 temp = player.transform.position - oldPos + dif;
		transform.position = temp;
	}
}
