using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class QuestManager : MonoBehaviour {

	public static QuestManager instance;

	private bool[] openQuests, completedQuests;
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
		openQuests = new bool[questsListeners.Length];
		completedQuests = new bool[questsListeners.Length];

		openQuests [0] = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncrementQuest(string _quest){
		for(int i = 0; i < questsListeners.Length; i ++){
			if (questsListeners [i] == _quest) {
				if (openQuests [i]) {
					questStages [i]++;
					if (questStages [i] == questMaximums [i]) {
						openQuests [i] = false;
						completedQuests [i] = true;
					}
					break;
				}
			}
		}
	}

	public bool[] GetOpenQuests(){
		return openQuests;
	}

	public bool[] GetCompletedQuests(){
		return completedQuests;
	}

	public void CloseOpenQuest(int _index){
		openQuests [_index] = false;
	}

	public void OpenQuest(int _index){
		openQuests [_index] = true;
	}

}
