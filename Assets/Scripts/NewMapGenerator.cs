using UnityEngine;
using System.Collections;

public class NewMapGenerator : MonoBehaviour {

	[SerializeField]private int height, width, scale;
	[SerializeField]private string seed;
	[SerializeField]private bool useRandomSeed;
	[SerializeField]private GameObject floor, wall, edge, parallel, perpendicular, three, surrounded, corner, tree;
	[SerializeField]private Mesh[] treeMeshs;
	[SerializeField]private int treeCount, treeDensity, treeSpread;
	[SerializeField]private GameObject[] doors = new GameObject[4];
	[SerializeField]private LayerMask myLayermask;
	[SerializeField]private GameObject[] enemies;
	[SerializeField]private int[] enemyCount, enemyDensity, enemySpread;
	[SerializeField]private int chestCount;
	[SerializeField]private GameObject chest;
	[SerializeField]private GameObject[] availableLoot;
	private int randomFillPercent;

	private MyPathfinding myPathfinding;

	int[,] map;
	int changes = 1;

	void Awake(){
		
	}

	void Start(){
		GenerateMap ();
	}

	void Update(){
		
	}

	private void GenerateMap(){
		myPathfinding = GetComponent<MyPathfinding>();
		map = new int[width, height];
		RandomFillMap ();
		while (changes > 0) {
			changes = 0;
			SmoothMap ();
		}
		changes = 1;

		CheckWalls ();
		PlaceEnemies ();
		GenerateTrees ();
		GenerateChests ();
		PlaceDoors ();
		myPathfinding.OnStart (new Vector2(width, height) * scale);
	}

	void RandomFillMap(){
		if(useRandomSeed){
			seed = System.DateTime.Now.ToString ();
		}
		System.Random pseudoRandom = new System.Random (seed.GetHashCode ());
		for (int i = 0; i < width; i++) {
			for (int o = 0; o < height; o++) {
				if (i == 0 || i == width - 1 || o == 0 || o == height - 1) {
					map [i, o] = 1;
				} else {
					float valueX = ((320 * i * i) / (width * width)) - ((320 * i) / width) + 90;
					float valueY = ((320 * o * o) / (height * height)) - ((320 * o) / height) + 90;
					if(valueX > valueY){
						map [i, o] = (pseudoRandom.Next (0, 100) < valueX) ? 1 : 0;
					}else{
						map [i, o] = (pseudoRandom.Next (0, 100) < valueY) ? 1 : 0;
					}
				}
			}
		}
	}

	void SmoothMap(){
		for(int i = 0; i < width; i++){
			for (int o = 0; o < height; o++) {
				int neighborWallTiles = GetSurroundingWallCount (i, o);
				if (neighborWallTiles > 4) {
					if (map [i, o] != 1) {
						changes++;
					}
					map [i, o] = 1;
				} else if(neighborWallTiles < 4) {
					if (map [i, o] != 0) {
						changes++;
					}
					map [i, o] = 0;
				}
			}
		}
	}

	int GetSurroundingWallCount(int gridX, int gridY){
		int WallCount = 0;
		for(int i = gridX - 1; i <= gridX +1; i++){
			for(int o = gridY - 1; o <= gridY +1; o++){
				if(i >= 0 && i < width && o >= 0 && o < height){
					if(i != gridX || o != gridY){
						WallCount += map [i, o];
					}
				}else{
					WallCount++;
				}
			}
		}
		return WallCount;
	}

	void CheckWalls(){
		for (int i = 0; i < width; i++) {
			for (int o = 0; o < height; o++) {
				if (map [i, o] == 1) {
					int myCase = 0;
					for (int e = 0; e < 4; e++) {
						int checkX = Mathf.RoundToInt(Mathf.Cos (e * 90 * Mathf.Deg2Rad)) + i;
						int checkY = Mathf.RoundToInt(Mathf.Sin (e * 90 * Mathf.Deg2Rad)) + o;
						if (checkX < 0 || checkX == width || checkY < 0 || checkY == height) {
							myCase += Mathf.RoundToInt (Mathf.Pow (10, e));
						} else if (map [checkX, checkY] == 1) {
							myCase += Mathf.RoundToInt (Mathf.Pow (10, e));
						}
					}
					InstantiateWall (i, o, myCase);
				} else {
					Object.Instantiate(floor, new Vector3(i * scale, 0, o * scale), Quaternion.Euler(0,0,0));
				}
			}
		}
	}

