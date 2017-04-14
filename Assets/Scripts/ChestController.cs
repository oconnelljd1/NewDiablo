using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour {

	private GameObject[] loot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Open(){
		Debug.Log ("Open chest" + loot.Length);
		if(loot.Length> 0){
			for(int i = 0; i< loot.Length; i++){
				loot [i].SetActive (true);
				float myRandom = Random.Range (0, 360) * Mathf.Deg2Rad;
				loot[i].transform.position = transform.position + new Vector3 (Mathf.Sin(myRandom), 0, Mathf.Cos(myRandom));
			}
		}
	}

	public void SetLoot(GameObject[] _loot){
		loot = _loot;
	}
}
