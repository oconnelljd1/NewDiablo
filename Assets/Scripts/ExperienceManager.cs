using UnityEngine;
using System.Collections;

public class ExperienceManager : MonoBehaviour {

	public static ExperienceManager instance;

	private int experience, toNextLevel, skillPoints = 1;

	void Awake(){
		if(instance){
			Object.Destroy (gameObject);
		}else{
			instance = this;
			Object.DontDestroyOnLoad (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetExperience(int _experience){
		experience += _experience;
		if(experience > toNextLevel){
			experience -= toNextLevel;
			toNextLevel += 5;
			skillPoints++;
		}
	}

	public int GetSkillPoints(){
		return skillPoints;
	}

	public void SpendSkillPoint(){
		skillPoints--;
	}
}