	void InstantiateWall(int i, int o, int myCase){
		switch (myCase) {
		case 0000:
			Object.Instantiate (surrounded, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 0, 0));
			break;
		case 0001:
			Object.Instantiate (three, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 0, 0));
			break;
		case 0010:
			Object.Instantiate (three, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 0, 0));
			break;
		case 0011:
			if(i - 1 > -1 && o - 1 > -1){
				if (map [i - 1, o - 1] == 0) {
					Object.Instantiate (corner, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 180, 0));
				} else {
					Object.Instantiate (perpendicular, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 270, 0));
				}
			}
			break;
		case 0100:
			Object.Instantiate (three, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 0, 0));
			break;
		case 0101:
			Object.Instantiate (parallel, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 0, 0));
			break;
		case 0110:
			if (i + 1 < width && o -1  > -1) {
				if (map [i + 1, o - 1] == 0) {
					Object.Instantiate (corner, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 90, 0));
				} else {
					Object.Instantiate (perpendicular, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 180, 0));
				}
			}
			break;
		case 0111:
			Object.Instantiate (edge, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 270, 0));
			break;
		case 1000:
			Object.Instantiate (three, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 0, 0));
			break;
		case 1001:
			if (i -1 > -1 && o + 1  < height) {
				if (map [i - 1, o + 1] == 0) {
					Object.Instantiate (corner, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 270, 0));
				} else {
					Object.Instantiate (perpendicular, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 0, 0));
				}
			}
			break;
		case 1010:
			Object.Instantiate (parallel, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 0, 0));
			break;
		case 1011:
			Object.Instantiate (edge, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 0, 0));
			break;
		case 1100:
			if (i + 1 < width && o + 1  < height) {
				if (map [i + 1, o + 1] == 0) {
					Object.Instantiate (corner, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 0, 0));
				} else {
					Object.Instantiate (perpendicular, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 90, 0));
				}
			}
			break;
		case 1101:
			Object.Instantiate (edge, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 90, 0));
			break;
		case 1110:
			Object.Instantiate (edge, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 180, 0));
			break;
		case 1111:
			bool stop = false;
			for(int e = -1; e < 2; e +=2){
				for(int a = -1; a < 2; a +=2){
					if(i + e > -1 && i + e < width && o + a > -1 && o + a < height){
						if (map [i + e, o + a] == 0) {
							Object.Instantiate (wall, new Vector3 (i * scale, 0, o * scale), Quaternion.Euler (0, 0, 0));
							stop = true;
						}
					}
				}
				if (stop) {
					break;
				}
			}
			break;
		default:
			Debug.Log ("error");
			break;
		}
	}

	void GenerateTrees(){	
		Vector3 center = Vector3.zero;
		for(int a = 0; a < Mathf.RoundToInt(treeCount / treeDensity); a++){
			for(int e = 0; e < 100; e++){
				center = new Vector3 (Random.Range(0, width * scale), 0, Random.Range(0,height * scale));
				if(IsEmpty(center)){
					Vector3 pos = Vector3.zero;
					for (int i = 0; i < treeDensity; i++) {
						for (int o = 0; o < 100; o++) {
							float theta = Random.Range (0,360) * Mathf.Deg2Rad;
							pos = center + (new Vector3 (Mathf.Sin(theta), 0.1f, Mathf.Cos(theta)) * Random.Range(0f, treeSpread));
							if(IsEmpty(pos)){
								Object.Instantiate (tree, pos, Quaternion.Euler (0, Random.Range (0, 360), 0));
								break;
							}
						}
					}
					break;
				}
			}
		}
		GameObject[] myTrees = GameObject.FindGameObjectsWithTag ("Tree");
		foreach(GameObject tree in myTrees){
			Mesh myMesh = treeMeshs[Random.Range (0, treeMeshs.Length)];
			tree.GetComponent<MeshFilter> ().mesh = myMesh;
			tree.GetComponent<MeshCollider>().sharedMesh = myMesh;
		}
	}

	private void PlaceDoors(){
		int lowX = width;
		int lowY = height;
		int highX = 0;
		int highY = 0;
		for(int i = 0; i < width; i++){
			for (int o = 0; o < height; o++) {
				if(map[i,o] == 0){
					if(i < lowX){
						lowX = i;
					}
					if(i > highX){
						highX = i;
					}
					if(o < lowY){
						lowY = o;
					}
					if(o > highY){
						highY = o;
					}
				}
			}
		}
		int xDif = (highX - lowX) / 4;
		int yDif = (highY - lowY) / 4;
		if(doors[0] != null){
			int random = Random.Range (lowY + yDif, highY - yDif);
			for(int i = 0; i < width; i++){
				if(map[i,random]==0){
					doors[0].transform.position = new Vector3 (i * scale,0, random*scale);
					break;
				}
			}
		}
		if(doors[1] != null){
			int random = Random.Range (lowX + xDif, highX - xDif);
			for(int i = 0; i < height; i++){
				if(map[random,i]==0){
					doors[1].transform.position = new Vector3  (random * scale,0, i*scale);
					break;
				}
			}
		}
		if(doors[2]!= null){
			int random = Random.Range (lowY + yDif, highY - yDif);
			for(int i = width - 1; i >= 0; i--){
				if(map[i,random]==0){
					doors[2].transform.position = new Vector3  (i * scale,0, random*scale);
					break;
				}
			}
		}
		if(doors[3]!= null){
			int random = Random.Range (lowX + xDif, highX - xDif);
			for(int i = height - 1; i >= 0; i--){
				if(map[random,i]==0){
					doors[3].transform.position = new Vector3  (random * scale,0, i*scale);
					break;
				}
			}
		}

		for (int i = 0; i < doors.Length; i++) {
			doors [i].GetComponent<DoorController> ().CheckDoor ();
		}
	}

	private void PlaceEnemies(){
		for(int a = 0; a < enemies.Length; a ++){
			int length = Mathf.RoundToInt(enemyCount[a] / enemyDensity[a]);
			Debug.Log ("length = " + length);
			for(int e = 0; e < length; e++){
				for(int i = 0; i < 100; i++){
					Vector3 center = new Vector3 (Random.Range(0, width * scale), 0, Random.Range(0,height * scale));
					if(IsEmpty(center)){
						for(int o = 0; o < enemyDensity[a]; o++){
							for(int u = 0; u < 100; u++){
								float theta = Random.Range (0,360) * Mathf.Deg2Rad;
								Vector3 place = center + (new Vector3 (Mathf.Sin(theta), 0.1f, Mathf.Cos(theta)) * Random.Range(0f, enemySpread[a]));
								//Debug.Log (place);
								if (IsEmpty (place)) {
									Object.Instantiate (enemies [a], place, enemies [a].transform.rotation);
									break;
								} else {
									//Debug.Log ("failed");
								}
							}
						}
						break;
					}
				}
			}
		}
	}

	private bool IsEmpty(Vector3 _spot){
		int CheckX = Mathf.FloorToInt(_spot.x / scale);
		int CheckY = Mathf.FloorToInt (_spot.z / scale);
		if (map [CheckX, CheckY] == 0) {
			if (!Physics.CheckSphere (_spot, 0.5f, myLayermask, QueryTriggerInteraction.Ignore)) {
				return true;
			} else {
				//Debug.Log ("already Inhabited");
			}
		} else {
			//Debug.Log ("Not on the Grid");
		}
		return false;
	}

	private void GenerateChests(){
		for(int a = 0; a < chestCount; a ++){
			Vector3 center = Vector3.zero;
			for (int e = 0; e < 100; e++) {
				center = new Vector3 (Random.Range (0, width * scale), 0, Random.Range (0, height * scale));
				if (IsEmpty (center)) {
					break;
				}
			}
			GameObject myChest = Instantiate (chest, center, Quaternion.Euler (new Vector3 (0, Random.Range (0, 360), 0))) as GameObject;
			int myRandom = Random.Range (0, 4);
			switch (myRandom) {
			case 3:
				myRandom = 1;
				break;
			default:
				break;
			}
			GameObject[] loot = new GameObject[myRandom];
			if(myRandom > 0){
				for(int i = 0; i < myRandom; i ++){
					loot[i] = Object.Instantiate(availableLoot [Random.Range (0, availableLoot.Length)], center, Quaternion.Euler(Vector3.zero)) as GameObject;
					loot [i].SetActive (false);
				}
			}
			myChest.GetComponent<ChestController> ().SetLoot (loot);
		}
	}
}
