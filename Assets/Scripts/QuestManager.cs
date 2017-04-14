using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class QuestManager : MonoBehaviour {

	public static QuestManager instance;

	private bool[] openQuests = new bool[5], completedQuests = new bool[5];
	[SerializeField]private string[] questsListeners;
	private int[] questStages;
	[SerializeField]private int[] questMaximums;

	void Awake(){
		if (instance) {
			Object.Destroy (gameObject);
		} else {
			Object.DontDestroyOnLoad (gameObject);
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncrementQuest(string _quest){
		int quest = 0;
		for(int i = 0; i < questsListeners.Length; i ++){
			if(questsListeners[i] == _quest){
				quest = i;
			}
		}
			if (openQuests [quest]) {
			questStages [quest]++;
		}
	}


}
