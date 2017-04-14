using UnityEngine;
using System.Collections;

public class TownManager : MonoBehaviour {

	[SerializeField]private Mesh[] treeMeshs;
	[SerializeField]private MyPathfinding myPathfinder;

	// Use this for initialization
	void Start () {
		GameObject[] trees = GameObject.FindGameObjectsWithTag ("Tree");
		foreach(GameObject tree in trees){
			Mesh myMesh = treeMeshs[Random.Range (0, treeMeshs.Length)];
			tree.GetComponent<MeshFilter> ().mesh = myMesh;
			tree.GetComponent<MeshCollider>().sharedMesh = myMesh;
		}

		myPathfinder.OnStart (new Vector2(50, 50));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
