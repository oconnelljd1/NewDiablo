using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCController : MonoBehaviour {

	[SerializeField]private Row[] preQuest, quest, postQuest;
	private int stageIndex = 0, indexIndex = 0;
	private bool talkMore = true;
	[SerializeField]private Text myText, myPrompt;
	private Row[] myRow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider trigger){
		if(!trigger.isTrigger){
			myPrompt.gameObject.SetActive (true);
		}
	}

	void OnTriggerStay(Collider trigger){
		if(trigger.CompareTag("Player")){
			if(Input.GetKeyDown(KeyCode.Return)){
				myPrompt.gameObject.SetActive (false);
				if (!myText.gameObject.transform.parent.gameObject.activeInHierarchy) {
					myText.gameObject.transform.parent.gameObject.SetActive (true);
					TalkToNPC ();
				}
				if (myText.gameObject.transform.parent.gameObject.activeInHierarchy) {
					if (talkMore) {
						talkMore = false;
						StartCoroutine ("AutoType");
					} else {
						StopCoroutine ("AutoType");
						myText.text = myRow [stageIndex].GetData(indexIndex);
						FinishBlurb ();
					}
				}
			}
		}
	}

	void OnTriggerExit(){
		EndConversation ();
		myPrompt.gameObject.SetActive (false);
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
		myText.gameObject.transform.parent.gameObject.SetActive (false);
		myText.text = "";
		talkMore = true;
		indexIndex = 0;
		myPrompt.gameObject.SetActive (true);
	}

	private void FinishBlurb(){
		indexIndex++;
		if(indexIndex == myRow[stageIndex].GetDataLength()){
			EndConversation ();
		}
		talkMore = true;
		myPrompt.gameObject.SetActive (true);
	}

	private IEnumerator AutoType(){
		myText.text = "";
		string myScript = myRow [stageIndex].GetData(indexIndex);
		foreach (char letter in myScript.ToCharArray()) {
			myText.text += letter;
			yield return new WaitForSeconds (0.1f);
		}
		FinishBlurb ();
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
