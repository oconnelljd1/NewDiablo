using UnityEngine;
using System.Collections;

public class PotionController : MonoBehaviour {

	[SerializeField]private int healthRestoreAmount, manaRestoreAmount;
	private HealthController myHealth;
	private ItemController myItemC;
	private int stackSize;

	// Use this for initialization
	void Start () {
		myHealth = PlayerController.instance.gameObject.GetComponent<HealthController> ();
		myItemC = GetComponent<ItemController> ();
	}

	public void RestoreStats(){
		myHealth.RestoreMana (manaRestoreAmount);
		myHealth.RestoreHealth (healthRestoreAmount);
		myItemC.DecrementStack ();
	}
}
