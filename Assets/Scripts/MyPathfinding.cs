using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyPathfinding : MonoBehaviour {

	private Node StartNode;
	private Node EndNode;
	private Node[,] grid;
	private float nodeRadius = 0.5f;
	private int nodeDiameter = 1;
	private int gridSizeX, gridSizeY;
	private List<Node> path = new List<Node>();
	private Vector2 gridWorldSize;
	[SerializeField] private LayerMask unwalkableMask;

	// Use this for initialization
	public void OnStart (Vector2 worldSize) {
		gridWorldSize = worldSize;
		gridSizeX = Mathf.FloorToInt (gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.FloorToInt (gridWorldSize.y / nodeDiameter);
		CreateGrid ();
		PlayerController.instance.SetPathfinder(this);
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (new Vector3(gridWorldSize.x / 2, 0.5f, gridWorldSize.y / 2), new Vector3(gridWorldSize.x, 0, gridWorldSize.y));
		if(grid != null){
			foreach (Node n in grid) {
				Gizmos.color = (n.GetWalkable())?Color.green:Color.red;
				if (path != null) {
					if (path.Contains (n)) {
						Gizmos.color = Color.black;
					}
				}
				if(n == StartNode){
					Gizmos.color = Color.blue;
				}else if(n == EndNode){
					Gizmos.color = Color.yellow;
				}
				Gizmos.DrawCube (n.GetWorldPos(), Vector3.one * nodeDiameter);
			}
		}
	}

	private void CreateGrid (){
		grid = new Node[gridSizeX, gridSizeY];
		for (int i = 0; i < gridSizeX; i++) {
			for (int o = 0; o < gridSizeY; o++) {
				Vector3 worldPoint = new Vector3 (i * nodeDiameter + nodeRadius, 0, o * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius - 0.01f, unwalkableMask));
				grid [i, o] = new Node (walkable, worldPoint, i,o);
			}
		}
	}

	public Node NodeFormWolrdPoint(Vector3 _worldPoint){
		float percentX = _worldPoint.x/gridWorldSize.x;
		float percentY = _worldPoint.z/gridWorldSize.y;
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);
		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		//Debug.Log (grid [x, y]);
		return grid [x, y];
	}

	private List<Node> GetNeighbors(Node node){
		List<Node> neighbors = new List<Node> ();
		for(int i = -1; i <  2; i ++){
			for(int o = -1; o < 2; o ++){
				if(i == 0 && o == 0){
					continue;
				}
				int checkX = node.GetGridX() + i;
				int checkY = node.GetGridY() + o;
				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY){
					neighbors.Add (grid[checkX, checkY]);
				}
			}
		}
		return neighbors;
	}

	int GetDistance(Node nodeA, Node nodeB){
		int distanceX = Mathf.Abs (nodeA.GetGridX() - nodeB.GetGridX());
		int distanceY = Mathf.Abs (nodeA.GetGridY () - nodeB.GetGridY ());
		if (distanceX > distanceY) {
			return (14 * distanceY) + (10 * (distanceX - distanceY));
		} else {
			return (14 * distanceX) + (10 * (distanceY - distanceX));
		}
	}

	public List<Vector3> FindPath (Vector3 startPos, Vector3 targetPos){
		StartNode = NodeFormWolrdPoint (startPos);
		EndNode = NodeFormWolrdPoint (targetPos);
		List<Node> openSet = new List<Node> ();
		List<Node> closedSet = new List<Node> ();
		openSet.Add (StartNode);
		/*
		//Start of my stupid code
		bool reachable = false;
		foreach(Node neighbor in GetNeighbors(EndNode)){
			if (neighbor.GetWalkable ()) {
				reachable = true;
				break;
			}
		}

		if (reachable) {
			//end of my stupid code (minus the closing bracket after this)	
			*/
			while (openSet.Count > 0) {
				Node currentNode = openSet [0];
				for (int i = 1; i < openSet.Count; i++) {
					if (openSet [i].GetfCost () < currentNode.GetfCost () || (openSet [i].GetfCost () == currentNode.GetfCost () && openSet [i].GethCost () < currentNode.GethCost ())) {
						currentNode = openSet [i];
					}
				}
				openSet.Remove (currentNode);
				closedSet.Add (currentNode);

				if (currentNode == EndNode) {
					break;
				}
				foreach (Node neighbor in GetNeighbors(currentNode)) {
					if (((neighbor.GetWalkable () == false) && neighbor != EndNode) || closedSet.Contains (neighbor)) {
						continue;
					}
					int newMovementCostToNeighbor = currentNode.GetgCost () + GetDistance (currentNode, neighbor);
					if (newMovementCostToNeighbor < neighbor.GetgCost () || !openSet.Contains (neighbor)) {
						neighbor.SetgCost (newMovementCostToNeighbor);
						neighbor.SethCost (GetDistance (neighbor, EndNode));
						neighbor.SetParent (currentNode);

						if (!openSet.Contains (neighbor)) {
							openSet.Add (neighbor);
						}
					}
				}
			}
			return RetracePath (StartNode, EndNode);
	}

	private List<Vector3> RetracePath(Node startNode, Node targetNode){
		List<Node> newPath = new List<Node> ();
		Node currentNode = targetNode;

		while(currentNode != startNode){
			newPath.Add (currentNode);
			if (currentNode.GetParent () != null) {
				currentNode = currentNode.GetParent ();
			} else {
				return new List<Vector3>();
			}
		}
		newPath.Reverse ();
		path = newPath;
		return RefinePath (path);
	}

	private List<Vector3> RefinePath (List<Node> path){
		List<Vector3> smoothPath = new List<Vector3> ();	
		for (int i = 0; i < path.Count; i++) {
			Vector3 currentPoint = path[i].GetWorldPos();
			for (int o = i + 1; o < path.Count; o ++){
				Vector3 endPoint = path[o].GetWorldPos();
				bool there = false;
				while(!there){
					currentPoint = Vector3.MoveTowards (currentPoint, endPoint, 0.1f);
					Node CurrentNode = NodeFormWolrdPoint(currentPoint);
					if(CurrentNode == path[i]){
						continue;
					}else if(CurrentNode == path[o]){
						there = true;
					}else if(CurrentNode.GetWalkable() == false){
						smoothPath.Add (path[o-1].GetWorldPos());
						i = o - 1;
						break;
					}
				}
				if(!there){
					break;
				}
			}
		}
		return smoothPath;
	}

	public LayerMask GetLayerMask(){
		return unwalkableMask;
	}
}
