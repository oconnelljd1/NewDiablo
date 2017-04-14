using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public static PlayerController instance;

	private MyPathfinding pathfinder;
	private List<Vector3> path = new List<Vector3>();

	private GameObject targetObject;
	private Vector3 targetPos;
	private Node targetNode;
	private bool here = true;
	private int currentTarget;

	[SerializeField] private float turnSpeed, moveSpeed = 2;
	[SerializeField]private LayerMask myLayerMask;
	
	private int nextDoor = 0;
	private float stepDistance;

	private HealthController myHealthController;

	void Awake(){
		if(instance != null){
			Object.Destroy (gameObject);
		}else{
			instance = this;
			Object.DontDestroyOnLoad (gameObject);
		}
		stepDistance = moveSpeed * moveSpeed * Time.deltaTime * Time.deltaTime;
	}

	// Use this for initialization
	void Start () {
		//navMesh = GetComponent<NavMeshAgent> (); 
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			//Debug.Log ("MouseClicked");
			if(!EventSystem.current.IsPointerOverGameObject()){
				//Debug.Log ("NotOverUI");
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Debug.DrawRay (ray.origin, ray.direction * Mathf.Infinity, Color.red);
				if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, myLayerMask, QueryTriggerInteraction.Ignore)){
					//Debug.Log ("HitSomething");
					path = new List<Vector3> ();
					here = false;
					targetObject = null;
					targetPos = Vector3.zero;
					//Debug.Log ("hit " + hit.collider.gameObject.tag);
					if (hit.collider.gameObject.tag == "Ground") {
						targetPos = hit.point;
					} else if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Item") || hit.collider.CompareTag("Chest")||hit.collider.CompareTag("NPC")) {
						targetObject = hit.collider.gameObject;
						targetPos = targetObject.transform.position;
						targetNode = pathfinder.NodeFormWolrdPoint (targetPos);
					}else if(hit.collider.CompareTag("Player")){
						here = true;
						return;
					}
					if(pathfinder.NodeFormWolrdPoint(transform.position) != pathfinder.NodeFormWolrdPoint(targetPos)){
						path = pathfinder.FindPath(transform.position, targetPos);
					}
					path.Add (targetPos);
					currentTarget = 0;
				}
				if(targetObject &&targetObject.tag == "Enemy"){
					targetPos = targetObject.transform.position;
					path [path.Count - 1] = targetPos;
					if(targetNode != pathfinder.NodeFormWolrdPoint (targetPos)){
						path = pathfinder.FindPath(transform.position, targetPos);
					}
				}
			}
		}

		if(!here){
			MoveTowards ();
		}
	}

	void OnTriggerEnter(Collider trigger){
		if (trigger.CompareTag ("Door")) {
			here = true;
			DoorController myDoorController = trigger.gameObject.GetComponent<DoorController> ();
			nextDoor = myDoorController.GetNextDoor();
			SceneController.instance.LoadScene(myDoorController.GetNextScene ());
		}
	}


	void MoveTowards(){
		Vector3 Displacement = Vector3.zero;
		if (targetObject) {
			Displacement = targetPos - transform.position;
			Displacement.y = 0;
			if (targetObject.CompareTag("Enemy")) {
				if (Displacement.sqrMagnitude < 1 + WeaponManager.instance.GetNextWeaponSqrRange ()) {
					WeaponManager.instance.TryAttack ();
					return;
				}
			} else if (targetObject.CompareTag("Item")) {
				if (Displacement.sqrMagnitude < 0.7f) {
					ItemManager.instance.AddItemToInventory (targetObject.GetComponentInParent<ItemController> ());
					here = true;
					return;
				}
			} else if (targetObject.CompareTag("Chest")){
				//Debug.Log (Displacement.sqrMagnitude);
				if(Displacement.sqrMagnitude < 1.25){
					//Debug.Log ("FoundChest");
					targetObject.GetComponent<ChestController> ().Open ();
					here = true;
					return;
				}
			}else if (targetObject.CompareTag("NPC")){
				if(Displacement.sqrMagnitude < 1.25f){
					here = true; 
					return;
				}
			}
		}
		Displacement = path[currentTarget] - transform.position;
		float theta = Mathf.Atan2 (Displacement.x, Displacement.z) * Mathf.Rad2Deg;
		transform.position = Vector3.MoveTowards (transform.position, path[currentTarget], moveSpeed * Time.deltaTime);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.Euler(0, theta, 0), turnSpeed *Time.deltaTime);

		if(transform.position == path[currentTarget]){
			currentTarget++;
			if(currentTarget == path.Count){
				here = true;
			}
		}
	}

	public int GetDoor(){
		return nextDoor;
	}

	public void SetNextDoor(int NextDoor){
		nextDoor = NextDoor;
	}

	public void SetPathfinder(MyPathfinding _pathfinder){
		pathfinder = _pathfinder;
	}

	public int GetDamage(){
		return 5;
	}

	public void Damage(){
		
	}
}
