using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCController : MonoBehaviour {

	[SerializeField]private Row[] preQuest, quest, postQuest;
	private int stageIndex = 0, indexIndex = 0;
	private bool talkMore = false;
	private Text myText;
	private Row[] myRow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider trigger){
		if(trigger.CompareTag("Player")){
			if(Input.GetKeyDown(KeyCode.Space)){
				if (!myText.gameObject.activeInHierarchy) {
					myText.gameObject.SetActive (true);
					TalkToNPC ();
				}
				if (myText.gameObject.activeInHierarchy) {
					if (talkMore) {
						talkMore = false;
						StartCoroutine ("AutoType");
					} else {
						StopCoroutine ("AutoType");
						myText.text = myRow [stageIndex].GetData(indexIndex);
					}
				}
			}
		}
	}

	void OnTriggerExit(){
		EndConversation ();
	}

	public void TalkToNPC(){
		bool[] myOpenQuests = QuestManager.instance.GetOpenQuests ();
		bool[] myCompletedQuests = QuestManager.instance.GetCompletedQuests ();
		for(int i = 0; i < myOpenQuests.Length; i++){
			if (myOpenQuests [i] || myCompletedQuests [i]) {
				stageIndex = i;				
			}
			if(myOpenQuests[i] && !myCompletedQuests[i]){
				myRow = quest;
			}else if(!myOpenQuests[i] && myCompletedQuests[i]){
				myRow = preQuest;
				QuestManager.instance.OpenQuest (i);
				stageIndex++;
			}else if(myOpenQuests[i] && myCompletedQuests[i]){
				myRow = postQuest;
				QuestManager.instance.CloseOpenQuest (i);
			}
		}
	}

	private void EndConversation(){
		myText.gameObject.SetActive (false);
		myText.text = "";
		talkMore = true;
		indexIndex = 0;
	}

	private IEnumerator AutoType(){
		myText.text = "";
		string myScript = myRow [stageIndex].GetData(indexIndex);
		foreach (char letter in myScript.ToCharArray()) {
			myText.text += letter;
			yield return new WaitForSeconds (0.1f);
		}
		indexIndex++;
		talkMore = true;
		if(indexIndex == myRow[stageIndex].GetDataLength()){
			EndConversation ();
		}
	}

}

[System.Serializable]
public class Row{
	[SerializeField] string[] data;

	public string GetData(int _index){
		return data [_index];
	}

	public string[] GetDataArray(){
		return data;
	}

	public int GetDataLength(){
		return data.Length;
	}
}
