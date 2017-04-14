using UnityEngine;
using System.Collections;

public class EggEnemyAI : MonoBehaviour {

	private BroodEnemyAI parent;
	[SerializeField] private GameObject broodling;

	// Use this for initialization
	void Start () {
		StartCoroutine (Hatch());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetParent(BroodEnemyAI _parent){
		parent = _parent;
	}

	private IEnumerator Hatch(){
		yield return new WaitForSeconds (5);
		///*
		BroodlingEnemyAI newBroodling = Instantiate (broodling, transform.position, transform.rotation) as BroodlingEnemyAI;
		newBroodling.SetParent (parent);
		//*/
		/*
		GameObject newBroodling = Object.Instantiate (broodling, transform.position, transform.rotation) as GameObject;
		newBroodling.GetComponent<BroodlingEnemyAI>().SetParent (parent);
		*/
		Object.Destroy (gameObject);
	}

	private void Death(){
		parent.ChildDeath ();
		StopCoroutine (Hatch());
		Object.Destroy (gameObject);
	}

	void OnTriggerEnter(Collider trigger){
		
	}

}
